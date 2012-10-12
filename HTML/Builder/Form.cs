
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.HTML.Builder.Element;
using System.Reflection;
using System.Collections;

namespace Tools.HTML.Builder
{
    /// <summary>
    /// constructs a form element from a given object
    /// use: Form.Create<Class,Interface>().ToHtml()
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

        #region Public Functions

        /// <summary>
        /// adds an Element of type IElement to the Form
        /// </summary>
        /// <param name="Element">IElement object</param>
        public void AddElement (IElement Element)
        {
            Elements.Add(Element);
        }

        /// <summary>
        /// adds an Element to the form
        /// </summary>
        /// <param name="ElementType"></param>
        /// <param name="Name"></param>
        public void AddElement (Type ElementType, string Name)
        {
            if (ElementType.GetInterfaces().Contains(this.InterfaceType))
            {
                this.Elements.Add(new Select(Name));
            }
            else
            {
                if (ElementType.IsAssignableFrom(typeof(DateTime)))
                {
                    this.Elements.Add(new Input("text", Name, "Date"));
                }
                else if (ElementType.IsAssignableFrom(typeof(Boolean)))
                {
                    this.Elements.Add(new Input("checkbox", Name));
                }
                else
                {
                    this.Elements.Add(new Input("text", Name));
                }
            }
        }

        public bool RemoveElement (string Name)
        {
            if (!this.Elements.Exists(x => x.Name.Equals(Name)))
            {
                return false;
            }
            this.Elements.RemoveAll(x => x.Name.Equals(Name));
            return true;
        }

        /// <summary>
        /// outputs form as html
        /// </summary>
        /// <returns>html string</returns>
        public string ToHtml ()
        {
            var Form = new StringBuilder();

            Form.AppendFormat("<form action='{0}' method='{1}'>\n\r\n\r", Action, Method);

            foreach (var Element in Elements)
            {
                Form.Append(Label.Get(((ElementBase)Element).Name, ((ElementBase)Element).Name));
                Form.Append(Element.ToHtml());
            }

            Form.Append("<input type='submit' value='Submit' />");

            Form.Append("</form>");

            return Form.ToString();
        }

        #endregion

        #region Private Functions

        private void Construct ()
        {
            foreach (var Property in this.ClassType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
            {
                Type PropertyType = Property.PropertyType;

                if (!(PropertyType.GetGenericArguments().Count() > 0 && PropertyType.GetGenericArguments()[0].GetInterfaces().Contains(this.InterfaceType)))
                {
                    AddElement(PropertyType, Property.Name);
                }
            }
        }

        #endregion

        #region Factories

        /// <summary>
        /// creates form elements using reflection
        /// </summary>
        /// <typeparam name="T">object to build form for</typeparam>
        /// <typeparam name="K">interface to recognize user-defined objects</typeparam>
        /// <returns>new Form object</returns>
        public static Form Create <T, K> () 
            where T : class 
            where K : class
        {
            var Temp = new Form();
            Temp.Action = "/" + typeof(T).Name + "/Create";
            Temp.Method = "post";
            Temp.ClassType = typeof(T);
            Temp.InterfaceType = typeof(K);            
            Temp.Elements = new List<IElement>();
            Temp.Construct();
            return Temp;
        }

        #endregion
    }
}
