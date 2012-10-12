
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.ViewModels.General;

namespace Tools.HTML.Builder.Element
{
    /// <summary>
    /// select element
    /// </summary>
    public class Select : ElementBase, IElement
    {
        #region Constructor

        public Select () { }

        /// <summary>
        /// new Select element
        /// </summary>
        /// <param name="Name">select name</param>
        /// <param name="Classes">select classes</param>
        public Select (string Name, params string[] Classes)
        {
            this.Name = Name;
            this.Classes = Classes.ToList();
            this.Classes.Add(Name);
            this.Options = new List<SelectElement>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// list of select options
        /// </summary>
        public List<SelectElement> Options { get; set; }

        #endregion

        #region Public Functions

        public void addOption (long ID, string DisplayName)
        {
            this.Options.Add(new SelectElement(ID, DisplayName));
        }

        public void addOption (SelectElement Option)
        {
            this.Options.Add(Option);
        }

        public void addOptions (List<SelectElement> Options)
        {
            this.Options.AddRange(Options);
        }

        /// <summary>
        /// converts Select element to html string
        /// </summary>
        /// <returns>html representation of Select element</returns>
        public override string ToHtml ()
        {
            var String = new StringBuilder();
                        
            String.AppendFormat("<select name='{0}'>\n", this.Name);
            String.Append("<option value=''>Select...</option>");

            foreach (var Option in Options)
            {
                String.AppendFormat("<option value='{0}'>{1}</option>\n", Option.ID, Option.DisplayName);
            }

            String.Append("</select><br /> <br />\n\n");

            return String.ToString();
        }

        #endregion
    }
}
