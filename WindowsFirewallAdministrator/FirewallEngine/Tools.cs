using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirewallEngine
{
    public class Tools
    {
        public static Dictionary<string, T> ParseType<T>()
        {
            var type = typeof(T);
            var names = type.GetEnumNames();
            var values = type.GetEnumValues();
            Dictionary<string, T> list = new Dictionary<string, T>();
            for (int i = 0; i < names.Length; i++)
                list.Add(names[i], (T)values.GetValue(i));
            return list;
        }

        public static Dictionary<string, object> ParseType(Type T)
        {
            var type = T;
            var names = type.GetEnumNames();
            var values = type.GetEnumValues();
            Dictionary<string, object> list = new Dictionary<string, object>();
            for (int i = 0; i < names.Length; i++)
                list.Add(names[i], values.GetValue(i));
            return list;
        }
    }
}
