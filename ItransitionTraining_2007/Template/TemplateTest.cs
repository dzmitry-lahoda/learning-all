using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace Itransition.Training
{
    [TestFixture]
    public class TemplateTest
    {

        [Test]
        public void NormalRender1()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            String[] namespaces = { "System", "System.Text" };
            String[] assemblyNames = { "mscorlib.dll", "System.dll" };
            TypeNamePair[] typeNamePairs = { new TypeNamePair("str", typeof(String)) };
            Template template = new Template("<%output.Write(str);%><%=\"NormalRender1\"%>", assemblyNames, namespaces, typeNamePairs);
            template.Render(output, new Object[] { "NormalRender1" });
            Assert.AreEqual("NormalRender1NormalRender1", stringBuilder.ToString());
        }

        [Test]
        public void NormalRender2()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            String[] namespaces = { "System", "System.Text" };
            String[] assemblyNames = { "mscorlib.dll", "System.dll" };
            TypeNamePair[] typeNamePairs = { new TypeNamePair("n", typeof(int)) };
            Template template = new Template("<%for (int i= 0; i < n; i++) {%> NormalRender2 <%}%>", assemblyNames, namespaces, typeNamePairs);
            template.Render(output, new Object[] { 3 });
            Assert.AreEqual(" NormalRender2  NormalRender2  NormalRender2 ",stringBuilder.ToString());
        }

        [Test]
        public void NormalRender3()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            String[] namespaces = { "System", "System", "System.Text" };
            String[] assemblyNames = { "mscorlib.dll", "System.dll" };
            TypeNamePair[] typeNamePairs = { new TypeNamePair("n", typeof(int)) };
            Template template = new Template("NormalRender3", assemblyNames, namespaces, typeNamePairs);
            template.Render(output, new Object[] { 3 });
            Assert.AreEqual("NormalRender3", stringBuilder.ToString());
        }

        [Test]
        public void NormalRender4()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            Template template = new Template("<%for (int i=0;i<2;i++){output.Write(\"<%%>\");}%>", null, null, null);
            template.Render(output, new Object[] { 3 });
            Console.WriteLine("\n" + stringBuilder);
        }

        [Test]
        public void NullRender()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            Template template = new Template("<%output.Write(\"NullRender\");%>", null, null, null);
            template.Render(output);
            Assert.AreEqual("NullRender", stringBuilder.ToString());
        }

        [Test]
        [ExpectedException(typeof(TemplateException))]
        public void NameConcurrency()
        {
            String[] namespaces = { "System", "System.Text" };
            TypeNamePair[] typeNamePairs = { new TypeNamePair("output", typeof(int)) };
            Template template = new Template("<%for (int i= 0; i < output; i++) {%> Test2 <%}%>", null, namespaces, typeNamePairs);
        }

        [Test]
        public void ParameterCountMismatch()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            String[] namespaces = { "System", "System.Text" };
            TypeNamePair[] typeNamePairs = { new TypeNamePair("n", typeof(int)) };
            Template template = new Template("<%for (int i= 0; i < n; i++) {%> Test2 <%}%>", null, namespaces, typeNamePairs);
            template.Render(output);
            Assert.AreEqual("", stringBuilder.ToString());
        }

        [Test]
        [ExpectedException(typeof(TemplateException))]
        public void WrongNamespace()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            String[] namespaces = { "%~`", "System.Text" };
            TypeNamePair[] typeNamePairs = { new TypeNamePair("n", typeof(int)) };
            Template template = new Template("<%for (int i= 0; i < n; i++) {%> WrongNamespace <%}%>", null, namespaces, typeNamePairs);
            template.Render(output);
        }

        [Test]
        [ExpectedException(typeof(TemplateException))]
        public void WrongAssemblyNames()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            String[] assemblyNames = { "%~`", "System.dll" };
            TypeNamePair[] temp = { new TypeNamePair("n", typeof(int)) };
            Template template = new Template("<%=\"WrongAssemblyNames\"%>", assemblyNames, null, temp);
            template.Render(output, new Object[] { 3 }); 
        }

        [Test]
        [ExpectedException(typeof(TemplateException))]
        public void WrongInputStringRender1()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            Template template = new Template("<%168`12435%>", null, null, null);
            template.Render(output, new Object[] { 3 });
        }

        [Test]
        [ExpectedException(typeof(TemplateException))]
        public void WrongInputStringRender2()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter output = new StringWriter(stringBuilder);
            Template template = new Template("<%for (int i=0;i<2;i++){}<%output.Write(\"ASD\");%>%>", null, null, null);
            template.Render(output, new Object[] { 3 });
            Console.WriteLine("\n"+stringBuilder);
        }

    }
}
