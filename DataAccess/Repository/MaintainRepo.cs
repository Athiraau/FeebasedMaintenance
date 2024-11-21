using Dapper;
using Dapper.Oracle;
using DataAccess.Context;
using DataAccess.Contracts;
using DataAccess.Dto;
using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MaintainRepo : IMaintenanceRepo
    {
        private readonly DapperContext _context;
        // private readonly PostResDto _postResDto;
        private readonly DtoWrapper _dtoWrapper;

        public MaintainRepo(DapperContext context, DtoWrapper dto)
        {
            _context = context;
            _dtoWrapper = dto;
        }

        //get method
        public async Task<dynamic> GetMaintenanceReport(string flag, string pagevalue, string paravalue)
        {
            // var ErrorStat = "";
            //var ErrorMsg = "";
            OracleRefCursor result = null;
            var procedureName = "proc_feebased_maintenance";
            var parameters = new OracleDynamicParameters();
            parameters.Add("p_flag", flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_pageval", pagevalue, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_paraval", paravalue, OracleMappingType.NVarchar2, ParameterDirection.Input);

            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);
            //   parameters.Add("p_ErrorStat", ErrorStat, OracleMappingType.Int32, ParameterDirection.Output);
            //   parameters.Add("p_ErrorMsg", ErrorMsg, OracleMappingType.NVarchar2, ParameterDirection.Output);



            parameters.BindByName = true;
            using var connection = _context.CreateConnection();
            var response = await connection.QueryAsync<dynamic>
                (procedureName, parameters, commandType : CommandType.StoredProcedure);
            return response;
           


        }

        public async Task<dynamic> PostMaintenanceReport(ReqDto reqDto)
        {
            OracleRefCursor result = null;
            var result1 = " ";
            var result2 = " "; 
            var procedureName = "proc_MTSS_POST";
            var parameters = new OracleDynamicParameters();

            parameters.Add("p_flag", reqDto.p_flag, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_pageval", reqDto.p_pagevalue, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_paraval", reqDto.p_paravalue, OracleMappingType.NVarchar2, ParameterDirection.Input);
            parameters.Add("p_ErrorStat",result1, OracleMappingType.NVarchar2, ParameterDirection.Output);
            parameters.Add("p_ErrorMsg", result2, OracleMappingType.NVarchar2, ParameterDirection.Output);
            parameters.Add("p_as_outresult", result, OracleMappingType.RefCursor, ParameterDirection.Output);

            parameters.BindByName = true;
            using var connection = _context.CreateConnection();
            var response = await connection.QueryAsync<dynamic>
                (procedureName, parameters, commandType: CommandType.StoredProcedure);
            return response;


        }
    }
}
