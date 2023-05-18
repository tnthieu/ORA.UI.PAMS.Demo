using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using ORA.UI.PAMS.Demo.Models;
using System.Xml.Linq;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ORA_UI_PAMS_Demo.Controllers
{
    [Route("api/[controller]")]
    public partial class SpecialNoteController : Controller
    {
        static List<SpecialNote> Notes = new List<SpecialNote>();
        private IHostingEnvironment Environment;

        public SpecialNoteController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            if (Notes.Count == 0)
            {
                var filePath = Environment.ContentRootPath + @"\XLSX\Attachments.xlsx";
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

                                Number = new Random().Next(0, 99),

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

        //auto validate
        //[HttpPut]
        //public IActionResult Update(int key, string values)
        //{
        //    var note = Notes.FirstOrDefault(a => a.Id == key);
        //    JsonConvert.PopulateObject(values, note);

        //    if (!TryValidateModel(note))
        //    {
        //        return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
        //    }
        //    return Ok();
        //}

        //manual validate
        [HttpPut]
        public IActionResult Update(int key, string values)
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
            if (note.CreatedDate.Year < 2021 && string.IsNullOrWhiteSpace(model.Fund))
            {
                return BadRequest("Fund is required when created year is less than 2021");
            }

            //if passed validate

            //update note by model
            note.Category = model.Category;
            note.Comment = model.Comment;
            note.Fund = model.Fund;
            note.Number = model.Number;

            return Ok();
        }
    }

    [Route("api/[controller]")]
    public partial class SpecialNoteValidateController : Controller
    {
        [Route("Row")]
        [HttpPost]
        public object Row([FromBody] SpecialNote note)
        {
            if (note.CreatedDate.Year < 2021 && string.IsNullOrWhiteSpace(note.Fund))
            {
                return Json(new { isValid = false, data = "Fund is required when created year is less than 2021" });
            }
            return Json(new { isValid = true });
        }

        //[Route("Comment")]
        //[HttpGet]
        //public IActionResult Comment(string Comment)
        //{
        //    if (string.IsNullOrWhiteSpace(Comment) || Comment.Length < 10)
        //    {
        //        return Json("Comment must have min length of 10");
        //    }
        //    return Json(true);
        //}        
    }
}