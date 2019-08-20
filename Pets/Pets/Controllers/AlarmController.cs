using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pets.Data.Model;

namespace Pets.Controllers
{
    [Route("big-car/app/[controller]")]
    public class AlarmController : Controller
    {
        //使用SSL时，无法Post，了解原因。
        private readonly ILogger _logger;
        public AlarmController(ILogger<AlarmController> logger)
        {
            this._logger = logger;
        }

        [HttpGet("Get")]
        public ActionResult<string> Get()
        {
            return "Hello";
        }

        [HttpPost("Save")]
        public Rsp Save([FromBody]IEnumerable<HEZDAlarmData> datas)
        {
            var alarms = datas.ToList();
            _logger.LogInformation($"收到报警：{alarms.Count}条");
            _logger.LogInformation($"报警信息：{JsonConvert.SerializeObject(alarms)}");
            return new Rsp()
            {
                code = 0,
                msg = "success",
            };
        }
    }
}