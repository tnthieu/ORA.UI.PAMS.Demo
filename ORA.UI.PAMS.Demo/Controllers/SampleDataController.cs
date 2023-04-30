using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ORA_UI_PAMS_Demo.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using ORA.UI.PAMS.Demo.Models;
using System.Security.Cryptography.Xml;
using Newtonsoft.Json;
using System.Xml.Linq;
using OfficeOpenXml;

namespace ORA_UI_PAMS_Demo.Controllers
{

    [Route("api/[controller]")]
    public partial class SampleDataController : Controller
    {
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            if (SampleData.Orders.Count == 0)
            {
                var filePath = @"C:\ORISWebApps\ORA.UI.PAMS.Demo\ORA.UI.PAMS.Demo\ORA.UI.PAMS.Demo\XLSX\Attachments.xlsx";
                FileInfo fileInfo = new FileInfo(filePath);

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    for (int row = 8; row <= 15; row++)
                    {
                        SampleData.Orders.Add(new SampleOrder
                        {
                            Id = row,
                            IsImportant = row % 2 == 0,

                            CreatedDate = worksheet.Cells[row, 2].Value?.ToString().Trim().Replace("202","2"),
                            ModifiedDate = worksheet.Cells[row, 3].Value?.ToString().Trim().Replace("202", "2"),

                            UserName = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                            Category = worksheet.Cells[row, 5].Value?.ToString().Trim(),

                            Comment = worksheet.Cells[row, 8].Value?.ToString().Trim(),
                            Fund = worksheet.Cells[row, 7].Value?.ToString().Trim(),

                            EditComment = "Test",
                            AttachmentCount = new Random().Next(0, 9).ToString(),
                        });
                    }
                }

                //for (var i = 0; i < 100; i++)
                //{
                //    SampleData.Orders.Add(new SampleOrder
                //    {
                //        Id = i,
                //        IsImportant = i % 2 == 0,

                //        CreatedDate = DateTime.Now,
                //        ModifiedDate = DateTime.Now,

                //        UserName = "Test",
                //        Category = "Category " + new Random().Next(1, 5).ToString(),

                //        Comment = "Test",
                //        Fund = new Random().Next(1000, 9999).ToString(),
                //        AttachmentName = "Test",

                //        EditComment = "Test",
                //        AttachmentCount = new Random().Next(0, 9).ToString(),
                //    });
                //}
            }
            return DataSourceLoader.Load(SampleData.Orders, loadOptions);
        }

        [HttpDelete]
        public void Delete(int key)
        {
            SampleData.Orders.RemoveAll(o => o.Id == key);
        }

        [HttpPut]
        public IActionResult Update(int key, string values)
        {
            var employee = SampleData.Orders.FirstOrDefault(a => a.Id == key);
            JsonConvert.PopulateObject(values, employee);

            return Ok();
        }
    }
}