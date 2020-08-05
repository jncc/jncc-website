using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using OfficeOpenXml;

namespace update_redirects
{
    class Program
    {
        static void Main(string[] args)
        {
            string excelSheetPath = args.FirstOrDefault();

            if (String.IsNullOrWhiteSpace(excelSheetPath))
                throw new Exception("Please specify path/to/redirect-list-new-website.xlsx");

            if (!File.Exists(excelSheetPath))
                throw new Exception("File doesn't exist at " + excelSheetPath);

            var redirects = GetRedirects(excelSheetPath);
            WriteMapFile(redirects);
        }

        static IEnumerable<XElement> GetRedirects(string excelSheetPath)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo(excelSheetPath)))
            {
                // worksheet is named "final"
                var sheet = excelPackage.Workbook.Worksheets["final"];

                foreach (int n in Enumerable.Range(1, sheet.Dimension.End.Row))
                {
                    Console.WriteLine($"Making redirect for row {n}...");

                    // parsing values as URIs will catch all manner of errors
                    var from =  new Uri(sheet.Cells[n, 1].Text.Trim());
                    var to   =  new Uri(sheet.Cells[n, 2].Text.Trim());
                    
                    // but IIS redirect maps only want the unencoded path to match on
                    string unencodedPath = HttpUtility.UrlDecode(from.PathAndQuery);

                    yield return new XElement(
                        "add",
                        new XAttribute("key", unencodedPath),
                        new XAttribute("value", to.ToString())
                    );
                }
            }
        }

        static void WriteMapFile(IEnumerable<XElement> redirects)
        {
            // IIS will break if there any duplicate map keys
            var distinctRedirects = redirects
                .DistinctBy(r => r.Attribute("key").Value);

            var rewriteMapsElement = new XElement("rewriteMaps",
                new XElement("rewriteMap",
                    new XAttribute("name", $"JnccDefraGovSiteRedirectsList"),
                    new XAttribute("defaultValue", ""),
                    distinctRedirects
                )
            );

            string path = "../../src/JNCC.PublicWebsite/RewriteMaps.config";
            Console.WriteLine($"Writing {path}...");
            File.WriteAllText(path, rewriteMapsElement.ToString());
        }
    }

    public static class Extensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();

            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }        
    }
}
