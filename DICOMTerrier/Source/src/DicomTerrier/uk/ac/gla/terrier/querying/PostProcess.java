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
 * The Original Code is PostProcess.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.querying;
/** PostProccess are designed to complement PostFilters. While PostProcesses
  * operate on the entire resultset at once, with PostFilters, each PostFilter
  * is called for each result in the resultset. PostProcesses can operate on the entire
  * resultset.
  * <B>Properties</B>
  * <ul>
  * <li><tt>querying.postprocesses.controls</tt> : A comma separated list of control to PostProcess
  * class mappings. Mappings are separated by ":". eg <tt>querying.postprocess.controls=qe:QueryExpansion</tt></li>
  * <li><tt>querying.postprocesses.order</tt> : The order postproceses should be run in</li></ul>
  * '''NB:''' Initialisation and running of post processes is carried out by the Manager.
  * @author Craig Macdonald
  * @version $Revision: 1.1 $
  */
public interface PostProcess
{
	/** Run the instantiated post process on the search request represented by q.
	  * @param manager The manager instance handling this search session.
	  * @param q the current query being processed
	  */
	public void process(Manager manager, SearchRequest q);
	
	/**
	 * Returns the name of the post processor.
	 * @return String the name of the post processor.
	 */
	public String getInfo();
}