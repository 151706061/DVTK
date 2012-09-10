package dicomsearch.search;

import java.rmi.RemoteException;
import javax.ejb.CreateException;
import javax.ejb.EJBHome;

public interface SearchEngine2Home extends EJBHome {
		SearchEngine2 create() throws CreateException, RemoteException;		
}
	

