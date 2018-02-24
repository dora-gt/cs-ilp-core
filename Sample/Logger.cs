using System;

namespace Sample
{
    public static class Logger
    {
        public static void Log(string log)
        {
            Console.WriteLine(log);
        }

        public static void Log(string log, params object[] args)
        {
            Console.WriteLine(string.Format(log, args));
        }
    }
}
