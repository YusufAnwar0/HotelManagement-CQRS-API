using Domain.Enums;
using FluentValidation;

namespace Application.Features.Roles.Commands.AssignPermissions
{
    public class AssignPermissionsCommandValidator : AbstractValidator<AssignPermissionsCommand>
    {
        public AssignPermissionsCommandValidator()
        {
            RuleFor(x => x.roleId).NotEmpty().WithMessage("Role Can't be empty").WithState(_ => ErrorCode.RoleRequired);
            RuleFor(x => x.Permissions).NotEmpty().WithMessage("Permissions Can't be empty").WithState(_ => ErrorCode.PermissionRequired);
            RuleForEach(x => x.Permissions).IsInEnum().WithMessage("One or more provided permissions are invalid or do not exist.").WithState(_ => ErrorCode.PermissionNotFound);
        }
    }
    
}
