using System;
using System.Collections;

namespace DvtkApplicationLayer
{
	/// <summary>
	/// Summary description for MediaFile.
	/// </summary>
	public class MediaFile : PartOfSession
	{	private string mediaFileName; 
		private IList resultmediaFiles = new ArrayList();
        /// <summary>
        /// Constructor.
        /// </summary>
		public MediaFile(Session session, String mediaFileName): base(session){
            this.mediaFileName = mediaFileName;
		}
        /// <summary>
        /// Name of mediaFile.
        /// </summary>
		public string MediaFileName {
			get {
			return mediaFileName ;}
			set {
				mediaFileName = value;}
		}
        /// <summary>
        /// Represents a collection of results for a media file.
        /// </summary>
		public IList Result{
			get 
			{ 
			return resultmediaFiles ;}
			set 
			{
				resultmediaFiles = value;
			}
		}
	}
}
