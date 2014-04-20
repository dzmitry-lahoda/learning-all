using System;
using System.Collections.Generic;
using System.Text;

namespace Itransition.Training
{
    public class TemplateException:Exception
    {
        /// <summary>
        /// Creates new TemplateException.
        /// </summary>
        /// <param name="input">String wich was given to template.</param>
        /// <param name="code">String returned after parsing</param>
        public TemplateException(String input, String code):base("Compilation failed.")
        {
            this.input = input;
            this.code = code;
        }

        private String input = String.Empty;
        private String code = String.Empty;

        /// <summary>
        /// Gets input string
        /// </summary>
        public String Input
        {
            get
            {
                return input;
            }
        }

        /// <summary>
        /// Gets parsed string
        /// </summary>
        public String Code
        {
            get
            {
                return code;
            }
        }


    }
}
