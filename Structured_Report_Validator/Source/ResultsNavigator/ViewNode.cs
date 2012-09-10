using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Dvtk.StructuredReportValidator.ResultsNavigator
{
    class ViewNode : TreeNode
    {
        private string path = string.Empty;

        
        /// <summary>
        /// Hide default constructor.
        /// </summary>
        private ViewNode()
        {
            // Do nothing.
        }


        public ViewNode(string path)
        {
            this.path = path;

            this.Name = System.IO.Path.GetFileNameWithoutExtension(this.path);
            this.Text = this.Name;
        }


        public string Path
        {
            get
            {
                return (this.path);
            }
        }

    }
}
