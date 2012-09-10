 /*
 * Terrier - Terabyte Retriever
 * Webpage: http://ir.dcs.gla.ac.uk/terrier
 * Contact: terrier{a.}dcs.gla.ac.uk
 * University of Glasgow - Department of Computing Science
 * Information Retrieval Group
 *
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
 * the License for the specific language governing rights and limitations
 * under the License.
 *
 * The Original Code is Manager.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.querying;
import gnu.trove.TIntArrayList;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Hashtable;
import uk.ac.gla.terrier.matching.Matching;
import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.matching.Model;
import uk.ac.gla.terrier.matching.QueryResultSet;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.matching.dsms.BooleanScoreModifier;
import uk.ac.gla.terrier.querying.parser.FieldQuery;
import uk.ac.gla.terrier.querying.parser.Query;
import uk.ac.gla.terrier.querying.parser.RequirementQuery;
import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.terms.TermPipeline;
import uk.ac.gla.terrier.terms.TermPipelineAccessor;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
  * This class is responsible for handling/co-ordinating the main high-level
  * operations of a query. These are:
  * <li>Pre Processing (Term Pipeline, Control finding, term aggregration)</li>
  * <li>Matching</li>
  * <li>Post-processing @see uk.ac.gla.terrier.querying.PostProcess</li>
  * <li>Post-filtering @see uk.ac.gla.terrier.querying.PostFilter</li>
  * </ul>
  * Example usage:
  * <pre>
  * Manager m = new Manager(index);
  * SearchRequest srq = m.newSearchRequest();
  * srq.setQuery(query);
  * m.runPreProcessing(srq);
  * m.runMatching(srq);
  * m.runPostProcess(srq);
  * m.runPostFilters(srq);
  * </pre>
  * <p>
  * <b>Properties</b><ul>
  * <li><tt>querying.default.controls</tt> - sets the default controls for each query</li>
  * <li><tt>querying.allowed.controls</tt> - sets the controls which a users is allowed to set in a query</li>
  * <li><tt>querying.postprocesses.order</tt> - the order post processes should be run in</li>
  * <li><tt>querying.postprocesses.controls</tt> - mappings between controls and the post processes they should cause</li>
  * <li><tt>querying.postfilters.order</tt> - the order post filters should be run in </li>
  * <li><tt>querying.postfilters.controls</tt> - mappings between controls and the post filters they should cause</li>
  * </ul>
  * <p><b>Controls</b><ul>
  * <li><tt>start</tt> : The result number to start at - defaults to 0 (1st result)</li>
  * <li><tt>end</tt> : the result number to end at - defaults to 0 (display all results)</li>
  * <li><tt>c</tt> : the c parameter for the DFR models, or more generally, the parameters for weighting model scheme</li>
  * </ul>
  */
public class Manager implements TermPipeline, TermPipelineAccessor
{
	/* ------------Module default namespaces -----------*/
	/** The default namespace for PostProcesses to be loaded from */
	public static final String NAMESPACE_POSTPROCESS
		= "uk.ac.gla.terrier.querying.";
	/** The default namespace for PostFilters to be loaded from */
	public static final String NAMESPACE_POSTFILTER
		= "uk.ac.gla.terrier.querying.";
	/** The default namespace for Matching models to be loaded from */
	public static final String NAMESPACE_MATCHING
		= "uk.ac.gla.terrier.matching.";
	/** The default namespace for Weighting models to be loaded from */
	public static final String NAMESPACE_WEIGHTING
		= "uk.ac.gla.terrier.matching.models.";
	/** The default namespace for TermPipeline modules to be loaded from */
	public final static String NAMESPACE_PIPELINE = "uk.ac.gla.terrier.terms.";
	/* ------------------------------------------------*/
	/* ------------Instantiation caches --------------*/
	/** Cache loaded Matching models in this hashtable */
	protected Hashtable Cache_Matching = new Hashtable();
	/** Cache loaded Weighting models in this hashtable */
	protected Hashtable Cache_Weighting = new Hashtable();
	/** Cache loaded PostProcess models in this hashtable */
	protected Hashtable Cache_PostProcess = new Hashtable();
	/** Cache loaded PostFitler models in this hashtable */
	protected Hashtable Cache_PostFilter = new Hashtable();
	/* ------------------------------------------------*/
	/** The term pipeline to pass all terms through */
	protected TermPipeline pipeline_first;
	/** The index this querying comes from */
	protected Index index;
	/** This contains a list of controls that may be set in the querying API */
	protected HashSet Allowed_Controls;
	/** This contains the mapping of controls and their values that should be 
	  * set identially for each query created by this Manager */
	protected Hashtable Default_Controls;
	/** How many default controls exist.
	  * Directly corresponds to Default_Controls.size() */
	protected int Defaults_Size = 0;

	/** An ordered list of post process names. The controls at the same index in the PostProcesses_Controls
	  * list turn on the post process in this list. */
	protected String[] PostProcesses_Order;
	
	/** A 2d array, contains (on 2nd level) the list of controls that turn on the PostProcesses
	  * at the same 1st level place on PostProcesses_Order */
	protected String[][] PostProcesses_Controls;

	/** An ordered list of post filters names. The controls at the same index in the  PostFilters_Controls
	  * list turn on the post process in this list. */
	protected String[] PostFilters_Order;

	/** A 2d array, contains (on 2nd level) the list of controls that turn on the PostFilters
	  * at the same 1st level place on PostFilters_Order */
	protected String[][] PostFilters_Controls;
	
	/** This class is used as a TermPipelineAccessor, and this variable stores
	  * the result of the TermPipeline run for that term. */
	protected String pipelineOutput = null;
	/** Constructor for making a querying copy of Terrier. All client
	  * code should instantiate one of these with the index they wish to
	  * use.
	  * @param diskIndexes The Index that should be used for this instance of Terrier
	  */
	public Manager(Index diskIndexes)
	{
		index = diskIndexes;
		load_pipeline();
		load_controls_allowed();
		load_controls_default();
		load_postprocess_controls();
		load_postfilters_controls();
	}
	/* ----------------------- Initialisation methods --------------------------*/
	/** load in the controls that user is allowed to set */
	protected void load_controls_allowed()
	{
		/* load in the controls that user is allowed to set */
		String allowed = ApplicationSetup.getProperty("querying.allowed.controls", "c,range").trim().toLowerCase();
		String[] controls = allowed.split("\\s*,\\s*");
		Allowed_Controls = new HashSet();
		for(int i=0;i<controls.length;i++)
		{
			Allowed_Controls.add(controls[i]);
		}
	}
	/** load in the control defaults */
	protected void load_controls_default()
	{
		String defaults = ApplicationSetup.getProperty("querying.default.controls", "").trim();
		String[] controls = defaults.split("\\s*,\\s*");
		Default_Controls = new Hashtable();
		for(int i=0;i<controls.length;i++)
		{
			String control[] = controls[i].split(":");
			/* control[0] contains the control name, control[1] contains the value */
			if (control.length < 2)
			{
				continue;
			}
			Default_Controls.put(control[0].toLowerCase(), control[1]);
		}
		Defaults_Size = Default_Controls.size();
	}
	
	protected static String[] tinySingleStringArray = new String[0];
	protected static String[][] tinyDoubleStringArray = new String[0][0];
	
	/** load in the allowed postprocceses controls, and the order to run post processes in */
	protected void load_postprocess_controls()
	{
		/* what we have is a mapping of controls to post processes, and an order post processes should
		   be run in.
		   what we need is the order to check the controls in, and which pp to run for each
		*/

		String[] order_pp, control_pp;
		String tmp = ApplicationSetup.getProperty("querying.postprocesses.order", "").trim();
		if (tmp.length() > 0)
			order_pp = tmp.split("\\s*,\\s*");
		else
			order_pp = new String[0];
		
		tmp = ApplicationSetup.getProperty("querying.postprocesses.controls", "").trim();
		if (tmp.length() > 0)
			control_pp = tmp.split("\\s*,\\s*");
		else
			control_pp = new String[0];
		
		//control_and_pp holds an array of pairs - control, pp, control, pp, control, pp
		String[] control_and_pp = new String[control_pp.length*2]; int count = 0;
		
		//iterate through controls and pp names putting in 1d array
		for(int i=0; i<control_pp.length; i++)
		{
			if (control_pp[i].indexOf(":") > 0)
			{
				String[] control_and_postprocess = control_pp[i].split(":");
				control_and_pp[count] = control_and_postprocess[0];//control
				control_and_pp[count+1] = control_and_postprocess[1];//postfilter
				count+=2;
			}
		}

		/* basically, we now invert, so we have an array of pp names, and in a separate array, a list
		of controls that can turn that pf on */
		ArrayList pp_order = new ArrayList();
		ArrayList pp_controls = new ArrayList();
		
		for(int i=0; i<order_pp.length; i++)
		{
			ArrayList controls_for_this_pp = new ArrayList();
			String tmpPP = order_pp[i];
			for(int j=0;j<count;j+=2)
			{
				if (tmpPP.equals(control_and_pp[j+1]))
				{
					controls_for_this_pp.add(control_and_pp[j]);
				}
			}
			//ok, there are controls that can turn this pf on, so lets enable it
			if (controls_for_this_pp.size() > 0)
			{
				pp_controls.add((String[])controls_for_this_pp.toArray(tinySingleStringArray));
				pp_order.add(tmpPP);
			}
		}
		//cast back to arrays
		PostProcesses_Order= (String[])pp_order.toArray(tinySingleStringArray);
		PostProcesses_Controls = (String[][])pp_controls.toArray(tinyDoubleStringArray);
	}


	/** load in the allowed post filter controls, and the order to run post processes in */
	protected void load_postfilters_controls()
	{
		/* what we have is a mapping of controls to post filters, and an order post processes should
		   be run in.
		   what we need is the order to check the controls in, and which pp to run for each
		*/

		String[] order_pf, control_pf;
		String tmp = ApplicationSetup.getProperty("querying.postfilters.order", "").trim();
		if (tmp.length() > 0)
			order_pf = tmp.split("\\s*,\\s*");
		else
			order_pf = new String[0];
		
		tmp = ApplicationSetup.getProperty("querying.postfilters.controls", "").trim();
		if (tmp.length() > 0)
			control_pf = tmp.split("\\s*,\\s*");
		else
			control_pf = new String[0];
		
		String[] control_and_pf = new String[control_pf.length*2]; int count = 0;
		//iterate through controls and pf names putting in 1d array
		for(int i=0; i<control_pf.length; i++)
		{
			if (control_pf[i].indexOf(":") > 0)
			{
				String[] control_and_postfilter = control_pf[i].split(":");
				control_and_pf[count] = control_and_postfilter[0];//control
				control_and_pf[count+1] = control_and_postfilter[1];//postfilter
				count+=2;
			}
		}

		/* basically, we now invert, so we have an array of pf names, in a separate array, a list
		of controls that can turn that pf on */
		ArrayList pf_order = new ArrayList();
		ArrayList pf_controls = new ArrayList();
		for(int i=0; i<order_pf.length; i++)
		{
			ArrayList controls_for_this_pf = new ArrayList();
			String tmpPF = order_pf[i];
			for(int j=0;j<count;j+=2)
			{
				if (tmpPF.equals(control_and_pf[j+1]))
				{
					controls_for_this_pf.add(control_and_pf[j]);
				}
			}
			//ok, there are controls that can turn this pf on, so lets enable it
			if (controls_for_this_pf.size() > 0)
			{
				pf_controls.add((String[])controls_for_this_pf.toArray(tinySingleStringArray));
				pf_order.add(tmpPF);
			}			
		}
		//cast back to arrays
		PostFilters_Order = (String[])pf_order.toArray(tinySingleStringArray);
		PostFilters_Controls = (String[][])pf_controls.toArray(tinyDoubleStringArray);
	}

	private static Class[] constructor_array_termpipeline = new Class[]{TermPipeline.class};

	/** load in the term pipeline */
	protected void load_pipeline()
	{
		String[] pipes = ApplicationSetup.getProperty(
				"termpipelines", "PorterStemmer").trim()
				.split("\\s*,\\s*");
		TermPipeline next = this;
		TermPipeline tmp;
		for(int i=pipes.length-1; i>=0; i--)
		{
			try{
				String className = pipes[i];
				if (className.length() == 0)
					continue;
				if (className.indexOf(".") < 0 )
					className = NAMESPACE_PIPELINE + className;
				Class pipeClass = Class.forName(className, false, this.getClass().getClassLoader());
				tmp = (TermPipeline) (pipeClass.getConstructor(
						constructor_array_termpipeline)
						.newInstance(new Object[] {next}));
				next = tmp;
			}catch (Exception e){
				System.err.println("TermPipeline object "+NAMESPACE_PIPELINE+pipes[i]+" not found: "+e);
			}
		}
		pipeline_first = next;
	}

	/* -------------------term pipeline implementation --------------------*/
	/**
	 * Make this object a term pipeline implementor.
	 * @see uk.ac.gla.terrier.terms.TermPipeline
	 */
	public void processTerm(String t)
	{
		pipelineOutput = t;
	}
	/*-------------- term pipeline accessor implementation ----------------*/
	/** A term pipeline accessor */
	public String pipelineTerm(String t)
	{
		pipelineOutput = null;
		pipeline_first.processTerm(t);
		return pipelineOutput;
	}
	/* -------------- factory methods for SearchRequest objects ---------*/
	/** Ask for new SearchRequest object to be made. This is internally a
	  * Request object */
	public SearchRequest newSearchRequest()
	{
		Request q = new Request();
		if (Defaults_Size >0)
			setDefaults(q);
		return (SearchRequest)q;
	}
	/** Ask for new SearchRequest object to be made. This is internally a
	  * Request object
	  * @param QueryID The request should be identified by QueryID
	  */
	public SearchRequest newSearchRequest(String QueryID)
	{
		Request q = new Request();
		if (Defaults_Size >0)
			setDefaults(q);
		q.setQueryID(QueryID);
		return (SearchRequest)q;
	}
	/** Set the default values for the controls of this new search request
	 *  @param srq The search request to have the default set to. This is
	 *  done using the Default_Controls table, which is loaded by the load_controls_default
	 *  method. The default are set in the properties file, by the <tt>querying.default.controls</tt> */
	protected void setDefaults(Request srq)
	{
		//NB/TODO: Hashtable.clone() is relatively expensive according j2sdk docs for Hashtable
		srq.setControls((Hashtable)Default_Controls.clone());
	}
	/**
	 * Returns the index used by the manager. It is used for matching
	 * and possibly post-processing.
	 * @return Index the index used by the manager.
	 */
	public Index getIndex() {
		return index;
	}
	//run methods
	//These methods are called by the application in turn
	//(or could just have one RUN method, and these are privates,
	//but I would prefer the separate method)
	/** runPreProcessing */
	public void runPreProcessing(SearchRequest srq)
	{
		Request rq = (Request)srq;
		Query query = rq.getQuery();
		//System.out.println(query);
		//get the controls
		boolean rtr = ! query.obtainControls(Allowed_Controls, rq.getControlHashtable());
		//we check that there is stil something left in the query
		if (! rtr)
		{
			rq.setEmpty(true);
			return;
		}
		rtr = query.applyTermPipeline(this);
		if (! rtr)
		{
			rq.setEmpty(true);
			return;
		}
		MatchingQueryTerms queryTerms = new MatchingQueryTerms();
		query.obtainQueryTerms(queryTerms);
		rq.setMatchingQueryTerms(queryTerms);
	}
	/** Runs the weighting and matching stage - this the main entry
	  * into the rest of the Terrier framework.
	  * @param srq the current SearchRequest object.
	  */
	public void runMatching(SearchRequest srq)
	{
		Request rq = (Request)srq;
		if (! rq.isEmpty())
		{
			//TODO some exception handling here for not found models
			Model wmodel = getWeightingModel(rq.getWeightingModel());
			wmodel.setParameter(Double.parseDouble(rq.getControl("c")));
			Matching matching = getMatchingModel(rq.getMatchingModel());
			matching.setModel(wmodel);
			System.err.println("weighting model: " + wmodel.getInfo());
			MatchingQueryTerms mqt = rq.getMatchingQueryTerms();
			Query q = rq.getQuery();
			
			
			/* now propagate fields into requirements, and apply boolean matching
			   for the decorated terms. */
			ArrayList requirement_list = new ArrayList();
			ArrayList field_list = new ArrayList();
			
			q.getTermsOf(RequirementQuery.class, requirement_list, true);
			q.getTermsOf(FieldQuery.class, field_list, true);
			for (int i=0; i<field_list.size(); i++){
				if (!requirement_list.contains(field_list.get(i))){
					requirement_list.add(field_list.get(i));				
				}
			}
			if (requirement_list.size()>0) {
				mqt.addDocumentScoreModifier(new BooleanScoreModifier(requirement_list));
			}

			mqt.setQuery(q);
			mqt.normaliseTermWeights();
			matching.match(rq.getQueryID(), mqt);
			//matching.match(rq.getQueryID(), rq.getMatchingQueryTerms());
			//now crop the collectionresultset down to a query result set.
			ResultSet outRs = matching.getResultSet();
			rq.setResultSet((ResultSet)(outRs.getResultSet(0, outRs.getResultSize())));			
		}
		else
		{
			System.err.println("Returning empty result set as query "+rq.getQueryID()+" is empty");
			rq.setResultSet(new QueryResultSet(0));
		}
	}
	/** Runs the PostProcessing modules in order added. PostProcess modules
	  * alter the resultset. Examples might be query expansions which completelty replaces
	  * the resultset.
	  * @param srq the current SearchRequest object. */
	public void runPostProcessing(SearchRequest srq)
	{
		Request rq = (Request)srq;
		Hashtable controls = rq.getControlHashtable();
		
		for(int i=0; i<PostProcesses_Order.length; i++)
		{
			String PostProcesses_Name = PostProcesses_Order[i];
			for(int j=0; j<PostProcesses_Controls[i].length; j++)
			{
				String ControlName = PostProcesses_Controls[i][j];
				String value = (String)controls.get(ControlName);
				//System.err.println(ControlName+ "("+PostProcesses_Name+") => "+value);
				if (value == null)
					continue;
				value = value.toLowerCase();
				if(! (value.equals("off") || value.equals("false")))
				{
					System.err.println("Processing: "+PostProcesses_Name);
					getPostProcessModule(PostProcesses_Name).process(this, srq);
					//we've now run this post process module, no need to check the rest of the controls for it.
					break;
				}
			}
		}
	}
	
	
	/** Runs the PostFilter modules in order added. PostFilter modules
	  * filter the resultset. Examples might be removing results that don't have
	  * a hostname ending in the required postfix (site), or document ids that don't match
	  * a pattern (scope).
	  * @param srq the current SearchRequest object. */
	public void runPostFilters(SearchRequest srq)
	{
		Request rq = (Request)srq;
		PostFilter[] filters = getPostFilters(rq);
		final int filters_length = filters.length;
		ResultSet results = rq.getResultSet();
		int ResultsSize = results.getResultSize();
		
		//load in the lower and upper bounds of the resultset
		String tmp = rq.getControl("start");/* 0 based */
		if (tmp.length() == 0)
			tmp = "0";
		int Start = Integer.parseInt(tmp);
		tmp = rq.getControl("end");
		if (tmp.length() == 0)
			tmp = "0";
		int End = Integer.parseInt(tmp);/* 0 based */
		if (End == 0)
		{
			End = ResultsSize -1;
		}
		int length = End-Start+1;
		if (End >= ResultsSize)
			length = ResultsSize-Start;
		//we've got no filters set, so just give the results ASAP
		//System.out.println("filters_length="+filters_length);
		if (filters_length == 0)
		{
			rq.setResultSet( results.getResultSet(Start, length) );
			return;
		}

		//tell all the postfilters that they are processing another query
		for(int i=0;i<filters_length; i++)
		{
			filters[i].new_query(this, srq, results);
		}
		
		int doccount = -1;//0 means 1 document
		TIntArrayList docatnumbers = new TIntArrayList();//list of resultset index numbers to keep
		byte docstatus; int thisDocId;
		int[] docids = results.getDocids();
		//System.out.println("we have "+docids.length+" docids");
		int elitesetsize = results.getExactResultSize();
		//System.out.println("lastdoc="+elitesetsize);
		for(int thisdoc = 0; thisdoc < elitesetsize; thisdoc++)
		{
			//run this document through all the filters
			docstatus = PostFilter.FILTER_OK;
			thisDocId = docids[thisdoc];
			//run this doc through the filters
			for(int i=0;i<filters_length; i++)
			{
				if ( ( docstatus = filters[i].filter(this, srq, results, thisdoc, thisDocId) )
					== PostFilter.FILTER_REMOVE
				)
					break;
					//break if the document has to be removed
			}
			//if it's not being removed, then
			if (docstatus != PostFilter.FILTER_REMOVE) //TODO this should always be true
			{
				//success, another real document
				doccount++;
				//check if it's in our results "WINDOW"
				if (doccount >= Start)
				{
					if (doccount <= End)
					{	//add to the list of documents to keep
						docatnumbers.add(thisdoc);
						//System.out.println("Keeping @"+thisdoc);
					}
					else
					{
						//we've now got enough results, break
						break;
					}
				}
			}
			else
			{
				//System.out.println("Removed");
			}
		}
		if (docatnumbers.size() < docids.length)
		{
			//result set is definently shorter, replace with new one
			rq.setResultSet( results.getResultSet(docatnumbers.toNativeArray()));
		}
	}
	/** parses the controls hashtable, looking for references to controls, and returns the appropriate
	  * postfilters to be run. */
	private PostFilter[] getPostFilters(Request rq)
	{
		Hashtable controls = rq.getControlHashtable();
		ArrayList postfilters = new ArrayList();
		for(int i=0; i<PostFilters_Order.length; i++)
		{
			String PostFilter_Name = PostFilters_Order[i];
			for(int j=0; j<PostFilters_Controls[i].length; j++)
			{
				String ControlName = PostFilters_Controls[i][j];
				String value = (String)controls.get(ControlName);
				System.err.println(ControlName+ "("+PostFilter_Name+") => "+value);
				if (value == null)
					continue;
				value = value.toLowerCase();
				if(! (value.equals("off") || value.equals("false")))
				{
					postfilters.add(getPostFilterModule(PostFilter_Name));
					//we've now run this post process module, no need to check the rest of the controls for it.
					break;
				}
			}
		}
		return (PostFilter[])postfilters.toArray(new PostFilter[0]);
	}
	
	/*-------------------------------- helper methods -----------------------------------*/
	//helper methods. These get the appropriate modules named Name of the appropate type
	//from a hashtable cache, or instantiate them should they not exist. ref is then casted and returned.
	//this all makes the run*() code much much cleaner
	/** Returns the matching model named ModelName. Caches already 
	  * instantiaed matching models in Hashtable Cache_Matching.
	  * If the matching model name doesn't contain '.', then NAMESPACE_MATCHING
	  * is prefixed to the name. 
	  * @param ModelName The name of the class to instantiate and return*/
	protected Matching getMatchingModel(String ModelName)
	{
		Matching rtr = null;
		//add the namespace if the modelname is not fully qualified
		if (ModelName.indexOf(".") < 0 )
			ModelName = NAMESPACE_MATCHING +ModelName;
		//check for already instantiated class
		rtr = (Matching)Cache_Matching.get(ModelName);
		if (rtr == null)
		{
			try
			{
				//load the class
				Class formatter = Class.forName(ModelName, false, this.getClass().getClassLoader());
				//get the correct constructor - an Index class in this case
				Class[] params = {Index.class};
				Object[] params2 = {index};
				//and instantiate
				rtr = (Matching) (formatter.getConstructor(params).newInstance(params2));
			}
			catch(Exception e)
			{
				System.err.println("Problem with matching model named: "+ModelName+" : "+e);
				e.printStackTrace();
				return null;
			}
			Cache_Matching.put(ModelName, rtr);
		}
		return rtr;
	}
	/** Returns the weighting model named ModelName. Caches already
	  * instantiaed matching models in Hashtable Cache_Weighting.
	  * If the weighting model name doesn't contain '.', then 
	  * NAMESPACE_WEIGHTING is prefixed to the name. 
	  * @param ModelName The name of the weighting model to instantiate */
	protected Model getWeightingModel(String ModelName)
	{
		Model rtr = null;
		if (ModelName.indexOf(".") < 0 )
			ModelName = NAMESPACE_WEIGHTING +ModelName;
		//check for already instantiated model
		rtr = (Model)Cache_Weighting.get(ModelName);
		if (rtr == null)
		{
			try
			{
				rtr = (Model) Class.forName(ModelName).newInstance();
			}
			catch(Exception e)
			{
				System.err.println("Problem with weighting model named: "+ModelName+" : "+e);
				return null;
			}
			Cache_Weighting.put(ModelName, rtr);
		}
		return rtr;
	}
	/** Returns the PostProcess named Name. Caches already
	  * instantiaed classes in Hashtable Cache_PostProcess.
	  * If the post process class name doesn't contain '.', 
	  * then NAMESPACE_POSTPROCESS is prefixed to the name. 
	  * @param Name The name of the post process to return. */
	protected PostProcess getPostProcessModule(String Name)
	{
		PostProcess rtr = null;
		if (Name.indexOf(".") < 0 )
			Name = NAMESPACE_POSTPROCESS +Name;
		//check for already loaded models
		rtr = (PostProcess)Cache_PostProcess.get(Name);
		if (rtr == null)
		{
			try
			{
				rtr = (PostProcess) Class.forName(Name).newInstance();
			}
			catch(Exception e)
			{
				System.err.println("Problem with postprocess named: "+Name+" : "+e);
				e.printStackTrace();
				return null;
			}
			Cache_PostProcess.put(Name, rtr);
		}
		return rtr;
	}
	/** Returns the post filter class named ModelName. Caches already
	  * instantiaed matching models in Hashtable Cache_PostFilter.
	  * If the matching model name doesn't contain '.',
	  * then NAMESPACE_POSTFILTER is prefixed to the name.
	  * @param Name The name of the post filter to return */
	protected PostFilter getPostFilterModule(String Name)
	{
		PostFilter rtr = null;
		if (Name.indexOf(".") < 0 )
			Name = NAMESPACE_POSTFILTER +Name;
		//check for already loaded post filters
		rtr = (PostFilter)Cache_PostFilter.get(Name);
		if (rtr == null)
		{
			try
			{
				rtr = (PostFilter) Class.forName(Name).newInstance();
			}
			catch(Exception e)
			{
				System.err.println("Problem with postprocess named: "+Name+" : "+e);
				return null;
			}
			Cache_PostFilter.put(Name, rtr);
		}
		return rtr;
	}
	
	/**
	 * Returns information about the weighting models and 
	 * the post processors used for the given search request.
	 * @param srq the search request for which we obtain 
	 *        the information.
	 * @return String information about the weighting models 
	 *         and the post processors used.
	 */
	public String getInfo(SearchRequest srq) {
		Request rq = (Request)srq; 
		StringBuffer info = new StringBuffer();
		
		//obtaining the weighting model information
		Model wmodel = getWeightingModel(rq.getWeightingModel());
		wmodel.setParameter(Double.parseDouble(rq.getControl("c")));
		info.append(wmodel.getInfo());
		
		//obtaining the post-processors information
		Hashtable controls = rq.getControlHashtable();
		
		for(int i=0; i<PostProcesses_Order.length; i++)
		{
			String PostProcesses_Name = PostProcesses_Order[i];
			for(int j=0; j<PostProcesses_Controls[i].length; j++)
			{
				String ControlName = PostProcesses_Controls[i][j];
				String value = (String)controls.get(ControlName);
				//System.err.println(ControlName+ "("+PostProcesses_Name+") => "+value);
				if (value == null)
					continue;
				value = value.toLowerCase();
				if(! (value.equals("off") || value.equals("false")))
				{
					info.append("_"+getPostProcessModule(PostProcesses_Name).getInfo());
					//we've now run this post process module, no need to check the rest of the controls for it.
					break;
				}
			}
		}		
		return info.toString();
	}
}
