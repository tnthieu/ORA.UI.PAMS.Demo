using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ORA_UI_PAMS_Demo.Models;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace ORA_UI_PAMS_Demo.Controllers
{

    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {

            for (var i = 0; i < 100; i++)
            {
                SampleData.Orders.Add(new SampleOrder
                {
                    IsImportant = i % 2 == 0,

                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,

                    UserName = "Test",
                    Category = "Test",

                    Comment = "Test",
                    Fund = new Random().Next(1000, 9999).ToString(),
                    AttachmentName = "Test",

                    EditComment = "Test",
                });
            }

            return DataSourceLoader.Load(SampleData.Orders, loadOptions);
        }

    }
}