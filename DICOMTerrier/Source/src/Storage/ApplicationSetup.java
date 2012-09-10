import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Properties;

/**
 * This class provides the configuration of the storage tool
 * It reads and stores the configuration
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class ApplicationSetup {

	private static Properties appProperties;
	
	static {
		try {
			appProperties = new Properties();
			FileInputStream in = new FileInputStream("storage.properties");
			appProperties.load(in);
			in.close();
		} catch (FileNotFoundException e) {
			System.err.println(e.getMessage());
		} catch (IOException e) {
			System.err.println(e.getMessage());
		}
	}
	
	/**
	 * Read a property
	 * @param key
	 * @param defaultValue
	 * @return
	 */
	public static String getProperty(String key, String defaultValue) {
		return appProperties.getProperty(key, defaultValue);
	}

	/**
	 * Set a property
	 * @param key
	 * @param value
	 */
	public static void setProperty(String key, String value) {
		appProperties.setProperty(key, value);
	}

	/**
	 * Save the properties to file
	 *
	 */
	public static void saveProperties() {
		try {
			FileOutputStream out = new FileOutputStream("storage.properties");
			appProperties.store(out, null);
			out.close();
		} catch (FileNotFoundException e) {
			System.err.println(e.getMessage());
		} catch (IOException e) {
			System.err.println(e.getMessage());
		}
	}
}
