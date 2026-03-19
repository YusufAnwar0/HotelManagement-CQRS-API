using Domain.Enums;
using FluentValidation;

namespace Application.Features.Users.Commands.AssignRole
{
    public class AssignRoleCommandValidator : AbstractValidator<AssignRoleCommand>
    {
        public AssignRoleCommandValidator()
        {
            RuleFor(x => x.roleId).NotEmpty().WithMessage("User Id can't be empty").WithState(_ => ErrorCode.RoleRequired);
            RuleFor(x => x.userId).NotEmpty().WithMessage("Role Id can't be empty").WithState(_ => ErrorCode.UserRequired);
        }
    }
}
