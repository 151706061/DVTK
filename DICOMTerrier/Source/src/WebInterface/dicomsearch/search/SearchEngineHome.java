package dicomsearch.search;

import java.rmi.RemoteException;
import javax.ejb.CreateException;
import javax.ejb.EJBHome;

public interface SearchEngineHome extends EJBHome {
		SearchEngine create() throws CreateException, RemoteException;		
}
	

