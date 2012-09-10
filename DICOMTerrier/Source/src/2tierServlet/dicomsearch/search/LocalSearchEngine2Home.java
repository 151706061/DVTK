package dicomsearch.search;

import javax.ejb.EJBLocalHome;
import javax.ejb.CreateException;

public interface LocalSearchEngine2Home extends 
  EJBLocalHome {
	LocalSearchEngine2 create() throws 
          CreateException;
}
