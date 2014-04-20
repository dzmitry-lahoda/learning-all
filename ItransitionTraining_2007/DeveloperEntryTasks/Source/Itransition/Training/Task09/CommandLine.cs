using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Itransition.Training.Task09
{
    /// <summary>
    /// Represents static class which provides method for command line parsing
    /// </summary>
    static class CommandLine
    {
        /// <summary>
        /// Parses command line arguments 
        /// </summary>
        /// <param name="args">Command line arguments</param>
        /// <returns>StringDictionary where keys and values are from parsed command line</returns>
        public static StringDictionary Parse(string[] args)
        {
            StringDictionary currentParsedArguments = new StringDictionary();
            Regex Spliter = new Regex(@"^-{1,2}|^/|=|:", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            Regex Remover = new Regex(@"^['""]?(.*?)['""]?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            string Parameter = null;
            string[] Parts;

            // Valid parameters forms:
            // {-,/,--}param{ ,=,:}((",')value(",'))
            // Examples: 
            // -param1 value1 --param2 /param3:"Test-:-work" 
            //   /param4=happy -param5 '--=nice=--'
            foreach (string str in args)
            {
                // Look for new parameters (-,/ or --) and a
                // possible enclosed value (=,:)
                Parts = Spliter.Split(str, 3);
                switch (Parts.Length)
                {
                    // Found a value (for the last parameter 
                    // found (space separator))
                    case 1:
                        if (Parameter != null)
                        {
                            if (!currentParsedArguments.ContainsKey(Parameter))
                            {
                                Parts[0] = Remover.Replace(Parts[0], "$1");
                                currentParsedArguments.Add(Parameter, Parts[0]);
                            }
                            Parameter = null;
                        }
                        // else Error: no parameter waiting for a value (skipped)
                        break;
                    // Found just a parameter
                    case 2:
                        // The last parameter is still waiting. 
                        // With no value, set it to true.
                        if (Parameter != null)
                        {
                            if (!currentParsedArguments.ContainsKey(Parameter))
                            {
                                currentParsedArguments.Add(Parameter, "true");
                            }
                        }
                        Parameter = Parts[1];
                        break;
                    // Parameter with enclosed value
                    case 3:
                        // The last parameter is still waiting. 
                        // With no value, set it to true.
                        if (Parameter != null)
                        {
                            if (!currentParsedArguments.ContainsKey(Parameter))
                            {
                                currentParsedArguments.Add(Parameter, "true");
                            }
                        }
                        Parameter = Parts[1];
                        // Remove possible enclosing characters (",')
                        if (!currentParsedArguments.ContainsKey(Parameter))
                        {
                            Parts[2] = Remover.Replace(Parts[2], "$1");
                            currentParsedArguments.Add(Parameter, Parts[2]);
                        }
                        Parameter = null;
                        break;
                }
            }
            // In case a parameter is still waiting
            if (Parameter != null)
            {
                if (!currentParsedArguments.ContainsKey(Parameter))
                {
                    currentParsedArguments.Add(Parameter, "true");
                }
            }
            return currentParsedArguments;
        }
    }
}

