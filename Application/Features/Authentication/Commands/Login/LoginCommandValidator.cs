using Domain.Enums;
using FluentValidation;

namespace Application.Features.Authentication.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Can't be Empty").WithState(_ => ErrorCode.EmailRequired)
                        .EmailAddress().WithMessage("Invalid Email Format").WithState(_ => ErrorCode.InvalidEmailFormat);

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password Can't be Empty").WithState(_ => ErrorCode.PasswordRequired);

        }
    }
}
