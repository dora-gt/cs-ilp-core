using System;
namespace Org.Interledger
{
    public static class Objects
    {
        public static T RequireNonNull<T>(T value) where T : class
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }
            return value;
        }
    }
}
