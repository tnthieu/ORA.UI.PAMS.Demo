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
    public partial class SpecialNoteController : Controller
    {
        static List<SpecialNote> Notes = new List<SpecialNote>();

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            if (Notes.Count == 0)
            {
                var filePath = @"C:\ORISWebApps\ORA.UI.PAMS.Demo\ORA.UI.PAMS.Demo\ORA.UI.PAMS.Demo\XLSX\Attachments.xlsx";
                FileInfo fileInfo = new FileInfo(filePath);

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    for (var i = 1; i <= 10; i++)
                        for (int row = 8; row <= 15; row++)
                        {
                            var data = new SpecialNote
                            {
                                Id = i * row,
                                IsImportant = row % 2 == 0,

                                UserName = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                                Category = worksheet.Cells[row, 5].Value?.ToString().Trim(),

                                Comment = worksheet.Cells[row, 8].Value?.ToString().Trim(),
                                Fund = worksheet.Cells[row, 7].Value?.ToString().Trim(),

                                EditComment = "Test",
                                AttachmentCount = new Random().Next(0, 9).ToString(),
                            };
                            Notes.Add(data);

                            data.CreatedDate = new DateTime(2020, 1, 1);
                            data.CreatedDate = data.CreatedDate.AddDays(new Random().Next(1, 365 * 2));
                            data.CreatedDate = data.CreatedDate.AddHours(new Random().Next(1, 23)).AddMinutes(new Random().Next(1, 59));

                            data.ModifiedDate = data.CreatedDate.AddDays(new Random().Next(1, 365));
                            data.ModifiedDate = data.ModifiedDate.AddHours(new Random().Next(1, 23)).AddMinutes(new Random().Next(1, 59));
                        }
                }
            }
            return DataSourceLoader.Load(Notes, loadOptions);
        }

        [HttpDelete]
        public void Delete(int key)
        {
            Notes.RemoveAll(o => o.Id == key);
        }

        [HttpPut]
        public IActionResult Update(int key, string values)
        {
            var employee = Notes.FirstOrDefault(a => a.Id == key);
            JsonConvert.PopulateObject(values, employee);

            return Ok();
        }
    }
}