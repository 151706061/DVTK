using System;

namespace DicomValidationToolKit.IodId
{
    using Dvtk.Dimse;
    using DicomValidationToolKit;
    using DicomValidationToolKit.Sessions;

    /// <summary>
    /// IOD identifier forms an DVT application specific identifier that specifies the
    /// Information Object Class Definition to use during the validation of Dicom message exchange.
    /// </summary>
    /// <remarks>
    /// Each Information Object Class definition consists of a description of its purpose and 
    /// the Attributes which define it.
    /// </remarks>
    public class IodId
    {
        string id = new Guid().ToString();
        public override bool Equals(Object obj) 
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            IodId iodId = (IodId)obj;
            // Use Equals to compare instance variables.
            return id.Equals(iodId.id);
        }
        public override int GetHashCode() 
        {
            return id.GetHashCode();
        }
        public IodId(string id)
        {
            this.id = id;
        }
        public IodId(DimseCommand m_DimseCommand, System.String sopClassUID)
        {
            // TODO: determine IodId from definition component
        }
    }
}
