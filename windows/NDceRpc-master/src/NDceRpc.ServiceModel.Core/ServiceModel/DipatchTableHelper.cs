using System;
using System.Linq;
using System.Reflection;

namespace NDceRpc.ServiceModel
{
    internal static class DipatchTableHelper
    {
        public static MethodInfo[] GetAllImplementations<T>()
        {
            return GetAllImplementations(typeof (T));
        }

        public static MethodInfo[] GetAllImplementations(Type t)
        {
            var ofThis = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] ops =
                ofThis.Union(
                    t.GetInterfaces().SelectMany(x => x.GetMethods(BindingFlags.Public | BindingFlags.Instance)))
                      .ToArray();
            return ops;
        }

        public static MethodInfo[] GetAllServiceImplementations<T>()
        {
            return GetAllServiceImplementations(typeof (T));
        }
        public static MethodInfo[] GetAllServiceImplementations(Type t)
        {
            return GetAllImplementations(t).Where(AttributesReader.IsOperationContract).ToArray();
        }

   
    }
}