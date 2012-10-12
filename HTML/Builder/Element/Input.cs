
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.HTML.Builder.Element
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
        public Input (string Type, string Name, params string[] Classes)
        {
            this.Type = Type;
            this.Name = Name;
            this.Classes = Classes.ToList();
            this.Classes.Add(Name);
        }

        #endregion

        #region Properties

        /// <summary>
        /// input type (checkbox, text, etc.)
        /// </summary>
        public string Type { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// converts Input to html string
        /// </summary>
        /// <returns>html representation of Input element</returns>
        public override string ToHtml ()
        {
           return String.Format("<input type='{0}' name='{1}' class='{2}' /><br /><br />\n\n", this.Type, this.Name, string.Join(" ", Classes));
        }

        #endregion
    }
}
