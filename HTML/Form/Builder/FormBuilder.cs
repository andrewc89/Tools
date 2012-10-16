
namespace Tools.HTML.Form.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using Tools.HTML.Form.Builder.Element;

    /// <summary>
    /// builds out the actual form via reflection
    /// </summary>
    public class FormBuilder
    {
        #region Constructors

        public FormBuilder () { }

        public FormBuilder (Form Form)
        {
            this.Form = Form;
        }

        #endregion
        
        #region Properties

        public Form Form { get; set; }

        #endregion

        #region Construct

        /// <summary>
        /// constructs form, converts object properties into form elements
        /// </summary>
        public void Construct ()
        {
            foreach (var Property in this.Form.ClassType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
            {
                Type PropertyType = Property.PropertyType;

                if (!(PropertyType.GetGenericArguments().Count() > 0 && PropertyType.GetGenericArguments()[0].GetInterfaces().Contains(this.Form.InterfaceType)))
                {
                    if (PropertyType.GetInterfaces().Contains(this.Form.InterfaceType))
                    {
                        AddSelect(Property.Name);
                    }
                    else
                    {
                        AddInput(PropertyType, Property.Name);
                    }
                }
            }
        }

        /// <summary>
        /// constructs form, converts object properties into form elements
        /// adds values to form elements from given object
        /// to be used on forms for editing already existing objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Construct<T> (T obj)
        {
            foreach (var Property in this.Form.ClassType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
            {
                Type PropertyType = Property.PropertyType;

                if (!(PropertyType.GetGenericArguments().Count() > 0 && PropertyType.GetGenericArguments()[0].GetInterfaces().Contains(this.Form.InterfaceType)))
                {
                    if (PropertyType.GetInterfaces().Contains(this.Form.InterfaceType))
                    {
                        var PropertyValue = Property.GetValue(obj, null);
                        var ID = (long)PropertyValue.GetType().GetProperty("ID").GetValue(PropertyValue, null);
                        AddSelect(Property.Name, ID);
                    }
                    else
                    {
                        string Value = Property.GetValue(obj, null).ToString();
                        AddInput(PropertyType, Property.Name, Value);
                    }
                }
            }
        }
        
        #endregion

        #region Add Elements

        /// <summary>
        /// adds a select element to the form with no option selected
        /// </summary>
        /// <param name="Name">name of object property</param>
        public void AddSelect (string Name)
        {
            this.Form.Elements.Add(new Select(Name, "required"));
        }

        /// <summary>
        /// adds a select element to the form with an option selected
        /// based on the ID of the value of the property of the supplied object
        /// (obj.[property name].ID)
        /// </summary>
        /// <param name="Name">property name</param>
        /// <param name="ID">id of obj's property value</param>
        public void AddSelect (string Name, long ID)
        {
            this.Form.Elements.Add(new Select(Name, ID, "required"));
        }

        /// <summary>
        /// adds an input element to the form with or w/o a value specified
        /// </summary>
        /// <param name="PropertyType">property type</param>
        /// <param name="Name">property name</param>
        /// <param name="Value">property value (as string)</param>
        public void AddInput (Type PropertyType, string Name, string Value = "")
        {
            if (PropertyType.IsAssignableFrom(typeof(DateTime)))
            {
                this.Form.Elements.Add(new Input("text", Name, Value, "required", "Date"));
            }
            else if (PropertyType.IsAssignableFrom(typeof(Boolean)))
            {
                this.Form.Elements.Add(new Input("checkbox", Name, Value));
            }
            else
            {
                this.Form.Elements.Add(new Input("text", Name, Value, "required"));
            }
        }

        #endregion
    }
}
