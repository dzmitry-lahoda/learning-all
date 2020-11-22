using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeLapseExp.WinFormsGUI
{
    internal class PresenterEventArgs:EventArgs
    {
        private object _tag;

        public PresenterEventArgs(object tag)
        {
            _tag = tag;
        }

        public object Tag
        {
            get { return _tag; }
        }
    }
}
