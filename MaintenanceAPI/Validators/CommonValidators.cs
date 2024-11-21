using DataAccess.Dto.Request;
using FluentValidation;

namespace MaintenanceAPI.Validators
{
    public class CommonValidators : AbstractValidator<ReqDto>
    {
        public CommonValidators()
        {
            RuleFor(d => d.p_flag).NotNull().NotEmpty().WithMessage("Flag is required");
        }
    }
}
