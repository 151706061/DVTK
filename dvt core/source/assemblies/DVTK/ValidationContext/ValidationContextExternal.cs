using System;

namespace DicomValidationToolKit
{
    using System.Collections;
    using System.Text;
    /// <summary>
    /// Summary description for DicomValidationContext.
    /// </summary>
    public class DicomValidationContext
    {
        private DicomValidationContext()
        {
        }
        static Hashtable m_attributeSetHashTable = new Hashtable();
        static Hashtable m_sessionHashTable = new Hashtable();
        static public Hashtable AttributeSets
        {
            get { return m_attributeSetHashTable; }
        }
        static public string AttributeSetsToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry myEntry in m_attributeSetHashTable)
            {
                sb.AppendFormat("key:\t{0}\n", myEntry.Key.ToString());
            }
            return sb.ToString();
        }
        static public Hashtable Sessions
        {
            get { return m_sessionHashTable; }
        }
        static public string SessionsToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DictionaryEntry myEntry in m_sessionHashTable)
            {
                sb.AppendFormat("key:\t{0}\n", myEntry.Key.ToString());
            }
            return sb.ToString();
        }
    }
}
