using System;
namespace Org.Interledger
{
    public static class Extensions
    {
        public static Type GetInterface(this Type type, Type interfaceType)
        {
            Objects.RequireNonNull(interfaceType);

            foreach (Type it in type.GetInterfaces())
            {
                if (interfaceType == it)
                {
                    return it;
                }
                if (interfaceType.IsGenericType &&
                    it.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == it.GetGenericTypeDefinition())
                {
                    return it;
                }
            }
            return null;
        }

        public static Type GetGenericArgument(this Type type, Type genericArgument)
        {
            Objects.RequireNonNull(genericArgument);

            foreach (Type it in type.GetGenericArguments())
            {
                if (genericArgument == it)
                {
                    return it;
                }
                if (genericArgument.IsGenericType &&
                    it.IsGenericType &&
                    genericArgument.GetGenericTypeDefinition() == it.GetGenericTypeDefinition())
                {
                    return it;
                }
            }
            return null;
        }
    }
}
