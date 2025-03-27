using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrustructure_Layer.Services
{
    public class TrimWhitespace
    {
        public void trim(object instance)
        {
            if (instance != null)
            {
                var props = instance.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        // Ignore indexers
                        .Where(prop => prop.GetIndexParameters().Length == 0)
                        // Must be both readable and writable
                        .Where(prop => prop.CanWrite && prop.CanRead);

                foreach (PropertyInfo prop in props)
                {
                    if (instance is IEnumerable)
                    {
                        foreach (var item in (IEnumerable)instance)
                        {
                            trim(item);
                        }
                    }
                    else if (prop.GetValue(instance, null) is string)
                    {
                        string value = (string)prop.GetValue(instance, null);
                        if (value != null)
                        {
                            value = value.Trim();
                            prop.SetValue(instance, value, null);
                        }
                    }
                    else
                        trim(prop.GetValue(instance, null));
                }
            }
        }
    }
}
