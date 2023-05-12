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
using System.Reflection;

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

                    var id = 0;
                    for (var i = 1; i <= 10; i++)
                        for (int row = 8; row <= 15; row++)
                        {
                            id++;
                            var data = new SpecialNote
                            {
                                Id = id,
                                IsActive = new Random().Next(0, 9) % 2 == 0,
                                IsImportant = row % 2 == 0,

                                UserName = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                                Category = worksheet.Cells[row, 5].Value?.ToString().Trim(),

                                Comment = worksheet.Cells[row, 8].Value?.ToString().Trim(),
                                Fund = worksheet.Cells[row, 7].Value?.ToString().Trim(),

                                EditComment = "Test",
                                AttachmentCount = new Random().Next(0, 9).ToString(),

                                Extra1 = "Extra1",
                                Extra2 = "Extra2",
                                Extra3 = "Extra3",
                                Extra4 = "Extra4",
                                Extra5 = "Extra5",
                                Extra6 = "Extra6",
                                Extra7 = "Extra7",
                                Extra8 = "Extra8",
                                Extra9 = "Extra9",
                                Extra10 = "Extra10",
                                Extra11 = "Extra11",
                                Extra12 = "Extra12",
                                Extra13 = "Extra13",
                                Extra14 = "Extra14",
                                Extra15 = "Extra15",
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

        //[HttpPut]
        //public IActionResult Update_AutoValidate(int key, string values)
        //{
        //    var note = Notes.FirstOrDefault(a => a.Id == key);
        //    JsonConvert.PopulateObject(values, note);

        //    if (!TryValidateModel(note))
        //    {
        //        return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
        //    }
        //    return Ok();
        //}

        [HttpPut]
        public IActionResult Update_ManualValidate(int key, string values)
        {
            //map values (json) from UI to model
            var model = new SpecialNote();
            JsonConvert.PopulateObject(values, model);

            //get note by id/key (from real data)
            var note = Notes.FirstOrDefault(a => a.Id == key);
            if (note == null)
            {
                //invalid id/key
                return Forbid();
            }

            //manual validate
            if (note.CreatedDate.Year < 2021 && !string.IsNullOrWhiteSpace(model.Fund))
            {
                ModelState.AddModelError("Fund", "if CreatedDate < 2021, Fund must be null.");
                return BadRequest("if CreatedDate < 2021, Fund must be null. yyy");
                ///return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
            }

            //if passed validate

            //update note by model
            note.Category = model.Category;
            note.Comment = model.Comment;
            note.Fund = model.Fund;

            return Ok();
        }

        [Route("SpecialNote/ValidateComment")]
        public IActionResult ValidateComment(string Comment = null)
        {
            if (string.IsNullOrWhiteSpace(Comment) || Comment.Length < 10)
            {
                return Json("Comment must have min length of 10");
            }
            return Json(true);
        }


        [Route("SpecialNote/ValidateFund")]
        public IActionResult ValidateFund(string Fund = null)
        {
            if (string.IsNullOrWhiteSpace(Fund) || Fund.Length < 10)
            {
                return Json("Fund must have min length of 10");
            }
            return Json(true);
        }
    }
}