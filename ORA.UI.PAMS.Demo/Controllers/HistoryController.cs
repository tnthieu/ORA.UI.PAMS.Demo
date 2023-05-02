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
    public partial class HistoryController : Controller
    {
        static List<History> Histories = new List<History>();

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            if (Histories.Count == 0)
            {
                for (var i = 0; i < 2; i++)
                {
                    Histories.Add(new History
                    {
                        Id = 1000 + i + new Random().Next(999,9999),
                        ModifiedDate = new DateTime(2021, 1, 1).AddDays(new Random().Next(1, 365)).AddHours(new Random().Next(1, 23)).AddMinutes(new Random().Next(1, 59)),
                        UserName = "Yunchong Lee",
                        Comment = "Modified Fund from \"57087\" to \"None\"",
                    });
                    Histories.Add(new History
                    {
                        Id = 1000 + i + new Random().Next(999, 9999),
                        ModifiedDate = new DateTime(2021, 1, 1).AddDays(new Random().Next(1, 365)).AddHours(new Random().Next(1, 23)).AddMinutes(new Random().Next(1, 59)),
                        UserName = "Yunchong Lee",
                        Comment = "Uploaded: Attachment Name: test.txt",
                    });
                    Histories.Add(new History
                    {
                        Id = 1000 + i + new Random().Next(999, 9999),
                        ModifiedDate = new DateTime(2021, 1, 1).AddDays(new Random().Next(1, 365)).AddHours(new Random().Next(1, 23)).AddMinutes(new Random().Next(1, 59)),
                        UserName = "Yunchong Lee",
                        Comment = "Category: Budget, Important: False, Fund: 57087",
                    });
                }
            }
            return DataSourceLoader.Load(Histories, loadOptions);
        }
    }
}