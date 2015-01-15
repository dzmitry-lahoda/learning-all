using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIbyExample.Common
{
    public static class Assert
    {
        public static void NotNull(object obj)
        {
            if  (obj==null) {
                throw new ArgumentNullException("obj");
            }
        }
    }
}
