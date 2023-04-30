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

namespace ORA_UI_PAMS_Demo.Controllers
{

    [Route("api/[controller]")]
    public partial class CategoryController : Controller
    {
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var Categorys = new List<Category>
            {
                new Category { Id = 1, Name = "Correspondence" },
                new Category { Id = 2, Name = "Invoice/Report Backup" },
                new Category { Id = 3, Name = "Budget" },
            };
            return DataSourceLoader.Load(Categorys, loadOptions);
        }
    }
}