using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WalzExplorer.Common
{
    public static class ObjectLibrary
    {
        public static void SetValue(object o, string name, object value)
        {
           
            Type type = o.GetType();
            //if (type.GetMethod("set_" + name) != null)
            //{
                PropertyInfo prop = type.GetProperty(name);
                prop.SetValue(o, value);
            //}
            
        }
        public static object GetValue(object o, string name)
        {

            Type type = o.GetType();
            //if (type.GetMethod("get_" + name) != null)
            //{
                PropertyInfo prop = type.GetProperty(name);
                return prop.GetValue(o);
            //}

        }
        public static object CreateNewInstanace(object o)
        {
            //returns a new instance of that object (i.e. new insatnce of object with same object type)
            Type type = o.GetType();
            return Activator.CreateInstance(type);
        }

        public static object CreateObject (Type type)
        {
            return Activator.CreateInstance(type);
        }

        
    }
}
