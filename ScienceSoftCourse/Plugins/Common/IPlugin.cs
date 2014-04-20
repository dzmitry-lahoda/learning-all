using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public interface IPlugin
    {
        void Move(String soure, String destination);
    }
}
