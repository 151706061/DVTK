package uk.ac.gla.terrier.utility;

import gnu.trove.TIntArrayList;

/**
 * This class is used to merge pointers from the normal inverted index and the comparable inverted index
 * into one list of postings.
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class MergePointers {

	/**
	 * Merge two pointers matrices 
	 * When docids differ they are simply added, when they are equal their
	 * statistics are merged. 
	 * @param p1 The first matrix
	 * @param p2 The second matrix
	 * @return The merged matrix
	 */
	public static int[][] merge (int [][] p1, int [][] p2){
		
		int[][] temp = new int[4][];
		
		int length1 = p1[0].length;
		int length2 = p2[0].length;
		int totallength = length1 + length2;
		TIntArrayList ar0 = new TIntArrayList(totallength);
		TIntArrayList ar1 = new TIntArrayList(totallength);
		TIntArrayList ar2 = new TIntArrayList(totallength);
		TIntArrayList ar3 = new TIntArrayList(totallength*4);
			
		if (p1[0][length1-1] > p2[0][length2-1]){
			int i=0,j=0, tagindexi=0, tagindexj=0;
			while(i<length1 && j<p2[0].length){
				if (p1[0][i] < p2[0][j]){
					ar0.add(p1[0][i]);
					ar1.add(p1[1][i]);
					ar2.add(p1[2][i]);
					for (int k=0; k<p1[2][i]; k++)
						ar3.add(p1[3][tagindexi++]);
					i++;
				}
				else if (p1[0][i] > p2[0][j]){
					ar0.add(p2[0][j]);
					ar1.add(p2[1][j]);
					ar2.add(p2[2][j]);
					for (int k=0; k<p2[2][j]; k++)
						ar3.add(p2[3][tagindexj++]);
					j++;
				}
				else{//equal so merge
					ar0.add(p1[0][i]);
					ar1.add(p1[1][i] + p2[1][j]);
					ar2.add(p1[2][i] + p2[2][j]);
					for (int k=0; k<p1[2][i]; k++)
						ar3.add(p1[3][tagindexi++]);
					for (int k=0; k<p2[2][j]; k++)
						ar3.add(p2[3][tagindexj++]);
					i++; j++;					
				}
			}
			while (i<length1){
				ar0.add(p1[0][i]);
				ar1.add(p1[1][i]);
				ar2.add(p1[2][i]);
				for (int k=0; k<p1[2][i]; k++)
					ar3.add(p1[3][tagindexi++]);
				i++;
			}			
		}
		else{
			int i=0,j=0, tagindexi=0, tagindexj=0;
			while(i<length1 && j<length2){
				if (p1[0][i] < p2[0][j]){
					ar0.add(p1[0][i]);
					ar1.add(p1[1][i]);
					ar2.add(p1[2][i]);
					for (int k=0; k<p1[2][i]; k++)
						ar3.add(p1[3][tagindexi++]);
					i++;
				}
				else if (p1[0][i] > p2[0][j]){
					ar0.add(p2[0][j]);
					ar1.add(p2[1][j]);
					ar2.add(p2[2][j]);
					for (int k=0; k<p2[2][j]; k++)
						ar3.add(p2[3][tagindexj++]);
					j++;
				}
				else{//equal so combine
					ar0.add(p1[0][i]);
					ar1.add(p1[1][i] + p2[1][j]);
					ar2.add(p1[2][i] + p2[2][j]);
					for (int k=0; k<p1[2][i]; k++)
						ar3.add(p1[3][tagindexi++]);
					for (int k=0; k<p2[2][j]; k++)
						ar3.add(p2[3][tagindexj++]);
					i++; j++;					
				}
			}
			while(j<length2){
				ar0.add(p2[0][j]);
				ar1.add(p2[1][j]);
				ar2.add(p2[2][j]);
				for (int k=0; k<p2[2][j]; k++)
					ar3.add(p2[3][tagindexj++]);
				j++;
			}				
		}		
		temp[0] = ar0.toNativeArray(); 
		temp[1] = ar1.toNativeArray();
		temp[2] = ar2.toNativeArray();
		temp[3] = ar3.toNativeArray();		
		return temp;
	} 
	
	
	/**
	 * Simple test case to demonstrate the mechanism
	 * @param args
	 */
	public static void main (String args[]){
		int [][] p1 = { 
				{1,2,4,6,13},
				{1,2,5,1,1},
				{3,2,3,3,2},
				{1,2,3,4,6,7,8,4,6,5,7,8,9}				
		};
		int [][] p2 = { 
				{3,4,6,13},
				{1,2,5,1},
				{3,2,3,3},
				{1,2,3,4,6,7,8,4,6,5,7}				
		};
		
		int[][] tmp = merge (p1,p2);
		
		for(int i=0; i<tmp.length; i++){
			System.out.print("{ ");
			for(int j=0; j<tmp[i].length; j++){
				System.out.print(tmp[i][j] + ", ");
			}
			System.out.println("}");
		}
	}	
}

