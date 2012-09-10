package uk.ac.gla.terrier.utility;
/**
 * Simple quicksort class
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class QSort {

	    void sort(int a[], String b[], int lo0, int hi0){
		int lo = lo0;
		int hi = hi0;
		if (lo >= hi) {
		    return;
		}
		int mid = a[(lo + hi) / 2];
		while (lo < hi) {
		    while (lo<hi && a[lo] < mid) {
			lo++;
		    }
		    while (lo<hi && a[hi] > mid) {
			hi--;
		    }
		    if (lo < hi) {
			int T = a[lo];
			String S = b[lo];
			
			a[lo] = a[hi];
			a[hi] = T;
			
			b[lo] = b[hi];
			b[hi] = S;
		    }
		}
		if (hi < lo) {
		    int T = hi;
		    hi = lo;
		    lo = T;
		}
		sort(a,b, lo0, lo);
		sort(a,b, lo == lo0 ? lo+1 : lo, hi0);
	    }

	    public void sort(int a[], String b[]) {
		sort(a,b, 0, a.length-1);
	    }
}
