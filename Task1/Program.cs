using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlExtracter html = new HtmlExtracter();
            string result = html.CovertHTMLtoJSON();

            // saving file in project location in bin/debug/net folder.
            string jsonFile = Environment.CurrentDirectory + "/output.json";
            System.IO.File.WriteAllText(jsonFile, result);

            Console.WriteLine(result);
        }
    }
}
