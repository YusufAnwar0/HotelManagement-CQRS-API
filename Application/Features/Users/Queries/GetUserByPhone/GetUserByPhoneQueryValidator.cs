using Domain.Enums;
using FluentValidation;

namespace Application.Features.Users.Queries.GetUserByPhone
{
    public class GetUserByPhoneQueryValidator : AbstractValidator<GetUserByPhoneQuery>
    {
        public GetUserByPhoneQueryValidator()
        {
            RuleFor(x => x.phoneNumber).NotEmpty().WithMessage("Phone number can't be empty").WithState(_ => ErrorCode.PhoneNumberRequired);
        }
    }
}
