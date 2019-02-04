using System;
using System.Collections.Generic;
using System.Text;

namespace WIMSystem.Utils
{
    class Reflection
    {
        public static object ReflectPropertyValue(object source, string property)
        {
            return source.GetType().GetProperty(property).GetValue(source, null);
        }
    }
}
