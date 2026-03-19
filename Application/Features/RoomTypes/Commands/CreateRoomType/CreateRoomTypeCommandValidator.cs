using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.Features.RoomTypes.Commands.CreateRoomType
{
    public class CreateRoomTypeCommandValidator : AbstractValidator<CreateRoomTypeCommand>
    {

        public CreateRoomTypeCommandValidator()
        {
            RuleFor(rt => rt.Code).NotEmpty().WithMessage("RoomType Code Can't be empty").WithState(_ => ErrorCode.RoomTypeCodeRequired);
            RuleFor(rt => rt.Name).NotEmpty().WithMessage("RoomType Name Can't be empty").WithState(_ => ErrorCode.RoomTypeNameRequired);
            RuleFor(rt => rt.Description).NotEmpty().WithMessage("RoomType Description Can't be empty").WithState(_ => ErrorCode.RoomTypeDescriptionRequired);
            RuleFor(rt => rt.BasePrice).NotEmpty().WithMessage("RoomType BasePrice Can't be empty").WithState(_ => ErrorCode.RoomTypeBasePriceRequired);
            RuleFor(rt => rt.MaxCapacity).NotEmpty().WithMessage("RoomType MaxCapacity Can't be empty").WithState(_ => ErrorCode.RoomTypeMaxCapacityRequired);
        }
    }
}
