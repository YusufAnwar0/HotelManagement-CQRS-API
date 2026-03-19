using Application.DTOs.ResponseDTOs;
using Application.Features.Offers.Commands.AddOfferToRoomType;
using Application.Features.Offers.Commands.CreateOffer;
using Application.Features.Offers.Commands.DeleteOffer;
using Application.Features.Offers.Commands.RemoveOfferFromRoomType;
using Application.Features.Offers.Commands.UpdateOffer;
using Application.Features.Offers.DTOs;
using Application.Features.Offers.Queries.GetAllOffers;
using Application.Features.Offers.Queries.GetOfferById;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly ISender _sender;

        public OffersController(ISender sender)
        {
            _sender = sender;
        }

        [HasPermission(Permissions.GetAllOffers)]
        [HttpGet]
        public async Task<ResponseDto<IEnumerable<GetOfferDto>>> GetAll()
        {
            return await _sender.Send(new GetAllOffersQuery());
        }

        [HasPermission(Permissions.GetOfferById)]
        [HttpGet("{id:guid}")]
        public async Task<ResponseDto<GetOfferDto>> GetById(Guid id)
        {
            return await _sender.Send(new GetOfferByIdQuery(id));
        }

        [HasPermission(Permissions.CreateOffer)]
        [HttpPost]
        public async Task<ResponseDto<bool>> Create([FromBody] CreateOfferCommand command)
        {
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.UpdateOffer)]
        [HttpPut("{id:guid}")]
        public async Task<ResponseDto<bool>> Update(Guid id, [FromBody] UpdateOfferCommand command)
        {
            if (id != command.Id)
            {
                return ResponseDto<bool>.Fail(Domain.Enums.ErrorCode.ValidationError, "ID mismatch");
            }
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.DeleteOffer)]
        [HttpDelete("{id:guid}")]
        public async Task<ResponseDto<bool>> Delete(Guid id)
        {
            return await _sender.Send(new DeleteOfferCommand(id));
        }

        [HasPermission(Permissions.AddOfferToRoomType)]
        [HttpPost("/offer")]
        public async Task<ResponseDto<bool>> AddOffer([FromBody] AddOfferToRoomTypeCommand command)
        {
            return await _sender.Send(command);
        }

        [HasPermission(Permissions.RemoveOfferFromRoomType)]
        [HttpDelete("/offer")]
        public async Task<ResponseDto<bool>> RemoveOffer([FromBody] RemoveOfferFromRoomTypeCommand command)
        {
            return await _sender.Send(command);
        }
    }
}

