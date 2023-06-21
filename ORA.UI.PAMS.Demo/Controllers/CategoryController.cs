using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using ORA.UI.PAMS.Demo.Models;

namespace ORA_UI_PAMS_Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    public partial class CategoryController : ControllerBase
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