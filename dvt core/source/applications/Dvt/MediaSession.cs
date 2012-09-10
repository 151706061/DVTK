// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;

namespace Dvt
{
	/// <summary>
	/// Summary description for MediaSession.
	/// </summary>
	public class MediaSession
	{
		public MediaSession(string session_file)
		{
            this.media_session = session_file;
            this._media_files = new ArrayList ();
		}

        public string session_filename
        {
            get { return this.media_session; }
        }

        public ArrayList media_files
        {
            get { return this._media_files; }
        }

        private string      media_session;
        private ArrayList   _media_files;
	}
}
