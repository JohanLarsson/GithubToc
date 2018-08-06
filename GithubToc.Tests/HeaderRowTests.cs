namespace GithubToc.Tests
{
    using NUnit.Framework;

    public class HeaderRowTests
    {
        [TestCase("# Gu.SerializationAsserts", "- [Gu.SerializationAsserts](#guserializationasserts)")]
        [TestCase("## Table of Contents", "  - [Table of Contents](#table-of-contents)")]
        [TestCase("## 1. XmlAssert", "  - [1. XmlAssert](#1-xmlassert)")]
        [TestCase("### 2.1 RoundTrip", "    - [2.1 RoundTrip](#21-roundtrip)")]
        [TestCase("### The options are:", "    - [The options are:](#the-options-are)")]
        [TestCase("## ItemsSource.Array2D & Array2DTransposed", "  - [ItemsSource.Array2D & Array2DTransposed](#itemssourcearray2d--array2dtransposed)")]
        [TestCase("### 2.2. Translator&lt;T&gt;.", "    - [2.2. Translator&lt;T&gt;.](#22-translatort)")]
        [TestCase("### 1.2.1 HasDataContractAttribute&lt;T&gt;()", "    - [1.2.1 HasDataContractAttribute&lt;T&gt;()](#121-hasdatacontractattributet)")]
        [TestCase("### 1.1.1 State (`MediaState`)", "    - [1.1.1 State (`MediaState`)](#111-state-mediastate)")]
        public void ParseRow(string markdown, string expected)
        {
            var headerRow = HeaderRow.Parse(markdown);
            Assert.AreEqual(expected, headerRow.ToString());
        }
    }
}