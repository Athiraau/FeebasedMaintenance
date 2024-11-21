using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IMaintenanceService
    {
        public Task<dynamic> GetReport(string flag, string pagevalue, string paravalue);
        public Task<dynamic> PostReport(ReqDto reqDto);
    }
}
