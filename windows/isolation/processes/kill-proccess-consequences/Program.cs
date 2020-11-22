using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace kill_proccess_consequences
{
    class Program
    {
        static void Main(string[] args)
        {
            var name = typeof (Program).FullName;
            var fname = name + ".txt";
            var mname = name.Replace(".", "/");
            Mutex m = OpenExistingOrCreateMutex(mname);
            try
            {
                m.WaitOne();
            }
            catch (AbandonedMutexException)
            {
                File.Delete(fname);
            }
            try
            {
                FileStream fileStream = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                for (byte i = 0; i < byte.MaxValue; i++)
                {
                    fileStream.WriteByte(i);
                    Thread.Sleep(100);
                    if (i % 10 == 0) fileStream.Flush();
                }
            }
            catch
            {
                m.ReleaseMutex();
            }

        }


        // make debugger to step through exceptional driven code
        [DebuggerStepperBoundary]
        [DebuggerStepThrough]
        public static Mutex OpenExistingOrCreateMutex(string mutexName)
        {
            try
            {
                return Mutex.OpenExisting(mutexName, System.Security.AccessControl.MutexRights.Synchronize);
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                return new Mutex(false, mutexName);
            }
        }
    }


}
