using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using ORA.UI.PAMS.Demo.Models;

namespace ORA_UI_PAMS_Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    public partial class LogController : ControllerBase
    {
        public void LogClientInfo([FromBody] ClientInfo clientInfo)
        {
            var ip = clientInfo.geoplugin_request;
        }
    }
}