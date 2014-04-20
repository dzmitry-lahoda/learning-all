using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using System.IO;

namespace EPAM.Trainings.Zadanie2_6
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("***** Showing attributes applaying to Messenger*****\n");
            Type t = typeof(Messenger<, >);
            object[] customAtts = t.GetCustomAttributes(false);
            Console.WriteLine("***** Attributes applied to {0} *****\n",t.ToString());
            foreach (Attribute v in customAtts)
                Console.WriteLine("-> {0}\n", v.ToString());
            Console.WriteLine("***************************************************\n");

            Console.WriteLine("***** Showing attributes *****\n");
            Assembly asm = Assembly.Load("Zadanie2.6");
            object[] types = asm.GetCustomAttributes(false);
            foreach (object atr in types)
            {
               Console.WriteLine(atr.ToString());
            }
            Console.WriteLine("***************************************************\n");

            CustomTypeViewer.Display("EPAM.Trainings.Zadanie2_6.MessengerEventArgs", "Zadanie2.6");

            Messenger<HashIndexedMessage, MessengerEventArgs> messenger = new Messenger<HashIndexedMessage, MessengerEventArgs>();
            Subscriber<HashIndexedMessage> receiver = new Subscriber<HashIndexedMessage>();
            String message;
            messenger.Sent += receiver.OnSend;
            message = Console.ReadLine();
            messenger.Send(new HashIndexedMessage(message, "1"));
            Console.ReadLine();
        }


                
    }
}
