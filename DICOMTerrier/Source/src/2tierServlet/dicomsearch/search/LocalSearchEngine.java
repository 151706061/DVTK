package dicomsearch.search;

import javax.ejb.EJBLocalObject;

public interface LocalSearchEngine extends 
  EJBLocalObject {
  public double calculateBonus(int multiplier, double 
    bonus);
}
