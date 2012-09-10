// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvt
{
	/// <summary>
	/// Summary description for DefinitionFile.
	/// </summary>
	public class DefinitionFile
	{
		public DefinitionFile(bool loaded,
                              string filename,
                              string sop_class_name,
                              string sop_class_uid,
                              string ae_title,
                              string ae_version,
                              string definition_root)
		{
			this.df_loaded = loaded;
            this.df_filename = filename;
            this.df_sop_class_name = sop_class_name;
            this.df_sop_class_uid = sop_class_uid;
            this.df_ae_title = ae_title;
            this.df_ae_version = ae_version;
            this.df_definition_root = definition_root;
		}

        public bool Loaded
        {
            get { return this.df_loaded; }
            set { this.df_loaded = value; }
        }
        public string Filename
        {
            get { return this.df_filename; }
            set { this.df_filename = value; }
        }
        public string SOPClassName
        {
            get { return this.df_sop_class_name; }
            set { this.df_sop_class_name = value; }
        }
        public string SOPClassUID
        {
            get { return this.df_sop_class_uid; }
            set { this.df_sop_class_uid = value; }
        }
        public string AETitle
        {
            get { return this.df_ae_title; }
            set { this.df_ae_title = value; }
        }
        public string AEVersion
        {
            get { return this.df_ae_version; }
            set { this.df_ae_version = value; }
        }
        public string DefinitionRoot
        {
            get { return this.df_definition_root; }
            set { this.df_definition_root = value; }
        }
		
        private bool    df_loaded;
        private string  df_filename;
        private string  df_sop_class_name;
        private string  df_sop_class_uid;
        private string  df_ae_title;
        private string  df_ae_version;
        private string  df_definition_root;
	}
}
