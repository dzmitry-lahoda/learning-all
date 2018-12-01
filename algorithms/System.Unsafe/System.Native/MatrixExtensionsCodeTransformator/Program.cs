using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using PGK.Extensions;

namespace MatrixExtensionsCodeTransformator
{
    internal class Program
    {

        

        static void Main(string[] args)
        {

            

            CopyGenericCode();
            
            GenerateAndCopyNumericExtensions();

        }

        private static void GenerateAndCopyNumericExtensions()
        {
            var context = new TransformationContext();

            context.SourceFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            context.SolutionFolder = Path.GetDirectoryName(context.SourceFolder);


            string manipulationDestinationFolder = CreateDestinationFolder(context, @"MatrixExtensions\Manipulation");
            string conversionDestinationFolder = CreateDestinationFolder(context, @"MatrixExtensions\Conversion");
            string explicitConversionDestinationFolder = CreateDestinationFolder(context, @"MatrixExtensions\Conversion\Explicit");
            string operationsDestinationFolder = CreateDestinationFolder(context, @"MatrixExtensions\Operations");

            var decimalCompatibles = new[] { "decimal", "long", "int" };
            var doubleCompatibles = new[] { "double", "float", "long", "int" };
            var floatCompatibles = new[] { "float", "long", "int" };
            var longCompatibles = new[] { "long", "int" };
            var intCompatibles = new[] { "int" };
            var operationsClasses = new[] { "NumericArrayOperationsExtensions", "NumericArray2DOperationsExtensions" };
            var simpleOperationClasses = new[] { "NumericArray2DSimpleOperationsExtensions", "NumericArraySimpleOperationsExtensions" };
            var simpleManipulationsClasses = new[] { "NumericArray2DSimpleManipulationExtensions" };

            var manipulationClasses = new[]
                                           {
                                               "NumericArray2DManipulationExtensions",
                                               "NumericArrayManipulationExtensions"
                                           };
            var conversionClasses = new[]
                                           {
                                               "NarrowToBroadConversionExtensions",
                                           };

            var explicitConversionClasses = new[]
                                           {
                                               "BroadToNarrowExplicitConversionExtensions",
                                           };

            var conversionStrings = new List<string> { "ToNumericBroad(", "ToNumericNarrow" };


            var toReplace = "NumericBroad";

            var narrowToBroad = "NarrowToBroad";


            var conversionDecimalRules = TransformationRuleFactory.Create(
                "decimal", decimalCompatibles.Skip(1), conversionClasses, narrowToBroad, conversionStrings, toReplace, "Decimal");
            var conversionDoubleRules = TransformationRuleFactory.Create(
    "double", doubleCompatibles.Skip(1), conversionClasses, narrowToBroad, conversionStrings, toReplace, "Double");
            var conversionFloatRules = TransformationRuleFactory.Create(
    "float", floatCompatibles.Skip(1), conversionClasses, narrowToBroad, conversionStrings, toReplace, "Float");
            var conversionLongRules = TransformationRuleFactory.Create(
    "long", longCompatibles.Skip(1), conversionClasses, narrowToBroad, conversionStrings, toReplace, "Long");
            var conversionIntRules = TransformationRuleFactory.Create(
    "int", intCompatibles.Skip(1), conversionClasses, narrowToBroad, conversionStrings, toReplace, "Int");

            var conversionRules = new List<TransformationRule>();
            conversionRules.AddRanges(conversionDecimalRules, conversionDoubleRules, conversionFloatRules, conversionLongRules, conversionIntRules);
            ExecuteTransformatorForEachRule(conversionRules, conversionDestinationFolder, context.SourceFolder);

            toReplace = "NumericNarrow";

            var broadToNarrow = "BroadToNarrow";

            var explicitConversionDecimalRules = TransformationRuleFactory.Create(
    "decimal", decimalCompatibles.Skip(1).Concat(new[] { "double", "float" }), explicitConversionClasses, broadToNarrow, conversionStrings, toReplace, true);

            var explicitConversionDoubleRules = TransformationRuleFactory.Create(
              "double", doubleCompatibles.Skip(1), explicitConversionClasses, broadToNarrow, conversionStrings, toReplace, true);
            var explicitConversionFloatRules = TransformationRuleFactory.Create(
    "float", floatCompatibles.Skip(1), explicitConversionClasses, broadToNarrow, conversionStrings, toReplace, true);
            var explicitConversionLongRules = TransformationRuleFactory.Create(
    "long", longCompatibles.Skip(1), explicitConversionClasses, broadToNarrow, conversionStrings, toReplace, true);
            var explicitConversionIntRules = TransformationRuleFactory.Create(
    "int", intCompatibles.Skip(1), explicitConversionClasses, broadToNarrow, conversionStrings, toReplace, true);

            var explicitConversionRules = new List<TransformationRule>();
            explicitConversionRules.AddRanges(explicitConversionDecimalRules, explicitConversionDoubleRules, explicitConversionFloatRules, explicitConversionLongRules, explicitConversionIntRules);
            ExecuteTransformatorForEachRule(explicitConversionRules, explicitConversionDestinationFolder, context.SourceFolder);


            var concatenationDecimalRules = TransformationRuleFactory.Create(
                "decimal", decimalCompatibles, manipulationClasses, "Numeric"
                );
            var concatenationDoubleRules = TransformationRuleFactory.Create(
    "double", doubleCompatibles, manipulationClasses, "Numeric"
    );
            var concatenationFloatRules = TransformationRuleFactory.Create(
    "float", floatCompatibles, manipulationClasses, "Numeric"
    );
            var concatenationLongRules = TransformationRuleFactory.Create(
"long", longCompatibles, manipulationClasses, "Numeric"
);

            var concatenationIntRules = TransformationRuleFactory.Create(
"int", intCompatibles, manipulationClasses, "Numeric"
);

            var concatenationRules = new List<TransformationRule>();
            concatenationRules.AddRanges(concatenationDecimalRules, concatenationDoubleRules, concatenationDoubleRules, concatenationFloatRules, concatenationLongRules, concatenationIntRules);
            ExecuteTransformatorForEachRule(concatenationRules, manipulationDestinationFolder, context.SourceFolder);

            #region Simple Manipulations

            var simpleManipulationDecimalRules = TransformationRuleFactory.Create(
    "decimal", decimalCompatibles.Take(1), simpleManipulationsClasses, "Numeric"
    );
            var simpleManipulationDoubleRules = TransformationRuleFactory.Create(
    "double", doubleCompatibles.Take(1), simpleManipulationsClasses, "Numeric"
    );
            var simpleManipulationFloatRules = TransformationRuleFactory.Create(
    "float", floatCompatibles.Take(1), simpleManipulationsClasses, "Numeric"
    );
            var simpleManipulationLongRules = TransformationRuleFactory.Create(
"long", longCompatibles.Take(1), simpleManipulationsClasses, "Numeric"
);

            var simpleManipulationIntRules = TransformationRuleFactory.Create(
"int", intCompatibles.Take(1), simpleManipulationsClasses, "Numeric"
);

            var simpleManipulationRules = new List<TransformationRule>();
            simpleManipulationRules.AddRanges(simpleManipulationDecimalRules, simpleManipulationDoubleRules, simpleManipulationDoubleRules, simpleManipulationFloatRules, simpleManipulationLongRules, simpleManipulationIntRules);
            ExecuteTransformatorForEachRule(simpleManipulationRules, manipulationDestinationFolder, context.SourceFolder);
            #endregion

            var strs = new List<string[]> {new []{"NumericNarrow[]","NumericBroad[]"},
            new []{"this NumericBroad[]", "this NumericNarrow[]"},
            new []{"NumericNarrow[,]","NumericBroad[,]"},
            new []{"this NumericBroad[,]", "this NumericNarrow[,]"}};


            var manipulationDecimalRules = TransformationRuleFactory.Create(
     decimalCompatibles, "decimal", manipulationClasses.Skip(1), "Numeric", strs
     );
            var manipulationDoubleRules = TransformationRuleFactory.Create(
    doubleCompatibles, "double", manipulationClasses.Skip(1), "Numeric", strs
    );
            var manipulationFloatRules = TransformationRuleFactory.Create(
    floatCompatibles, "float", manipulationClasses.Skip(1), "Numeric", strs
    );


            var manipulationLongRules = TransformationRuleFactory.Create(
longCompatibles, "long", manipulationClasses.Skip(1), "Numeric", strs
);

            var manipulationIntRules = TransformationRuleFactory.Create(
intCompatibles, "int", manipulationClasses.Skip(1), "Numeric", strs
);

            var manipulationRules = new List<TransformationRule>();
            manipulationRules.AddRanges(manipulationDecimalRules, manipulationDoubleRules, manipulationDoubleRules, manipulationFloatRules, manipulationLongRules, manipulationIntRules);
            ExecuteTransformatorForEachRule(manipulationRules, manipulationDestinationFolder, context.SourceFolder);




            var simpleDecimalRules = TransformationRuleFactory.Create(
    "decimal", decimalCompatibles.Take(1), simpleOperationClasses, "Numeric"
    );
            var simpleDoubleRules = TransformationRuleFactory.Create(
                "double", floatCompatibles.Take(1), simpleOperationClasses, "Numeric"
                );
            var simpleFloatRules = TransformationRuleFactory.Create(
"float", floatCompatibles.Take(1), simpleOperationClasses, "Numeric"
);
            var simpleLongRules = TransformationRuleFactory.Create(
 "long", longCompatibles.Take(1), simpleOperationClasses, "Numeric"
 );
            var simpleIntRules = TransformationRuleFactory.Create(
"int", intCompatibles.Take(1), simpleOperationClasses, "Numeric"
);
            var simpleOperationsRules = new List<TransformationRule>();
            simpleOperationsRules.AddRanges(simpleDecimalRules, simpleDoubleRules, simpleFloatRules, simpleLongRules, simpleIntRules);
            ExecuteTransformatorForEachRule(simpleOperationsRules, operationsDestinationFolder, context.SourceFolder);


            var decimalRules = TransformationRuleFactory.Create(
                "decimal", decimalCompatibles, operationsClasses, "Numeric"
                );
            var doubleRules = TransformationRuleFactory.Create(
                "double", doubleCompatibles, operationsClasses, "Numeric"
                );
            var floatRules = TransformationRuleFactory.Create(
"float", floatCompatibles, operationsClasses, "Numeric"
);
            var longRules = TransformationRuleFactory.Create(
 "long", longCompatibles, operationsClasses, "Numeric"
 );
            var intRules = TransformationRuleFactory.Create(
"int", intCompatibles, operationsClasses, "Numeric"
);
            var operationsRules = new List<TransformationRule>();
            operationsRules.AddRanges(decimalRules, doubleRules, doubleRules, floatRules, longRules, intRules);
            ExecuteTransformatorForEachRule(operationsRules, operationsDestinationFolder, context.SourceFolder);
        }

        /// <summary>
        /// Copies code of generic extensions to MatrixExtension code folder.
        /// </summary>
        private static void CopyGenericCode()
        {
            var genericPath = "../MatrixExtensionsGenericCodeBase/";

            var genericDirectoryInfo = new DirectoryInfo(genericPath);
            var genericExtensions = genericDirectoryInfo.FindFilesRecursive("*.cs").Where(item => item.Name.Contains("Extensions"));

            var matrixExtensions = new DirectoryInfo("../MatrixExtensions/");

            foreach (var item in genericExtensions)
            {
                string newPath = item.FullName.Remove(genericDirectoryInfo.FullName);
                newPath = Path.Combine(matrixExtensions.FullName, newPath);
                if (File.Exists(newPath))
                {
                    File.Delete(newPath);
                }
                File.Copy(item.FullName, newPath);
            }
        }



        private static string CreateDestinationFolder(TransformationContext context, string namespaceDirectoryPath)
        {
            string result = Path.Combine(context.SolutionFolder, namespaceDirectoryPath);
            return result;
        }

        public static IEnumerable<string> Up(IEnumerable<string> compatibles)
        {
            var skiped = compatibles.Skip(1);
            var newReplaces = new List<string>();
            foreach (var replace in skiped)
            {
                newReplaces.Add(replace.Replace(replace[0].ToString(), replace[0].ToString().ToUpper()));
            }
            return newReplaces;
        }

        public static void ExecuteTransformatorForEachRule(IEnumerable<TransformationRule> rules, string destinationFolder, string sourceFolder)
        {
            var transformators = new List<Transformator>();
            foreach (var rule in rules)
            {
                transformators.Add(new Transformator(rule, destinationFolder, sourceFolder));
            }
            foreach (var transformator in transformators)
            {
                transformator.Execute();
            }
        }
    }
}
