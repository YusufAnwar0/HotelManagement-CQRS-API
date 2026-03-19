using Domain.Enums;
using FluentValidation;

namespace Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.roleName).NotEmpty().WithMessage("role name can't be empty").WithState(_ => ErrorCode.RoleRequired);
        }
    }
}
