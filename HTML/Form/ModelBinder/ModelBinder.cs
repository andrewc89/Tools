
namespace Tools.HTML.Form.ModelBinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Reflection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ModelBinder : DefaultModelBinder
    {
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
                    if (PropertyType.FullName.StartsWith("Objects.Models"))
                    {
                        var Load = PropertyType.GetMethod("Load", BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Static, null, new Type[] { typeof(long) }, null);
                        var Value = Load.Invoke(new object(), new object[] { long.Parse(Form.GetValue(Property.Name + ".ID").AttemptedValue) });
                        Property.SetValue(Instance, Value, null);
                    }
                    else if (PropertyType.Equals(typeof(bool)))
                    {
                        if (Form.GetValue(Property.Name) == null)
                        {
                            Property.SetValue(Instance, false, null);
                        }
                        else if (Form.GetValue(Property.Name).Equals("on"))
                        {
                            Property.SetValue(Instance, true, null);
                        }
                    }
                    else
                    {
                        Property.SetValue(Instance, Convert.ChangeType(bindingContext.ValueProvider.GetValue(Property.Name).AttemptedValue, PropertyType), null);
                    }
                }
            }

            return Instance;
        }
    }
}
