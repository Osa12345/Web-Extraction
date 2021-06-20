using NUnit.Framework;
using Task1;
using System.IO;
using System;

namespace Task1Test
{
    public class UnitTestTask1
    {
        [Test]
        public void Test1()
        {

            string fileName = "ExpectedOutput.json";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            string expectedOutput = "";
            using (StreamReader jsonStream = new StreamReader(path))
            {
                expectedOutput = jsonStream.ReadToEnd();
            }

            Task1.HtmlExtracter d = new HtmlExtracter();
            string actualOutput = d.CovertHTMLtoJSON();
            Assert.That(actualOutput, Is.EqualTo(expectedOutput).IgnoreCase); 
        }
    }
}