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
 * The Original Code is BlockDirectIndexBuilder.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.structures.indexing;
import uk.ac.gla.terrier.structures.DocumentIndex;
import uk.ac.gla.terrier.structures.FilePosition;
import uk.ac.gla.terrier.structures.trees.BlockFieldDocumentTreeNode;
import uk.ac.gla.terrier.structures.trees.BlockTree;
import uk.ac.gla.terrier.structures.trees.BlockTreeNode;
import uk.ac.gla.terrier.structures.trees.FieldDocumentTreeNode;
/**
 * Builds a direct index using block and possibly field information.
 * @author Douglas Johnson &amp; Vassilis Plachouras &amp; Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class BlockDirectIndexBuilder extends DirectIndexBuilder {
	/**
	 * Constructs an instance of the class with 
	 * the given document index.
	 * @param docIndex The document index to be used
	 */
	public BlockDirectIndexBuilder(DocumentIndex docIndex) {
		super();
	}
	/**
	 * Constructs an instance of the class with
	 * the given document index. The underlying direct file
	 * has the given non-default filename.
	 * @param filename the non-default filename used for 
	 *        the underlying direct file.
	 */
	public BlockDirectIndexBuilder(String filename) {
		super(filename);
	}
	/**
	 * Adds a document to the direct index and returns the offset 
	 * in the direct index after adding the document. The document 
	 * is passed as an array of FieldDocumentTreeNode.
	 * @param terms FieldDocumentTreeNode[] the array of the 
	 *        document's terms.
	 * @return FilePosition the offset of the direct file after 
	 *         adding the document.
	 */
	public FilePosition addDocument(FieldDocumentTreeNode[] terms)
	{
		if (saveTagInformation) {
			System.out.println("I'm using taginformation!!!!");
			addFieldDocument((BlockFieldDocumentTreeNode[])terms);
		} else {
			System.out.println("I'm NOT using taginformation!!!!");
			addNoFieldDocument((BlockFieldDocumentTreeNode[])terms);
		}
		/* find out where we are */
		FilePosition rtr = getLastEndOffset();
		
		/* flush to disk if necessary */
		if (DocumentsSinceFlush++ >= DocumentsPerFlush)
		{
			flushBuffer();
			resetBuffer();
			DocumentsSinceFlush = 0;
		}
		/* and then return where the position of the last 
		 * write to the DirectIndex */
		return rtr;
	}
	/**
	 * Adds a document to the direct index with field information 
	 * and returns the offset in the direct index after adding 
	 * the document. The document is passed as an array 
	 * of FieldDocumentTreeNode.
	 * @param fieldTerms FieldDocumentTreeNode[] the array of 
	 *        the document's terms with field information.
	 * @return FilePosition the offset of the direct file after 
	 *         adding the document.
	 */
	private void addFieldDocument(FieldDocumentTreeNode[] fieldTerms) {
		BlockFieldDocumentTreeNode[] terms = 
			(BlockFieldDocumentTreeNode[])fieldTerms;
		final int termsCount = terms.length;
		
		BlockFieldDocumentTreeNode treeNode1 = terms[0];
		int termCode = treeNode1.getTermCode();
		file.writeGamma(termCode +1);
		file.writeUnary(treeNode1.frequency);
		System.out.println("The fieldscore is: " + treeNode1.getFieldScore());
		file.writeBinary(fieldTags, treeNode1.getFieldScore());
	
		BlockTree blockTree = treeNode1.blockTree;
		BlockTreeNode[] blockTreeNodes = blockTree.toArray();
		file.writeUnary(blockTreeNodes.length);
		
		BlockTreeNode blockTreeNode1 = blockTreeNodes[0];
		file.writeGamma(blockTreeNode1.blockId+1);
	
		int blockTreeNodesLength = blockTreeNodes.length;
		for (int i=1; i<blockTreeNodesLength; i++) {
			blockTreeNode1 = blockTreeNodes[i];
			file.writeGamma(blockTreeNode1.blockId - blockTreeNodes[i-1].blockId);
		}
		int prevTermCode = termCode;
		
		/* now write out the rest of the postings list to the DirectIndex */
		for(int TermNo = 1; TermNo < termsCount; TermNo++) {
			treeNode1 = terms[TermNo];
			termCode = treeNode1.getTermCode();
			file.writeGamma(termCode - prevTermCode);
			file.writeUnary(treeNode1.frequency);
			file.writeBinary(fieldTags, treeNode1.getFieldScore());
	
			blockTree = treeNode1.blockTree;
			blockTreeNodes = blockTree.toArray();
			file.writeUnary(blockTreeNodes.length);
			
			blockTreeNode1 = blockTreeNodes[0];
			file.writeGamma(blockTreeNode1.blockId+1);
			
			blockTreeNodesLength = blockTreeNodes.length;
			for (int i=1; i<blockTreeNodesLength; i++) {
				blockTreeNode1 = blockTreeNodes[i];
				file.writeGamma(blockTreeNode1.blockId - blockTreeNodes[i-1].blockId);
			}
			prevTermCode = termCode;
		}		
	}
	/**
	 * Adds a document to the direct index without field information 
	 * and returns the offset in the direct index after adding the 
	 * document. The document is passed as an array of FieldDocumentTreeNode.
	 * @param fieldTerms FieldDocumentTreeNode[] the array of the 
	 *        document's terms with field information.
	 * @return FilePosition the offset of the direct file after adding 
	 *         the document.
	 */
	private void addNoFieldDocument(FieldDocumentTreeNode[] fieldTerms) {
		BlockFieldDocumentTreeNode[] terms = 
			(BlockFieldDocumentTreeNode[])fieldTerms;
		final int termsCount = terms.length;
		/* write the first entry to the DirectIndex */
		BlockFieldDocumentTreeNode treeNode1 = terms[0];
		int termCode = treeNode1.getTermCode();
		file.writeGamma(termCode +1);
		file.writeUnary(treeNode1.frequency);
	
		BlockTree blockTree = treeNode1.blockTree;
		BlockTreeNode[] blockTreeNodes = blockTree.toArray();
		file.writeUnary(blockTreeNodes.length);
		
		BlockTreeNode blockTreeNode1 = blockTreeNodes[0];
		file.writeGamma(blockTreeNode1.blockId+1);
		
		int blockTreeNodesLength = blockTreeNodes.length;
		for (int i=1; i<blockTreeNodesLength; i++) {
			blockTreeNode1 = blockTreeNodes[i];
			file.writeGamma(blockTreeNode1.blockId - blockTreeNodes[i-1].blockId);
		
		}
		int prevTermCode = termCode;
		
		/* now write out the rest of the postings list to the DirectIndex */
		for(int TermNo = 1; TermNo < termsCount; TermNo++) {
			treeNode1 = terms[TermNo];
			termCode = treeNode1.getTermCode();
			file.writeGamma(termCode - prevTermCode);
			file.writeUnary(treeNode1.frequency);
	
			blockTree = treeNode1.blockTree;
			blockTreeNodes = blockTree.toArray();
			file.writeUnary(blockTreeNodes.length);
			
			blockTreeNode1 = blockTreeNodes[0];
			file.writeGamma(blockTreeNode1.blockId+1);
	
			blockTreeNodesLength = blockTreeNodes.length;
			for (int i=1; i<blockTreeNodesLength; i++) {
				blockTreeNode1 = blockTreeNodes[i];
				file.writeGamma(blockTreeNode1.blockId - blockTreeNodes[i-1].blockId);
	
			}
			prevTermCode = termCode;
		}		
	}
}
