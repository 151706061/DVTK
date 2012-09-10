package uk.ac.gla.terrier.structures.indexing;

import uk.ac.gla.terrier.structures.FilePosition;
import uk.ac.gla.terrier.structures.trees.FieldDocumentTreeNode;
import uk.ac.gla.terrier.structures.trees.DICOMFieldDocumentTreeNode;
import java.util.ArrayList; //TODO Create own class for this

/**
 * Builds a direct index for DICOM data
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class DirectDICOMIndexBuilder extends DirectIndexBuilder {
	
	public DirectDICOMIndexBuilder(String path){
		super(path);
	}
	
	/**
	 * Adds a document in the direct index, using structure information
	 * The addition of the document's terms in the data structure is handled 
	 * by the private methods addFieldDocument 
	 * @param terms FieldDocumentTreeNode[] the array that contains the 
	 *        document's terms to index.
	 * @return FilePosition the offset in the direct index after adding the
	 *         terms.
	 */
	public FilePosition addDocument(FieldDocumentTreeNode[] terms)
	{
		addFieldDocument((DICOMFieldDocumentTreeNode[])terms);
				
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
	 * Adds a document in the direct index, using field information.
	 * @param terms DICOMFieldDocumentTreeNode[] the array that contains the 
	 *        document's terms to index.
	 * @return FilePosition the offset in the direct index after adding the
	 *         terms.
	 */
	private void addFieldDocument(DICOMFieldDocumentTreeNode[] terms) {

		//System.out.println("Writing next document");
		DICOMFieldDocumentTreeNode treeNode1 = terms[0];
		/* write the first entry to the DirectIndex */
		//System.out.println("Writing termcode gamma: " + treeNode1.termCode);
		int termCode = treeNode1.getTermCode();
		file.writeGamma(termCode+1);
		//System.out.println("Writing termfrequency unary : " + treeNode1.frequency);
		file.writeUnary(treeNode1.frequency);
		
		/*write field information*/
		int numberOfFields = treeNode1.numberOfFieldIds();
		//System.out.println("Writing number of fields unary : " + numberOfFields);
		file.writeUnary(numberOfFields+1);
		
		if(numberOfFields>0){
			ArrayList fieldList = treeNode1.getFieldIdList();//TODO create dedicated class
			int [] fields = new int[fieldList.size()];
			for (int i=0; i<fields.length;i++)
				fields[i] = ((Integer)fieldList.get(i)).intValue();
			//Arrays.sort(fields);		
			
			int tagCode = fields[0];
			//System.out.println("Writing tagcode gamma : " + tagCode);
			file.writeGamma(tagCode+1);
			int prevTagCode = tagCode;			
			
			for (int i = 1; i<numberOfFields; i++){
				tagCode = fields[i];
				//System.out.println("Writing tagcode - prevTagCode gamma : " + (tagCode-prevTagCode));
				file.writeGamma(tagCode - prevTagCode);
				prevTagCode = tagCode;
			}
		}
				
		int prevTermCode = termCode;
		
		final int length = terms.length;
		if (length > 1) {
			for (int termNo = 1; termNo < length; termNo++) {
				treeNode1 = terms[termNo];
				//System.out.println("Writing termcode - prevTermCode gamma " +  (treeNode1.termCode-prevTermCode));
				termCode = treeNode1.getTermCode();
				file.writeGamma(termCode - prevTermCode);
				//System.out.println("Writing termfrequency unary : " + treeNode1.frequency);
				file.writeUnary(treeNode1.frequency);
				
				numberOfFields = treeNode1.numberOfFieldIds();
				//System.out.println("Writing number of fields unary : " + numberOfFields);
				file.writeUnary(numberOfFields+1);
				
				if(numberOfFields>0){
					ArrayList fieldList = treeNode1.getFieldIdList();//TODO create dedicated class
					int [] fields = new int[fieldList.size()];
					for (int i=0; i<fields.length;i++)
						fields[i] = ((Integer)fieldList.get(i)).intValue();
					//Arrays.sort(fields);					
					
					int tagCode = fields[0];
					//System.out.println("Writing tagcode gamma : " + tagCode);
					file.writeGamma(tagCode+1);
					int prevTagCode = tagCode;
				
					for (int i = 1; i<numberOfFields; i++){
						tagCode = fields[i];
						//System.out.println("Writing tagcode - prevTagCode gamma : " + (tagCode-prevTagCode));
						file.writeGamma(tagCode - prevTagCode);
						prevTagCode = tagCode;
					}						
				}
								
				prevTermCode = termCode;
			}
		}	
	}	
}
