using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ConsoleApp2
{
    internal class StringHashTests
    {

        internal void Test()
        {
            var magazine1 = new string[] { "asd", "qwe", "zxc" };
            var note1 = new string[] { "zxc", "qwe" };
            Assert.True(subset(note1, magazine1));

            var magazine2 = new string[] { "asd", "qwe", "zxc" };
            var note2 = new string[] { "rty", "qwe" };
            Assert.False(subset(note2, magazine2));

            var magazine3 = new string[] { "asd", "qwe", "zxc" };
            var note3 = new string[] { "qwe", "qwe" };
            Assert.False(subset(note3, magazine3));

            var magazine4 = new string[] { "qwe", "qwe", "qwe" };
            var note4 = new string[] { "qwe", "qwe" };

            Assert.True(subset(note4, magazine4));
        }

        private bool subset(string[] items, string[] of)
        {
            var dic = new Dictionary<string, int>(of.Length);
            for (int i = 0; i < of.Length; i++)
            {
                var v = of[i];
                var a = 1;
                if (dic.TryGetValue(v, out a))
                {
                    dic[v] = a + 1;
                }
                else
                    dic.Add(of[i], 1);
            }


            for (int i = 0; i < items.Length; i++)
            {
                var v = items[i];
                if (dic.ContainsKey(v))
                {
                    var nw = dic[v] - 1;
                    if (nw < 0) return false;
                    dic[v] = nw;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private bool subsetRepeat(string[] items, string[] of)
        {
            var found = 0;
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 0; j < of.Length; j++)
                {
                    if (items[i] == of[j])
                    {
                        found++;
                        continue;
                    }
                }
            }

            return found == items.Length;
        }
    }
}