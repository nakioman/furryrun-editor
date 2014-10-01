using System;
using System.Linq;

namespace FurryRun.Editor.Infrastructure
{
    public static class SerializableGenerics
    {
        private const string Of = "Of";

        /// <summary>
        /// Gets the key-value pair name
        /// </summary>
        /// <param name="tKey"></param>
        /// <param name="tValue"></param>
        /// <returns></returns>
        public static string GetKeyValuePairName(Type tKey, Type tValue)
        {
            // return key-value pair name
            return GetTypeName(tKey) + GetTypeName(tValue);
        }

        /// <summary>
        /// Returns the name of the type
        /// </summary>
        /// <param name="type">Type to generate the name from</param>
        /// <returns>The name of the type</returns>
        public static String GetTypeName(Type type)
        {
            String typeName = null;

            // Is type generic
            if (type.IsGenericType)
            {
                // Get type name - Generics 
                typeName = type.Name.Substring(0, type.Name.Length - 2);
                typeName += Of;

                // Get type's arguments
                Type[] types = type.GetGenericArguments();
                typeName = types.Aggregate(typeName, (current, t) => current + GetTypeName(t));
            }
            else if (type.IsArray)
            {
                // Compose array name as "ArrayOf" and get array element's type name
                if (type.BaseType != null) typeName = type.BaseType.Name;
                typeName += Of;
                typeName += GetTypeName(type.GetElementType());
            }
            else
            {
                // Append type's name
                typeName = type.Name;
            }

            // return type's name
            return typeName;
        }
    }
}