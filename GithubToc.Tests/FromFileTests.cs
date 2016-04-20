namespace GithubToc.Tests
{
    using System;
    using System.IO;
    using System.Text;

    using NUnit.Framework;

    public class FromFileTests
    {
        [Test, Explicit]
        public void DumpHeaders()
        {
            var path = @"C:\Git\Gu.SerializationAsserts\README.md";
            if (!File.Exists(path))
            {
                Assert.Fail();
            }

            var markdown = File.ReadAllText(path);
            var stringBuilder = new StringBuilder();
            foreach (var header in TableOfContents.GetHeaders(markdown))
            {
                stringBuilder.AppendLine(header);
            }
            var headers = stringBuilder.ToString();
            Console.Write(headers);
        }

        [Test]
        public void GenerateToc()
        {
            var markdown = "# Gu.SerializationAsserts\r\n" +
                           "## Table of Contents\r\n" +
                           "## 1. XmlAssert\r\n" +
                           "## 2. XmlSerializerAssert\r\n" +
                           "### 2.1 RoundTrip\r\n" +
                           "### 2.2 Equal\r\n" +
                           "## 3. XmlAssertOptions\r\n" +
                           "### The options are:\r\n" +
                           "## 4. DataContractSerializerAssert \r\n" +
                           "## 5. BinaryFormatterAssert \r\n" +
                           "## 6. FieldAssert\r\n";

            var expected = "- [Gu.SerializationAsserts](#guserializationasserts)\r\n" +
                           "  - [Table of Contents](#table-of-contents)\r\n" +
                           "  - [1. XmlAssert](#1-xmlassert)\r\n" +
                           "  - [2. XmlSerializerAssert](#2-xmlserializerassert)\r\n" +
                           "    - [2.1 RoundTrip](#21-roundtrip)\r\n" +
                           "    - [2.2 Equal](#22-equal)\r\n" +
                           "  - [3. XmlAssertOptions](#3-xmlassertoptions)\r\n" +
                           "    - [The options are:](#the-options-are)\r\n" +
                           "  - [4. DataContractSerializerAssert](#4-datacontractserializerassert)\r\n" +
                           "  - [5. BinaryFormatterAssert](#5-binaryformatterassert)\r\n" +
                           "  - [6. FieldAssert](#6-fieldassert)\r\n";
            var toc = TableOfContents.Create(markdown);
            Console.Write(toc);
            Assert.AreEqual(expected, toc);
        }
    }

    public class HeaderRowTests
    {
        [TestCase("# Gu.SerializationAsserts", "- [Gu.SerializationAsserts](#guserializationasserts)")]
        [TestCase("## Table of Contents", "  - [Table of Contents](#table-of-contents)")]
        [TestCase("## 1. XmlAssert", "  - [1. XmlAssert](#1-xmlassert)")]
        [TestCase("### 2.1 RoundTrip", "    - [2.1 RoundTrip](#21-roundtrip)")]
        [TestCase("### The options are:", "    - [The options are:](#the-options-are)")]
        public void TestNameTest(string markdown, string expected)
        {
            var headerRow = HeaderRow.Parse(markdown);
            Assert.AreEqual(expected, headerRow.ToString());
        }
    }
}
