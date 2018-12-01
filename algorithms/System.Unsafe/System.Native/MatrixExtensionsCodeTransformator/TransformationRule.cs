using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixExtensionsCodeTransformator
{
    internal class TransformationRule
    {
        private string _numericBroad;
        private string _numericNarrow;
        private string _numericClassname;
        private string _destinationClassname;
        private string _destinationFilename;
        private string _sourceFilename;
        private string _fileExtension = "cs";
        private IEnumerable<string[]> _customReplaceStrings;

        public TransformationRule(string numericBroad, string numericNarrow, string numericClassname, string numericClassnameReplacion, IEnumerable<string[]> customReplaceStrings,string customClassnameReplacion)
        {
            _numericClassname = numericClassname;
            _numericBroad = numericBroad;
            _numericNarrow = numericNarrow;
            _destinationClassname = _numericClassname.Replace(numericClassnameReplacion,customClassnameReplacion );
            _sourceFilename = _numericClassname + "." + _fileExtension;
            _destinationFilename = _destinationClassname + "." + _fileExtension;
            _customReplaceStrings = customReplaceStrings;
        }

        public TransformationRule(string numericBroad, string numericNarrow, string numericClassname, string numericClassnameReplacion, IEnumerable<string[]> customReplaceStrings)
        {
            _numericClassname = numericClassname;
            _numericBroad = numericBroad;
            _numericNarrow = numericNarrow;
            _destinationClassname = _numericClassname.Replace(numericClassnameReplacion, numericBroad.Up1() + numericNarrow.Up1());
            _sourceFilename = _numericClassname + "." + _fileExtension;
            _destinationFilename = _destinationClassname + "." + _fileExtension;
            _customReplaceStrings = customReplaceStrings;
        }

        public TransformationRule(string numericBroad, string numericNarrow, string numericClassname, string numericClassnameReplacion)
            : this(numericBroad, numericNarrow, numericClassname, numericClassnameReplacion, new List<string[]>())
        {
            
        }


        public List<string[]> GetReplaceStrings()
        {
            var replaceStrings = new List<string[]>();
            
            //replace semantically meaningfull extensions methods by their content
            replaceStrings.Add(new []{".IsEmpty()",".Length == 0"});
            replaceStrings.Add(new[] { ".RowCount()", ".GetLength(0)" });
            replaceStrings.Add(new[] { ".ColumnCount()", ".GetLength(1)" });
            
            replaceStrings.AddRange(_customReplaceStrings);
            

            replaceStrings.AddRange(
                        new List<string[]> {
                             new[] {_numericClassname,_destinationClassname},
                             new[] {"NumericBroad", _numericBroad},

                             new[] {"NumericNarrow", _numericNarrow}
                         });

            return replaceStrings;
        }
        
        public string DestinationFilename
        {
            get { return _destinationFilename; }
        }

        public string SourceFilename
        {
            get { return _sourceFilename; }
        }
    }
}
