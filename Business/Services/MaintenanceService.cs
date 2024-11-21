using Business.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
   public class MaintenanceService : IMaintenanceService
    {
        private readonly MaintainRepo _repo;
        //   private readonly PostResDto _postResDto;
        private readonly DtoWrapper _dtoWrapper;
        private readonly IConfiguration _config;

        public MaintenanceService(MaintainRepo repo, DtoWrapper dtoWrapper, IConfiguration config)
        {
            _repo = repo;
            // _postResDto = postResDto;
            _dtoWrapper = dtoWrapper;
            _config = config;
        }

        public async Task<dynamic> GetReport(string flag, string pagevalue, string paravalue)
        {
            var res = await _repo.GetMaintenanceReport(flag, pagevalue, paravalue);
            // _resDto.result = res;
            return res;

        }

        public async Task<dynamic> PostReport(ReqDto reqDto)
        {
            var res = await _repo.PostMaintenanceReport(reqDto);
            // _resDto.result = res;
            return res;

        }

    }
}
