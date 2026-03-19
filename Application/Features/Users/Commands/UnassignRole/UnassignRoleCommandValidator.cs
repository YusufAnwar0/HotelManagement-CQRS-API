using Domain.Enums;
using FluentValidation;

namespace Application.Features.Users.Commands.UnassignRole
{
    public class UnassignRoleCommandValidator : AbstractValidator<UnassignRoleCommand>
    {
        public UnassignRoleCommandValidator()
        {
            RuleFor(x => x.roleId).NotEmpty().WithMessage("User Id can't be empty").WithState(_ => ErrorCode.RoleRequired);
            RuleFor(x => x.userId).NotEmpty().WithMessage("Role Id can't be empty").WithState(_ => ErrorCode.UserRequired);

        }
    }
}
