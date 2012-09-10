package dicomsearch.search;

import javax.ejb.EJBObject;
import java.rmi.RemoteException;

public interface SearchEngine2 extends EJBObject {
  public int create() throws RemoteException;
  public Results processQuery(String query, String filterString, double cParameter, boolean sort, int start) throws RemoteException;
  public void close() throws RemoteException;
}
