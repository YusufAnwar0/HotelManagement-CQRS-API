using Domain.Enums;
using FluentValidation;

namespace Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.userId).NotEmpty().WithMessage("Role Id can't be empty").WithState(_ => ErrorCode.UserRequired);
        }
    }
}
