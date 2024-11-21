using DataAccess.Dto.Request;
using DataAccess.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dto
{
    public class DtoWrapper
    {
        public ReqDto _ReqDto;
        public ResDto _ResDto;
        


        public ReqDto ReqDto
        {
            get
            {
                if (_ReqDto == null)
                {
                    _ReqDto = new ReqDto();
                }
                return _ReqDto;
            }
        }


        public ResDto ResDto
        {
            get
            {
                if (_ResDto == null)
                {
                    _ResDto = new ResDto();
                }
                return _ResDto;
            }
        }

        
    }
}
