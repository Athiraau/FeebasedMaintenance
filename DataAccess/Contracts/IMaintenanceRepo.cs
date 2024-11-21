using DataAccess.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IMaintenanceRepo
    {
        public Task<dynamic> GetMaintenanceReport(string flag, string pagevalue, string paravalue);
        public  Task<dynamic> PostMaintenanceReport(ReqDto reqDto);
    }
}
