
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.HTML.Form.Builder.Element;
using System.Reflection;
using System.Collections;
using Tools.ViewModels.General;

namespace Tools.HTML.Form.Builder
{
    /// <summary>
    /// constructs a form element from a given object.
    /// use: FormFactory.Create&lt;Class,Interface&gt;().ToHtml() + chaining functions
    /// </summary>
    public class Form
    {
        #region Constructor

        public Form () { }

        public Form (Type ClassType, Type InterfaceType, string Method = "post")
        {            
            this.ClassType = ClassType;
            this.InterfaceType = InterfaceType;
            this.Action = "/" + ClassType.Name + "/Create";
            this.Method = Method;
            this.Elements = new List<IElement>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// form action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// form method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// form elements
        /// </summary>
        public List<IElement> Elements { get; set; }

        /// <summary>
        /// type of object specified for form
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        /// interface type for user-defined objects
        /// </summary>
        public Type InterfaceType { get; set; }

        #endregion

        #region Chaining Functions

        /// <summary>
        /// add options to a Select element specified by name
        /// </summary>
        /// <param name="ElementName">name of Select element to add options to</param>
        /// <param name="Options">list of SelectElement objects to add</param>
        /// <returns>this</returns>
        public Form AddSelectOptions (string ElementName, List<SelectElement> Options)
        {
            if (this.Elements.Exists(x => x.Name.Equals(ElementName)) && this.Elements.Find(x => x.Name.Equals(ElementName)) is Select)
            {
                var Element = (Select)this.Elements.Find(x => x.Name.Equals(ElementName));
                Element.AddOptions(Options);
            }
            return this;
        }

        /// <summary>
        /// allows for changing the Form's Action while chaining
        /// </summary>
        /// <param name="Action">string Action</param>
        /// <returns>this</returns>
        public Form SetAction (string Action)
        {
            this.Action = Action;
            return this;
        }

        /// <summary>
        /// allows for changing the Form's Method while chaining
        /// </summary>
        /// <param name="Method">string Method</param>
        /// <returns>this</returns>
        public Form SetMethod (string Method)
        {
            this.Method = Method;
            return this;
        }

        /// <summary>
        /// adds "required" class to all elements for jquery validation
        /// </summary>
        /// <returns>this</returns>
        public Form AllRequired ()
        {
            foreach (var Element in this.Elements)
            {
                Element.Classes.Add("required");
            }
            return this;
        }

        /// <summary>
        /// adds "required" class to specified element for jquery validation
        /// </summary>
        /// <param name="ElementName">Element name to match</param>
        /// <returns>this</returns>
        public Form IsRequired (string ElementName)
        {
            if (this.Elements.Exists(x => x.Name.Equals(ElementName)))
            {
                this.Elements.Find(x => x.Name.Equals(ElementName)).Classes.Add("required");
            }
            return this;
        }

        /// <summary>
        /// adds "required" class to specified elements for jquery validation
        /// </summary>
        /// <param name="Elements">Element names to match</param>
        /// <returns>this</returns>
        public Form AreRequired (List<string> Elements)
        {
            foreach (var ElementName in Elements)
            {
                IsRequired(ElementName);
            }
            return this;
        }

        /// <summary>
        /// removes any "required" classes from specified element
        /// </summary>
        /// <param name="ElementName">Element name to match</param>
        /// <returns>this</returns>
        public Form IsNotRequired (string ElementName)
        {
            if (this.Elements.Exists(x => x.Name.Equals(ElementName)))
            {
                this.Elements.Find(x => x.Name.Equals(ElementName)).Classes.RemoveAll(x => x.Equals("required"));
            }
            return this;
        }

        /// <summary>
        /// removes any "required" classes from specified elements
        /// </summary>
        /// <param name="Elements">Element names to match</param>
        /// <returns>this</returns>
        public Form AreNotRequired (List<string> Elements)
        {
            foreach (var ElementName in Elements)
            {
                IsNotRequired(ElementName);
            }
            return this;
        }

        #endregion

        #region ToHtml

        /// <summary>
        /// outputs form as html
        /// </summary>
        /// <returns>html string</returns>
        public string ToHtml ()
        {
            var Form = new StringBuilder();

            Form.AppendFormat("<form id='{0}-form' action='{1}' method='{2}'>\n\n", this.ClassType.Name.ToLower(), this.Action, this.Method);

            foreach (var Element in Elements)
            {
                Form.Append(Label.Get(Element.Name, Element.Name));
                Form.Append(Element.ToHtml());
            }

            Form.Append("<input type='submit' value='Submit' />");

            Form.Append("</form>");

            return Form.ToString();
        }        

        #endregion
    }
}
