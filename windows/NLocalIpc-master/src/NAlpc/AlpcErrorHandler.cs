using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NAlpc.Tests
{
    public class AlpcErrorHandler
    {
        public static void Check(int status)
        {
            if (status != 0)
                throw new Win32Exception(status);
        }
    }
}
