using Business.Contracts;
using Business.Helpers;
using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using DataAccess.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;

namespace MaintenanceAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/Maintenance")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        private readonly MaintainRepo _maintenanceRepo;
        private readonly ILoggerService _logger;
        private readonly IServiceWrapper _service;
        private readonly HelperWrapper _helper;
        private readonly DtoWrapper _dto;


        public MaintenanceController(MaintainRepo maintenanceRepo, ILoggerService logger, IServiceWrapper service, HelperWrapper helper, DtoWrapper dto)
        {
            _maintenanceRepo = maintenanceRepo;
            _logger = logger;
            _dto = dto;
            _service = service;
            _helper = helper;

        }

        [HttpGet("GetMaintenanceData/{flag}/{pagevalue}/{paravalue}", Name = "GetMaintenanceData")]
        public async Task<dynamic> GetMaintenanceData([FromRoute] string flag, string pagevalue, string paravalue)
        {

            ResDto _resDto = new ResDto();
            var data = await _service.maintenanceService.GetReport(flag, pagevalue, paravalue);
            if (data == null)
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();
            }
            else
            {
                  _resDto.Result = data;

                return Ok(JsonConvert.SerializeObject(_resDto));

            }
        }

        [HttpPost("PostMaintenance", Name = "PostMaintenance")]
        public async Task<IActionResult> PostMaintenance([FromBody] ReqDto reqDto)
        {
            ResDto _dto = new ResDto();
            var data = await _service.maintenanceService.PostReport(reqDto);
            if (data == null)
            {
                _logger.LogError($"Details of filter data could not be returned in db.");

                return NotFound();
            }
            else
            {
                   _dto.Result = data;
                return Ok(JsonConvert.SerializeObject(_dto));

            }

        }
    }
}
