package uk.ac.gla.terrier.structures.indexing;

import java.io.BufferedOutputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.FileOutputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

import gnu.trove.TIntArrayList;
import gnu.trove.TIntHashSet;
import gnu.trove.TIntIntHashMap;
import gnu.trove.TObjectIntHashMap;
import gnu.trove.TIntIntIterator;
import gnu.trove.TObjectIntIterator;
import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.structures.CollectionStatistics;
import uk.ac.gla.terrier.structures.InvertedIndex;
import uk.ac.gla.terrier.structures.ComparableLexicon;
import uk.ac.gla.terrier.compression.BitFile;
import uk.ac.gla.terrier.utility.ApplicationSetup;

/**
 * This class builds up the inverted tag index for comparable terms
 * This way faster retrieval of exact matching and range querying can be provided.
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class InvertedTagBuilder {
	
	/**
	 * The underlying bit file.
	 */
	protected BitFile writeFile;
	/**
	 * Dataoutputstream.
	 */
	protected DataOutputStream dos = null;
	
	protected BufferedOutputStream bos;
	
	/**Set of root tags */
	TIntHashSet rootTagIds = new TIntHashSet();
	
	public InvertedTagBuilder(){
		try{
			writeFile = new BitFile(ApplicationSetup.INVERTED_TAG_FILENAME, "rw");
			bos = new BufferedOutputStream(new FileOutputStream(ApplicationSetup.TERMTAGLEXICON_FILENAME));
		} catch(FileNotFoundException e){
			//ignore
		}
	}
	
	public InvertedTagBuilder(String fileName1, String fileName2){
		try{
			writeFile = new BitFile(fileName1, "rw");
			bos = new BufferedOutputStream(new FileOutputStream(fileName2));
		} catch(FileNotFoundException e){
			//ignore
		}
		
	}
	
	/**Add a root tag to the set of root tags*/
	public void addRootTagId(int tagId){
		rootTagIds.add(tagId+1);
	}
	
	public void close(){
		try{
			writeFile.close();
			bos.close();
		} catch(IOException e){
			System.out.println();
		}
	}
	
	public int invertTags(String[] terms) {
		
		
		TObjectIntHashMap setOfTags = new TObjectIntHashMap(3000);
		int tagSetPointer = 1;
		String [] prefix = {"", "0", "00", "000", "0000", "00000", "000000"};
		String total, tmp;
		
		/**maximum length of a tag structure pattern*/
		int maxLength = 0;		
		
		int numOfTags = (int) Index.getIndex().getTagLexicon().getNumberOfTagLexiconEntries();
		
		InvertedIndex II = Index.getIndex().getComparableInvertedIndex();
		ComparableLexicon CL = Index.getIndex().getComparableLexicon();
		BitFile file = II.getBitFile();
		
		try{
			for (int q=0; q< terms.length; q++){
				
				ByteArrayOutputStream buffer = new ByteArrayOutputStream();
				dos = new DataOutputStream(buffer);
				CL.findTerm(terms[q]);
	
				TIntArrayList [][] invertedTags = new TIntArrayList[numOfTags][4];
				
				int termid = CL.getTermId();
				byte startBitOffset = CL.getStartBitOffset();
				long startOffset = CL.getStartOffset();
				byte endBitOffset = CL.getEndBitOffset();
				long endOffset = CL.getEndOffset();
				

				file.readReset(startOffset, startBitOffset, endOffset, endBitOffset);
				int prefDocId = -1;
				int docId;

				TIntArrayList curTagIds;
				
				while (((file.getByteOffset() + startOffset) < endOffset)
						|| (((file.getByteOffset() + startOffset) == endOffset) && (file
								.getBitOffset() < endBitOffset))) {
	
					//read documnent ID
					docId = file.readGamma() + prefDocId;
					prefDocId = docId;
					
					//read document frequency
					file.readUnary();
					
					//read number of fields 
					int tagfreq = file.readUnary()-1;
					
					//read the number of tags
					int[] tmp2 = new int[tagfreq];

					//Read the tag ids
					if(tagfreq>0)
						file.readGammas(tmp2);
					
					TIntIntHashMap codesMap = new TIntIntHashMap();
					int curNumberOfTags = tagfreq;
					int m=0;
					
					int [] curDocFreqs = new int[curNumberOfTags];
					int [] curNumOfTags = new int[curNumberOfTags];
					TIntArrayList [] curTagStructureIds = new TIntArrayList[curNumberOfTags];
					int curIndex = 0;
					
					while(m<curNumberOfTags){
						total = "";
						curTagIds = new TIntArrayList(tmp2.length);
						
						tmp = Integer.toString(tmp2[m]);
						total += prefix[5-tmp.length()]+ tmp;
						
						curTagIds.add(tmp2[m++]);
						
						while((m<curNumberOfTags) && ( !rootTagIds.contains(tmp2[m]+1)) ){
							tmp = Integer.toString(tmp2[m]);
							total += prefix[5-tmp.length()]+ tmp;
							curTagIds.add(tmp2[m++]);					
						}
						
						int tagStructureId = setOfTags.get(total);
						if (tagStructureId <1){
							tagStructureId = tagSetPointer;
							setOfTags.put(total, tagSetPointer++);
							if (total.length()> maxLength)
								maxLength = total.length();
						}
						
						for (int i=0; i<curTagIds.size(); i++){
							if (!rootTagIds.contains(curTagIds.get(i)+1)){
								
								int ind = codesMap.get(curTagIds.get(i))-1;
								if (ind > 0 ){
									curDocFreqs[ind]++;
									curNumOfTags[curIndex]++;
									curTagStructureIds[ind].add(tagStructureId);
								} else {
									curDocFreqs[curIndex] = 1;
									curNumOfTags[curIndex] = 1; 
									curTagStructureIds[curIndex] = new TIntArrayList();
									curTagStructureIds[curIndex].add(tagStructureId);
																		
									codesMap.put(curTagIds.get(i), curIndex+1);
									curIndex++;
								}								
							}
						}
					}

					TIntIntIterator ite = codesMap.iterator();
					for (int i= codesMap.size(); i>0; i--){
						ite.advance();
						int ind = ite.value()-1;
						int key = ite.key();
						if(invertedTags[key][0] == null){
							invertedTags[key][0] = new TIntArrayList();
							invertedTags[key][1] = new TIntArrayList();
							invertedTags[key][2] = new TIntArrayList();
							invertedTags[key][3] = new TIntArrayList();
						}
						invertedTags[key][0].add(docId);
						invertedTags[key][1].add(curDocFreqs[ind]);
						invertedTags[key][2].add(curNumOfTags[ind]);
						invertedTags[key][3].add(curTagStructureIds[ind].toNativeArray());

					}
				}
				
				writeFile.writeReset();
				for (int i = 0; i<numOfTags; i++){
					if (invertedTags[i][0]!=null && invertedTags[i][0].size()>0){
						String tagString = i+"";
						String term = termid +"";
						
						tagString = prefix[5-tagString.length()]+ tagString;
						term = prefix [7-term.length()] + term;
						int frequency = 0;
						dos.writeBytes(term+tagString);
						dos.writeInt(invertedTags[i][0].size());

						int[] tmpMatrix0 = invertedTags[i][0].toNativeArray();
						int[] tmpMatrix1 = invertedTags[i][1].toNativeArray();
						int[] tmpMatrix3 = invertedTags[i][2].toNativeArray();
						int[] tmpMatrix4 = invertedTags[i][3].toNativeArray();
						
						//write the first entry
						int docid = tmpMatrix0[0];
						writeFile.writeGamma(docid + 1);
						int termfreq = tmpMatrix1[0];
						frequency += termfreq;
						writeFile.writeUnary(termfreq);

						int tagfreq = tmpMatrix3[0];
						writeFile.writeUnary(tagfreq+1);
						int tagid;
						if (tagfreq>0){
							//Now write the sequence of tag identifiers
							tagid = tmpMatrix4[0];
							writeFile.writeGamma(tagid + 1);
							for (int l = 1; l < tagfreq; l++) {
								writeFile.writeGamma(tmpMatrix4[l] - tagid);
								tagid = tmpMatrix4[l];
							}
						}
						int tagindex = tagfreq;
						for (int k = 1; k < tmpMatrix0.length; k++) {
							writeFile.writeGamma(tmpMatrix0[k] - docid);
							docid = tmpMatrix0[k];
							termfreq = tmpMatrix1[k];
							frequency += termfreq;
							writeFile.writeUnary(termfreq);
							tagfreq = tmpMatrix3[k];
							writeFile.writeUnary(tagfreq+1);
							if (tagfreq>0){
								tagid = tmpMatrix4[tagindex];
								writeFile.writeGamma(tagid + 1);
								tagindex++;
								for (int l = 1; l < tagfreq; l++) {
									writeFile.writeGamma(tmpMatrix4[tagindex] - tagid);
									tagid = tmpMatrix4[tagindex];
									tagindex++;
								}
							}						
						}
						dos.writeInt(frequency);
						endOffset = writeFile.getByteOffset();
						endBitOffset = writeFile.getBitOffset();
						
						dos.writeLong(endOffset);
						dos.writeByte(endBitOffset);								
					}					
				}
				writeFile.writeFlush();
				buffer.writeTo(bos);				
			}
			file.close();
			
			
			TObjectIntIterator ite = setOfTags.iterator();
			String[] tagset = new String[setOfTags.size()];
			
			for(int i= setOfTags.size(); i>0; i--){
				ite.advance();
				tagset[ite.value()-1] = (String) ite.key();
			}
			
			BufferedOutputStream bos =
				new BufferedOutputStream(new FileOutputStream(ApplicationSetup.INVERTED_TAG_FILENAME + "id"));
			ByteArrayOutputStream buffer = new ByteArrayOutputStream();
			dos = new DataOutputStream(buffer);
			byte[] zeroBuffer =
				new byte[maxLength];
			
			String term;
			for (int i=0; i<tagset.length; i++){
				term = (String)tagset[i];
				dos.writeBytes(term);
				dos.write(
						zeroBuffer,
						0,
						maxLength - term.length());				
				
			}
			
			CollectionStatistics.addTagStructureMaxlength(maxLength);
			buffer.writeTo(bos);
			
		} catch (IOException e){
			System.err.println(e.getMessage());
			System.exit(1);
		}
		
		return 0;
	}	
}
