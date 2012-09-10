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
 * The Original Code is DocumentInitialWeightIndex.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Ben He <ben{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.structures.indexing;
import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import uk.ac.gla.terrier.structures.CollectionStatistics;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * This class implements a data structure for the term estimate (P_{avg})
 * of language modelling approach to IR.
 */
public class DocumentInitialWeightIndex {
	/** The array of term estimate for each term. It is sorted by termid. */
	protected double[] docWeights;
	
	protected String INDEX_FILENAME; 
	
	public File fDocumentInitialWeightIndex;
	
	//protected Lexicon lexicon;
	
	/**
	 * 
	 */
	public DocumentInitialWeightIndex() {
		INDEX_FILENAME = ApplicationSetup.TERRIER_INDEX_PATH
			+ ApplicationSetup.FILE_SEPARATOR
			+ ApplicationSetup.TERRIER_INDEX_PREFIX
			+ '.'
			+ ApplicationSetup.getProperty("dw.suffix", "dw");

		this.fDocumentInitialWeightIndex = new File(INDEX_FILENAME);
		this.docWeights = new double[(int)CollectionStatistics.getNumberOfDocuments()];
		if (this.fDocumentInitialWeightIndex.exists()){
			try{
				DataInputStream in = new DataInputStream(
					new BufferedInputStream(new FileInputStream(this.fDocumentInitialWeightIndex)));
				for (int i = 0; i < CollectionStatistics.getNumberOfDocuments(); i++){
					this.docWeights[i] = in.readDouble();
				}
				in.close();
			}
			catch(IOException ioe){
				ioe.printStackTrace();
				System.exit(1);
			}
		}
	}
	
	public void dumpDocumentInitialWeightIndex(){
		for (int i = 0; i < this.docWeights.length; i++)
			System.out.println(i + ": " + this.docWeights[i]);
	}
	
	public double getDocumentInitialWeight(int docid){
		return this.docWeights[docid];
	}
}
