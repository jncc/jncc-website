using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using OfficeOpenXml;

namespace update_redirects
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string excelSheetPath = args.FirstOrDefault();

            if (String.IsNullOrWhiteSpace(excelSheetPath))
            {
                throw new Exception("Please specify path/to/redirect-list-new-website.xlsx");
            }
            if (!File.Exists(excelSheetPath))
            {
                throw new Exception("File doesn't exist at " + excelSheetPath);
            }

            using (var excelPackage = new ExcelPackage(new FileInfo(excelSheetPath)))
            {
                // worksheet is named "final"
                var sheet = excelPackage.Workbook.Worksheets["final"];

                var redirects  = from n in Enumerable.Range(1, sheet.Dimension.End.Row)
                                 select sheet.Cells[n, 3].Text; // redirects are in third column

                var rewriteElements = from r in redirects
                                      select XElement.Parse(r);

                var rewriteMapsElement = new XElement("rewriteMaps",
                    new XElement("rewriteMap",
                        new XAttribute("name", "JnccDefraGovSiteRedirectsList"),
                        new XAttribute("defaultValue", ""),
                        rewriteElements
                    )
                );

                File.WriteAllText(
                    "../../src/JNCC.PublicWebsite/RewriteMaps.config",
                    rewriteMapsElement.ToString()
                );
            }
        }
    }
}
