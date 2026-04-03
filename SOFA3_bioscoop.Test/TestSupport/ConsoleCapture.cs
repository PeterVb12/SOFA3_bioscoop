using System;
using System.IO;

namespace SOFA3_bioscoop.Test.TestSupport
{
    internal static class ConsoleCapture
    {
        private static readonly object Gate = new object();

        public static string Run(Action action)
        {
            lock (Gate)
            {
                TextWriter prev = Console.Out;
                var sw = new StringWriter();
                try
                {
                    Console.SetOut(sw);
                    action();
                }
                finally
                {
                    Console.SetOut(prev);
                }

                return sw.ToString();
            }
        }
    }
}
