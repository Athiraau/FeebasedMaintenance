using Business.Contracts;
using Business.Helpers;
using DataAccess.Dto;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly MaintainRepo _maintainRepo;
        private IMaintenanceService _service;
        private readonly DtoWrapper _dto;
        private IJwtUtils _jwtutils;
        private readonly ILoggerService _loggerService;
        private IConfiguration _config;
        private readonly CommonValidationHelper _commonValidationHelper;


        public ServiceWrapper(MaintainRepo maintainRepo, IMaintenanceService service, DtoWrapper dto, IJwtUtils jwtutils, ILoggerService loggerService, IConfiguration config, CommonValidationHelper commonValidationHelper)
        {
            _maintainRepo = maintainRepo;
            _service = service;
            _dto = dto;
            _jwtutils = jwtutils;
            _loggerService = loggerService;
            _config = config;
            _commonValidationHelper = commonValidationHelper;
        }

        public IJwtUtils JwtUtils
        {
            get
            {
                if (_jwtutils == null)
                {
                    _jwtutils = new JwtUtils(_config, _loggerService);
                }
                return _jwtutils;
            }
        }

        public IMaintenanceService maintenanceService
        {
            get
            {
                if (_service == null)
                {
                    _service = new MaintenanceService(_maintainRepo, _dto, _config);

                }
                return _service;
            }
        }
    }
}
