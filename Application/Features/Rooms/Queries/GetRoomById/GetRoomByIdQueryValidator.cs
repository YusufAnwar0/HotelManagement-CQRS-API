using Domain.Enums;
using Domain.IRepositories;
using Domain.Models;
using FluentValidation;

namespace Application.Features.Rooms.Queries.GetRoomById
{
    public class GetRoomByIdQueryValidator : AbstractValidator<GetRoomByIdQuery>
    {

        public GetRoomByIdQueryValidator()
        {
            RuleFor(x => x.roomId).NotEmpty().WithMessage("Room Id Can't be Empty").WithState(_ => ErrorCode.RoomIdRequired);
        }
    }
}
