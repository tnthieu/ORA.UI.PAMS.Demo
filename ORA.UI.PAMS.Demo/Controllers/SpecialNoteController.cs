using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using ORA.UI.PAMS.Demo.Library;
using ORA.UI.PAMS.Demo.Models;
using System.Xml.Linq;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ORA_UI_PAMS_Demo.Controllers
{
    [Route("api/[controller]/[action]")]
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

                            //SubmissionMethod
                            data.SubmissionMethod += "Email%";
                            if (Library.RandomBool())
                                data.SubmissionMethod += "Portal%";
                            if (Library.RandomBool())
                                data.SubmissionMethod += "Postal Mail%";
                            data.SubmissionMethod = Library.RemoveLast(data.SubmissionMethod, "%");

                            //SitePurpose
                            data.SitePurpose += "Billing%";
                            if (Library.RandomBool())
                                data.SitePurpose += "Dunning%";
                            data.SitePurpose = Library.RemoveLast(data.SitePurpose, "%");

                            //Contacts
                            if (Library.RandomBool())
                                data.Contacts += "Email: " + new Random().Next(100, 999) + "@gmail.com<br/>Name: mr. " + new Random().Next(1000000, 9999999) + "<br/>Phone: " + new Random().Next(1000000, 9999999) + "<br/>%";
                            if (Library.RandomBool())
                                data.Contacts += "Email: " + new Random().Next(100, 999) + "@gmail.com<br/>Name: mr. " + new Random().Next(1000000, 9999999) + "<br/>Phone: " + new Random().Next(1000000, 9999999) + "<br/>%";
                            if (Library.RandomBool())
                                data.Contacts += "Email: " + new Random().Next(100, 999) + "@gmail.com<br/>Name: mr. " + new Random().Next(1000000, 9999999) + "<br/>Phone: " + new Random().Next(1000000, 9999999) + "<br/>%";
                            data.Contacts = Library.RemoveLast(data.Contacts, "%");
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
            //get note by id/key (from real data)
            var note = Notes.FirstOrDefault(a => a.Id == key);
            if (note == null)
            {
                //invalid id/key
                return Forbid();
            }

            var model = JsonConvert.DeserializeObject<SpecialNote>(JsonConvert.SerializeObject(note));
            JsonConvert.PopulateObject(values, model);

            var validate = ValidateNote(model);
            if (!validate.isValid)
            {
                return BadRequest(validate.data);
            }

            //if passed validate
            JsonConvert.PopulateObject(values, note);
            return Ok();
        }

        [HttpPost]
        public object ValidateRow([FromBody] SpecialNote note)
        {
            var validate = ValidateNote(note);
            return Json(validate);
        }

        Validate ValidateNote(SpecialNote note)
        {
            var result = new Validate();

            if (note.CreatedDate.Year < 2021 && string.IsNullOrWhiteSpace(note.Fund))
            {
                result.isValid = false;
                result.data += "Fund is required when created year is less than 2021";
            }

            if (note.CreatedDate.Year < 2022 && string.IsNullOrWhiteSpace(note.Fund))
            {
                result.isValid = false;
                result.data += "Fund is required when created year is less than 2022";
            }

            return result;
        }

        //[HttpGet]
        //public IActionResult ValidateComment(string Comment)
        //{
        //    if (string.IsNullOrWhiteSpace(Comment) || Comment.Length < 10)
        //    {
        //        return Json("Comment must have min length of 10");
        //    }
        //    return Json(true);
        //}  
    }
}