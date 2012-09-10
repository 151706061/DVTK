package dicomsearch.search;

import javax.ejb.EJBLocalHome;
import javax.ejb.CreateException;

public interface LocalSearchEngineHome extends 
  EJBLocalHome {
	LocalSearchEngine create() throws 
          CreateException;
}
