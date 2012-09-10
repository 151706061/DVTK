package uk.ac.gla.terrier.utility;

/**
 * This class is used to compare Strings to determine their order 
 * for the lexicon
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0 
 */
public class Compare {
	
	/**
	 * Compare 2 strings for their order and takes numerals into account.
	 * Numbers are compared based on their value, not their lexicografic order; 
	 * @param string0 The first string
	 * @param string1 The second string
	 * @return the value 0 if the argument string is equal to this string based on the equals method of the class String; 
	 * a value less than 0 if this string is lexicographically less than the string argument;
	 * and a value greater than 0 if this string is lexicographically greater than the string argument. 
	 * Examples of ordering are:
	 * 1000 before hello
	 * 11 before 100
	 * william5th before william10th
	 * 12.54.745.100 before 12.54.1014.100
	 * 001 before 1
	 */	
	public static int compareWithNumeric(String string0, String string1){
		float floatValue0;
		float floatValue1;
		if (checkFloat(string0)){
			//The first is a float/integer
			floatValue0 = floatValue;
			if (checkFloat(string1)){
				//The second is also an integer
				floatValue1 = floatValue;
				if (floatValue0 != floatValue1)
					return (int)Math.signum(floatValue0 - floatValue1);
				else
					return string0.compareTo(string1);
			}
			//The second is not a float
			else return -1;
				
		} else if(checkFloat(string1))
			//First is not a float, second is a float
			return 1;
		
		return myCompare(string0, string1);
	}
	
	/**
	 * Compare 2 strings for their order and takes numerals into account.
	 * Numbers are compared based on their value, not their lexicografic order; 
	 * @param string0 The first string
	 * @param string1 The second string
	 * @return the value 0 if the argument string is equal to this string OR the numeric float values are equal ; 
	 * a value less than 0 if this string is lexicographically less than the string argument;
	 * and a value greater than 0 if this string is lexicographically greater than the string argument. 
	 */	
	public static int compareWithNumericWeek(String string0, String string1){
		
		float floatValue0;
		float floatValue1;
		if (checkFloat(string0)){
			//The first is a float/integer
			floatValue0 = floatValue;
			if (checkFloat(string1)){
				//The second is also an integer
				floatValue1 = floatValue;
				if (floatValue0 != floatValue1)
					return (int)Math.signum(floatValue0 - floatValue1);
				else
					return 0;				
			}
			//The second is not a float
			else return -1;
				
		} else if(checkFloat(string1))
			//First is not a float, second is a float
			return 1;
		
		return myCompare(string0, string1);
	}
	
	private static float floatValue;
	
	public static boolean checkFloat(String s){
		try{
			char[] chars = s.toCharArray();
			int size = chars.length;
			
			if( chars[0] == '+' || chars[0] == '-' || (chars[0] <= 57 && chars[0] >= 48)){
				//Integer possibility;
				int x=1;
				while(x < size){
					if ( (chars[x] > 57 || chars[x] < 48)){
						break;
					}
					x++;
				}
				if (x==size){
					floatValue = new Double(s).floatValue();
					return true;								
				} else if (chars[x] == '.'){
					//Float possibility
					x++;
					while(x < size){
						if ( (chars[x] > 57 || chars[x] < 48)){
							break;
						}
						x++;
					}
					if (x==size){
						floatValue = new Float(s).floatValue();
						return true;
					} else if (x==(size-1) && chars[x]=='d' || chars[x]=='f'){
						floatValue = new Float(s).floatValue();
						return true;
					}
					//TODO advanced float possibilities
				}
			} else if(chars[0] == '.'){
				//Float possibility
				int x =1;
				while(x < size){
					if ( (chars[x] > 57 || chars[x] < 48)){
						break;
					}
					x++;
				}
				if (x==size){
					floatValue = new Float(s).floatValue();
					return true;
				} else if (x==(size-1) && chars[x]=='d' || chars[x]=='f'){
					floatValue = new Float(s).floatValue();
					return true;
				}			
			}
			if (s.equals("Infinity")){
				floatValue = new Float(s).floatValue();
				return true;
			}
			if (s.equals("-Infinity")){
				floatValue = new Float(s).floatValue();
				return true;
			}			
			return false;
		} catch (NumberFormatException e){
			return false;
		}
	}
	
	private static int myCompare (String string0, String string1){
		//System.out.println("Checking with mycompare");
		int length0 = string0.length();
		int length1 = string1.length();
		
		int min = Math.min(length0, length1);
				
		int i=0;
		char ch0 = string0.charAt(i);
		char ch1 = string1.charAt(i);
		i++;
			
		while (ch0==ch1 && i<min){
			ch0 = string0.charAt(i);
			ch1 = string1.charAt(i);
			i++;
		}
		
		if (ch0!=ch1){
				//Check whether strings differ on numeric value
			if ( (ch0<'0' || '9'<ch0) && (ch1<'0' || '9'<ch1) )
				//Both not numbers.
				return ch0-ch1;
		
			else if ((ch0>='0' && '9'>=ch0) && (ch1>='0' && '9'>=ch1)){
				//Both numbers
				boolean notZero = false;
				String tmp0 = "";
				String tmp1 = "";
				
				//Check whether we already missed some number and look back
				int k =i-2;
				if(k>=0){
					char ch = string0.charAt(k);
					while(k>=0 && ch>='0' && ch<='9'){
						if(ch!='0'){						
							notZero = true;
							break;
						}
						ch = string0.charAt(k--);						
					}
				}		
				
				if (notZero){
					tmp0 = "1";
					tmp1 = "1";
				}
				//Build up first integer
				int j=i;
				while (j<length0 && (ch0>='0' && '9'>=ch0)){
					tmp0 += ch0;
					ch0 = string0.charAt(j++);										
				}
				
				if (j==length0 && (ch0>='0' && '9'>=ch0))
					tmp0 += ch0;
				
				//Build up second integer
				j=i;
				while (j<length1 && (ch1>='0' && '9'>=ch1)){
					tmp1 += ch1;
					ch1 = string1.charAt(j++);										
				}
				if (j==length1 && (ch1>='0' && '9'>=ch1))
					tmp1 += ch1;
				
				double int0 = new Double(tmp0).doubleValue();
				double int1 = new Double(tmp1).doubleValue();

				int sign = (int)Math.signum(int0-int1);
				if (sign != 0)
					return sign;
				else{
					//It should not be zero
					if (tmp0.length()==tmp1.length()){
						System.out.println("It still is zero!!! " + string0 + "," + string1 );
						//probably numbers are too big
						i=0;
						while(tmp0.charAt(i) == tmp1.charAt(i))
							i++;
					
						int0 = new Double(tmp0.substring(i)).doubleValue();
						int1 = new Double(tmp1.substring(i)).doubleValue();
						System.out.println("Now substracting " + int0 + " from " + int1 );
						sign = (int)Math.signum(int0-int1);
						return sign;
					}
					return string0.compareTo(string1);
				}					
			} 
			else if (ch0>='0' && '9'>=ch0){
				//first is char
				if (i>1 && string0.charAt(i-2)>='0' && '9'>=string0.charAt(i-2))
					return 1;
				else 
					return ch0-ch1;
					
			}
			else{
				//second is char
				if (i>1 && string0.charAt(i-2)>='0' && '9'>=string0.charAt(i-2))
					return -1;
				else 
					return ch0-ch1;
			}
			
		} else 
			return length0-length1;	
	}
}
