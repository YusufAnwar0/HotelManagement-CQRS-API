using Domain.Enums;
using FluentValidation;

namespace Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.roleId).NotEmpty().WithMessage("Role Id Can't be Empty").WithState(_ => ErrorCode.RoleRequired);
        }
    }
}
