using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using ORA.UI.PAMS.Demo.Models;

namespace ORA_UI_PAMS_Demo.Controllers
{
    [Route("api/[controller]")]
    public partial class FundController : Controller
    {
        [HttpGet]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            var Funds = new List<Category>
            {
                new Category { Id = 1, Name = "57087" },
                new Category { Id = 2, Name = "--No Fund--" },
            };
            return DataSourceLoader.Load(Funds, loadOptions);
        }
    }
}