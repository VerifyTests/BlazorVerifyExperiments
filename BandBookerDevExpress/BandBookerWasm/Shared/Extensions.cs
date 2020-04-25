using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BandBookerWasm.Shared
{
    public static class PropertyExtensions
    {
        
        static readonly ConcurrentDictionary<Tuple<Type, string>, PropertyInfo> cache =
            new ConcurrentDictionary<Tuple<Type, string>, PropertyInfo>();

        static PropertyInfo GetInfo(Type objectType, string propertyName)
        {
            var key = new Tuple<Type, string>(objectType, propertyName);
            PropertyInfo result = cache.GetOrAdd(key, (key) => objectType.GetProperty(propertyName));
            return result;
        }

        public static bool HasProperty(this object obj, string propertyName)
        {
            return GetInfo(obj.GetType(), propertyName) != null;
        }

        public static T GetPropertyValue<T>(this object obj, string propertyName)
        {            
            var propType = GetInfo(obj.GetType(), propertyName);
            var result = (T)propType?.GetValue(obj);
            return result;
        }

        public static void SetPropertyValue<T>(this object obj, string name, T value)
        {
            var propType = obj.GetType().GetProperty(name);
            propType.SetValue(obj, value);
        }
        public static void SetPropertyValue(this object obj, string name, object value)
        {
            var propType = obj.GetType().GetProperty(name);
            propType.SetValue(obj, value);
        }

        public static T AssignToObject<T>(this IDictionary<string, object> dictionary, T dataItem)
        {
            foreach (var field in dictionary.Keys)
            {
                if (dataItem.HasProperty(field))
                    dataItem.SetPropertyValue(field, dictionary[field]);
            }
            return dataItem;
        }
    }
}
