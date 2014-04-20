using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using System.Reflection;

namespace Itransition.Training
{
    /// <summary>
    /// Represent class wich processes specially organized strings.
    /// </summary>
    public class Template
    {
        private String defaultNamespace = "Itransition.Training";
        private String defaultClassName;
        private String defaultMethodName;

        private String input = String.Empty;
        private String code = String.Empty;

        private TypeNamePair[] typeNamePairs;
        private Type compiledType;
        private CodeDomProvider codeDomProvider;

        /// <summary>
        /// Creates new instance of Itransition.Training.Template.
        /// </summary>
        /// <param name="input">String for parsing.</param>
        /// <param name="assemblyNames">Imported assemblies.</param>
        /// <param name="namespaces">Imported namespaces.</param>
        /// <param name="typeNamePairs">Imported type and names pairs</param>
        public Template(String input, String[] assemblyNames, String[] namespaces, TypeNamePair[] typeNamePairs)
        {
            this.typeNamePairs = typeNamePairs;
            this.defaultClassName = "Class";
            this.defaultMethodName = "Method";
            this.input = input;
            this.code = GetCode();
            this.codeDomProvider = new CSharpCodeProvider();
            CodeCompileUnit codeCompileUnit = GetCompileUnit(namespaces);
            CompilerParameters compilerParameters = GetCompilerParameters(assemblyNames);
            this.compiledType = GetCompiledType(codeCompileUnit, compilerParameters);
        }

        /// <summary>
        /// Executes code that writes into output.
        /// </summary>
        /// <param name="output"></param>
        /// <param name="variables"></param>
        public void Render(TextWriter output)
        {
            this.Render(output, null);
        }

        /// <summary>
        /// Executes code that writes into output.
        /// </summary>
        /// <param name="output"></param>
        /// <param name="variables">Instances of imported to template type and name pairs</param>
        public void Render(TextWriter output, Object[] variables)
        {
            List<object> methodParameters = new List<object>();
            methodParameters.Add(output);
            if (typeNamePairs == null)
            {
                InvokeMethod(methodParameters);
            }
            else if ((variables != null) && (variables.Length == typeNamePairs.Length))
            {
                foreach (object var in variables)
                {
                    methodParameters.Add(var);
                }
                InvokeMethod(methodParameters);
            }
        }

        private void InvokeMethod(List<object> methodParameters)
        {
            if (compiledType != null)
            {
                object obj = Activator.CreateInstance(compiledType);
                MethodInfo methodInfo = compiledType.GetMethod(defaultMethodName);
                methodInfo.Invoke(obj, methodParameters.ToArray());
            }
        }

        #region CodeCompileUnit building

        private CodeCompileUnit GetCompileUnit(String[] namespaces)
        {
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            AddCodeNamespace(namespaces, compileUnit);
            return compileUnit;
        }

        private void AddCodeNamespace(String[] namespaces, CodeCompileUnit compileUnit)
        {
            CodeNamespace codeNamespace = new CodeNamespace(defaultNamespace);
            if (namespaces != null)
            {
                foreach (String var in namespaces)
                {
                    codeNamespace.Imports.Add(new CodeNamespaceImport(var));
                }
            }
            AddCodeTypeDeclaration(codeNamespace);
            compileUnit.Namespaces.Add(codeNamespace);
        }

        private void AddCodeTypeDeclaration(CodeNamespace codeNamespace)
        {
            CodeTypeDeclaration typeDeclaration = new CodeTypeDeclaration(defaultClassName);
            AddCodeMemberMethod(typeDeclaration);
            codeNamespace.Types.Add(typeDeclaration);
        }

        private void AddCodeMemberMethod(CodeTypeDeclaration typeDeclaration)
        {
            CodeMemberMethod memberMethod = new CodeMemberMethod();
            memberMethod.ReturnType = new CodeTypeReference("System.Void");
            memberMethod.Name = defaultMethodName;
            memberMethod.Attributes = MemberAttributes.Public;
            memberMethod.Parameters.Add(new CodeParameterDeclarationExpression("System.IO.TextWriter", "output"));
            AddMethodParameters(memberMethod);
            AddMethodBody(memberMethod);
            typeDeclaration.Members.Add(memberMethod);
        }

        private void AddMethodParameters(CodeMemberMethod memberMethod)
        {
            if (typeNamePairs != null)
            {
                foreach (TypeNamePair var in typeNamePairs)
                {
                    memberMethod.Parameters.Add(
                        new CodeParameterDeclarationExpression(var.Type, var.Name));
                }
            }
        }

        private void AddMethodBody(CodeMemberMethod memberMethod)
        {
            CodeSnippetExpression codeSnippetExpression = new CodeSnippetExpression(code);
            memberMethod.Statements.Add(codeSnippetExpression);
        }


        #endregion

        #region Compilation

        private CompilerParameters GetCompilerParameters(String[] assemblyNames)
        {
            CompilerParameters compilerParameters = new CompilerParameters();
            if (assemblyNames != null)
            {
                foreach (String var in assemblyNames)
                {
                    compilerParameters.ReferencedAssemblies.Add(var);
                }
            }
            compilerParameters.GenerateInMemory = true;
            return compilerParameters;
        }

        private Type GetCompiledType(CodeCompileUnit codeCompileUnit,CompilerParameters compilerParameters)
        {
            CompilerResults compilerResults =
            codeDomProvider.CompileAssemblyFromDom(compilerParameters, codeCompileUnit);
            if (compilerResults.Errors.HasErrors)
            {
                throw new TemplateException(input, code);
            }
            Assembly assembly = compilerResults.CompiledAssembly;
            return assembly.GetType(defaultNamespace + "." + defaultClassName);
        }

        #endregion

        //TODO: Parse string wich contains "output.Write("<%....%>");"
        private String GetCode()
        {
            String pattern = @"((%>)(?<text>.*?)(?=<%))|((<%)(?<code>.*?)(?=%>))";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches("%>" + input + "<%");
            return ParseMatches(matches);
        }

        private String ParseMatches(MatchCollection matches)
        {
            StringBuilder code = new StringBuilder();
            foreach (Match match in matches)
            {
                String codeGroup = match.Groups["code"].Value;
                code.Append(ParseCodeGroup(codeGroup));
                String textGroup = match.Groups["text"].Value;
                code.Append(ParseTextGroup(textGroup));
            }
            return code.ToString();
        }

        private static String ParseTextGroup(String textGroup)
        {
            if (textGroup != String.Empty)
            {
                return " output.Write(\"" + textGroup + "\");";
            }
            return String.Empty;
        }

        private static String ParseCodeGroup(String codeGroup)
        {
            if (codeGroup != String.Empty)
            {
                if (codeGroup[0] == '=')
                {
                    String text = codeGroup.Remove(0, 1);
                    return " output.Write(" + text + ");";
                }
                else
                {
                    return codeGroup;
                }
            }
            return String.Empty;
        }

     }
}
