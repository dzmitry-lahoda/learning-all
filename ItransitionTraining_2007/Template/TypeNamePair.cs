using System;
using System.Collections.Generic;
using System.Text;

namespace Itransition.Training
{
    /// <summary>
    /// Describes the type and name pair of variable.
    /// </summary>
    public class TypeNamePair
    {
        private Type type;
        private String name;

        /// <summary>
        /// Greates new type and name pair
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public TypeNamePair(String name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        /// <summary>
        /// Gets type of variable
        /// </summary>
        public Type Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Gets name of variable
        /// </summary>
        public String Name
        {
            get
            {
                return name;
            }
        }
    }
}
