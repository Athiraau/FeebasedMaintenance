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
    public class CommonValidationHelper
    {
        private readonly ErrorResponse _error;
        private readonly DtoWrapper _dto;
        private readonly IValidator<ReqDto> _DtoValidator;

        public CommonValidationHelper(ErrorResponse error, DtoWrapper dto, IValidator<ReqDto> DtoValidator)
        {
            _error = error;
            _dto = dto;
            _DtoValidator = DtoValidator;

        }

        public async Task<ErrorResponse> ValidateFlag(string flag)   //commonly using for all FLAG validation
        {
            ErrorResponse errorRes = null;

            _dto.ReqDto.p_flag = flag;
            var validationResult = await _DtoValidator.ValidateAsync(_dto.ReqDto);

            errorRes = ReturnErrorRes(validationResult);

            return errorRes;
        }



        public ErrorResponse ReturnErrorRes(FluentValidation.Results.ValidationResult Res)

        {
            List<string> errors = new List<string>();
            foreach (var row in Res.Errors.ToArray())
            {
                errors.Add(row.ErrorMessage.ToString());
            }
            _error.errorMessage = errors;
            return _error;
        }
    }
}
