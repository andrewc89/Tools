using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Tools.FileSystem
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class File
    {
        #region Constructor

        public File () { }

        //public File (string FileName) 
        //{
        //    this.FileName = FileName;
        //}

        //public File (string FilePath)
        //{
        //    this.FilePath = FilePath;
        //    this.FileName = FilePath.Contains('/') 
        //        ? FilePath.Substring(FilePath.LastIndexOf('/') + 1) : FilePath;
        //}

        #endregion

        #region Properties

        public string FileName { get; set; }

        public string FilePath { get; set; }

        #endregion

        #region Public Functions

        public bool Save ()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return false;
            }
            try
            {

            }
            catch (Exception e)
            {
                Debug.WriteLine("Tools.System.File.Save() encountered an error: " + e.Message);
                return false;
            }
            return false;
        }

        #endregion
    }
}
