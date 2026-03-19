using Domain.Enums;
using FluentValidation;

namespace Application.Features.Roles.Commands.UnassignPermission
{
    public class UnassignPermissionCommandValidator : AbstractValidator<UnassignPermissionCommand>
    {
        public UnassignPermissionCommandValidator()
        {
            RuleFor(x => x.roleId).NotEmpty().WithMessage("Role Can't be empty").WithState(_ => ErrorCode.RoleRequired);
            RuleFor(x => x.Permission).NotEmpty().WithMessage("Permissions Can't be empty").WithState(_ => ErrorCode.PermissionRequired);
        }
    }
}
