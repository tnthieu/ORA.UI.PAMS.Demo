using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using ORA.UI.PAMS.Demo.Models;

namespace ORA_UI_PAMS_Demo.Controllers
{
    [Route("api/[controller]")]
    public partial class AttachmentController : Controller
    {
        static List<Attachment> Attachments = new List<Attachment>();

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            if (Attachments.Count == 0)
            {
                for (var i = 0; i < 3; i++)
                {
                    Attachments.Add(new Attachment
                    {
                        Id = 1000 + i + new Random().Next(999, 9999),
                        Name = "Note-" + (i + 1) + ".pdf",
                        ModifiedDate = new DateTime(2021, 1, 1).AddDays(new Random().Next(1, 365)).AddHours(new Random().Next(1, 23)).AddMinutes(new Random().Next(1, 59)),
                        UserName = "Yunchong Lee",
                        Bytes = new Random().Next(9999, 99999),
                    });
                }
            }
            return DataSourceLoader.Load(Attachments, loadOptions);
        }
    }
}