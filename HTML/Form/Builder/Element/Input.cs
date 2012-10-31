
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Admin.Models.Form.Builder.Element
{
    /// <summary>
    /// input element
    /// </summary>
    public class Input : ElementBase, IElement
    {
        #region Constructor

        public Input () { }

        /// <summary>
        /// new Input element
        /// </summary>
        /// <param name="Type">input type (checkbox, text, etc.)</param>
        /// <param name="Name">input name (for postback)</param>
        /// <param name="Classes">input classes</param>
        public Input (string Type, string Name, string Value = "", params string[] Classes)
        {
            this.Type = Type;
            this.Name = Name;
            this.Value = Value;
            this.Classes = Classes.ToList();
            this.Classes.Add(Name);
        }

        #endregion

        #region Properties

        /// <summary>
        /// input type (checkbox, text, etc.)
        /// </summary>
        public string Type { get; set; }

        public string Value { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// converts Input to html string
        /// </summary>
        /// <returns>html representation of Input element</returns>
        public override string ToHtml ()
        {
            if (this.Type.Equals("checkbox"))
            {
                string Checked = (this.Value.ToLower().Equals("true")) ? "checked='checked'" : "";
                return String.Format("<input type='{0}' name='{1}' {2} class='{3}' /><br /><br />\n\n", this.Type, this.Name, Checked, string.Join(" ", Classes.RemoveAll(x => x.Equals("required"))));
            }
            else if (this.Type.Equals("hidden"))
            {
                return String.Format("<input type='{0}' name='{1}' value='{2}' class='{3}' />\n\n", this.Type, this.Name, this.Value, string.Join(" ", Classes));
            }
            else
            {
                return String.Format("<input type='{0}' name='{1}' value='{2}' class='{3}' /><br /><br />\n\n", this.Type, this.Name, this.Value, string.Join(" ", Classes));
            }
        }

        #endregion
    }
}
