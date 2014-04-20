using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Itransition.Training
{
    /// <summary>
    /// Provides method that creates unique identifier for local machine. 
    /// </summary>
    public static class UniqueIdentifier
    {
        private static object synhronizationRoot=new object();
         
        private static int counter = 0;       
 
        /// <summary>
        /// Returns unique identifier for local machine. 
        /// </summary>
        /// <returns>Unique identifier.</returns>
        public static String GetNewIdentifier()
        {
            lock(synhronizationRoot)
            {
                counter++;
                return String.Format("{0}-{1}-{2}-{3}-{4}",
                    Process.GetCurrentProcess().Id,
                    AppDomain.CurrentDomain.Id,
                    Thread.CurrentThread.ManagedThreadId,
                    DateTime.Now.Ticks,
                    counter
                    );
            };
        }
    }
}
