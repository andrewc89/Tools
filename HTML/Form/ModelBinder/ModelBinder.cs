﻿
namespace Admin.Models.Form.ModelBinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Reflection;

    /// <summary>
    /// generic model binder
    /// </summary>
    public class ModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// bind form data to object
        /// </summary>
        /// <param name="controllerContext">controller context</param>
        /// <param name="bindingContext">binding context</param>
        /// <returns>form data-bound object</returns>
        public override object BindModel (ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var ModelType = bindingContext.ModelType;
            var Instance = Activator.CreateInstance(ModelType);
            var Form = bindingContext.ValueProvider;

            foreach (var Property in ModelType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance))
            {
                Type PropertyType = Property.PropertyType;

                if (!(PropertyType.GetGenericArguments().Count() > 0 ))
                {
                    if (!PropertyType.FullName.StartsWith("System"))
                    {
                        var Load = PropertyType.GetMethod("Load", BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Static, null, new Type[] { typeof(long) }, null);
                        var Value = Load.Invoke(new object(), new object[] { long.Parse(Form.GetValue(Property.Name + ".ID").AttemptedValue) });
                        if (Property.GetSetMethod() != null) Property.SetValue(Instance, Value, null);
                    }
                    else if (PropertyType.Equals(typeof(bool)))
                    {
                        if (Form.GetValue(Property.Name) == null)
                        {
                            if (Property.GetSetMethod() != null) Property.SetValue(Instance, false, null);
                        }
                        else //if (Form.GetValue(Property.Name).Equals("on"))
                        {
                            if (Property.GetSetMethod() != null) Property.SetValue(Instance, true, null);
                        }
                    }
                    else
                    {
                        if (Property.GetSetMethod() != null) Property.SetValue(Instance, Convert.ChangeType(bindingContext.ValueProvider.GetValue(Property.Name).AttemptedValue, PropertyType), null);
                    }
                }
            }

            return Instance;
        }
    }
}
