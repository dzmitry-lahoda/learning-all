using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dnHTM
{
    public class HtmNode:Node<Memory> 
    {
        public const int maximumNumberOfTemporalGroups = 5;

        private Algorithm _algortihm;

        public HtmNode(int number)
        {
            Number = number;
        }
        
   public List<Input> Inputs { get; set; }
        public Output Out { get; set; }
        public Level Level { get; set; }

 
        public void AddInput(Input input)
        {
            Inputs.Add(input);
        }



    }
}
