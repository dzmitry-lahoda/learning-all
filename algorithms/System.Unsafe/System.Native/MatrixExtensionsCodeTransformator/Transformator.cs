using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MatrixExtensionsCodeTransformator
{
    internal class Transformator
    {
        private string _sourceFilePath;
        private string _destinationFilePath;
        private TransformationRule _rule;

        private string _content;

        public Transformator(TransformationRule rule, string destinationFolder, string sourceFolder)
        {
            _rule = rule;
            _destinationFilePath = Path.Combine(destinationFolder, rule.DestinationFilename);
            _sourceFilePath = Path.Combine(sourceFolder, rule.SourceFilename); ;
        }

        public void Execute()
        {
            _content = File.ReadAllText(_sourceFilePath);
            FindReplace();
            File.Delete(_destinationFilePath);
            File.WriteAllText(_destinationFilePath,_content);
        }

        private void FindReplace()
        {
            foreach (var tuple in _rule.GetReplaceStrings())
            {
                _content = _content.Replace(tuple[0], tuple[1]);
            }
        }
    }
}
