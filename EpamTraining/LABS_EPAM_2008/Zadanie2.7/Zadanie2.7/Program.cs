using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Threading;

namespace EPAM.Trainings
{
    class Program
    {
        private static object synchronizationRoot=new Object();

        private delegate void SendMessage();

        private static void Send(object o)
        {
            ((Messenger<HashIndexedMessage, MessengerEventArgs>)o).Send(new HashIndexedMessage("Message from Timer", DateTime.Now.Ticks.ToString()));
        }

        static Random r = new Random();

        static void Main(string[] args)
        {
            Messenger<HashIndexedMessage, MessengerEventArgs> messenger= new Messenger<HashIndexedMessage, MessengerEventArgs>();

            Subscriber<HashIndexedMessage> subscriber = new Subscriber<HashIndexedMessage>();
            subscriber.messenger = messenger;
            System.Threading.Timer timer1 = new Timer(
                delegate 
                {
                    subscriber.Start();
                }
                , messenger, 0, 1234);

            ThreadPool.QueueUserWorkItem(delegate
            {
                while (true)
                {
                    subscriber.Start();
                    Thread.Sleep(r.Next(1000, 2000));
                }

            }
                );

            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    int count = messenger.list.Count-1;
                    
                    while (true)
                    {
                        if (messenger.list.Count > (count+1))
                        {
                            lock (synchronizationRoot)
                            {
                                count = messenger.list.Count - 1;
                                using (Finder finder = new Finder())
                                {
                                    finder.Write(messenger.list[count].Info);
                                }
                            }
                            Console.WriteLine("Messages detected {0}", messenger.list.Count);
                            Thread.Sleep(500);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("BUGAGA!");
                }

            }
            );
            Thread.Sleep(10000);
            Console.ReadLine();
        }


                
    }
}
