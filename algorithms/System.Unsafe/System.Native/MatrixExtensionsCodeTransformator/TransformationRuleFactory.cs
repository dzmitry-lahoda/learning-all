using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixExtensionsCodeTransformator
{
    internal class TransformationRuleFactory
    {
        public static List<TransformationRule> Create(string numericBroad, IEnumerable<string> numericNarrows, string[] numericClassnames, string numericClassnameReplacion)
       {
            var rules = new List<TransformationRule>();
            foreach (var numericNarrow in numericNarrows)
            {
                foreach (var numericClassname in numericClassnames)
                {
                    rules.Add(new TransformationRule(numericBroad,numericNarrow,numericClassname,numericClassnameReplacion));
                }
            }
            return rules;
       }

        public static List<TransformationRule> Create(string numericBroad, IEnumerable<string> numericNarrows, string[] numericClassnames, string numericClassnameReplacion, IEnumerable<string> customReplaceString, string toReplace, string byReplace)
        {
            var rules = new List<TransformationRule>();
            foreach (var numericNarrow in numericNarrows)
            {
                foreach (var numericClassname in numericClassnames)
                {
                    var customReplacions = new List<string[]>();
                    foreach (var replaceString in customReplaceString)
                    {
                        var newString = replaceString.Replace(toReplace, byReplace);
                        customReplacions.Add(new []{replaceString,newString});
                    }
                    
                    rules.Add(new TransformationRule(numericBroad, numericNarrow, numericClassname, numericClassnameReplacion, customReplacions));
                }
            }
            return rules;
        }

        public static List<TransformationRule> Create(string numericBroad, IEnumerable<string> numericNarrows, string[] numericClassnames, string numericClassnameReplacion, IEnumerable<string> customReplaceString, string toReplace, bool copy)
        {
            var rules = new List<TransformationRule>();
            foreach (var numericNarrow in numericNarrows)
            {
                foreach (var numericClassname in numericClassnames)
                {
                    var customReplacions = new List<string[]>();
                    foreach (var replaceString in customReplaceString)
                    {
                        var newString = replaceString.Replace(toReplace, numericNarrow.Up1());
                        customReplacions.Add(new[] { replaceString, newString });
                    }
                    rules.Add(new TransformationRule(numericBroad, numericNarrow, numericClassname, numericClassnameReplacion, customReplacions));
                }
            }
            return rules;
        }


        public static IEnumerable<TransformationRule> Create(IEnumerable<string> numericBroads, string numericNarrow, IEnumerable<string> numericClassnames, string numericClassnameReplacion, IEnumerable<string[]> customFirstReplaces)
        {
            var rules = new List<TransformationRule>();
            foreach (var numericBroad in numericBroads)
            {
                foreach (var numericClassname in numericClassnames)
                {
                    rules.Add(new TransformationRule(numericNarrow, numericBroad, numericClassname, numericClassnameReplacion, customFirstReplaces, numericBroad.Up1() + numericNarrow.Up1()));
                }
            }
            return rules;
        }
    }
}
