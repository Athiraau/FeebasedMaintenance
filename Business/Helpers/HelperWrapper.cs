using DataAccess.Dto.Request;
using DataAccess.Dto;
using DataAccess.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public class HelperWrapper
    {
        private CommonValidationHelper _commonHelper;
        private readonly ErrorResponse _error;
        private readonly DtoWrapper _dto;
        private readonly IValidator<ReqDto> _DtoValidator;

        public HelperWrapper(CommonValidationHelper commonHelper, ErrorResponse error, DtoWrapper dto, IValidator<ReqDto> DtoValidator)


        {
            _commonHelper = commonHelper;
            _error = error;
            _dto = dto;
            _DtoValidator = DtoValidator;

        }

        public CommonValidationHelper CHelper
        {
            get
            {
                if (_commonHelper == null)
                {
                    _commonHelper = new CommonValidationHelper(_error, _dto, _DtoValidator);
                }
                return _commonHelper;
            }
        }
    }
}
