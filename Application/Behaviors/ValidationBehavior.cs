using Application.DTOs.ResponseDTOs;
using Application.DTOs.ValidationDTOs;
using Application.Interface;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : IValidationResponse, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request)));

            var faliuers = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (faliuers.Count != 0)
            {
                var errors = faliuers.Select(e => new ValidationErrorDto
                {
                    Code = e.CustomState is ErrorCode code ? (int)code : (int)ErrorCode.ValidationError,
                    Message = e.ErrorMessage
                }).ToList();

                var response = new TResponse();
                response.AddValidationErrors(errors);

                return response;

            }

            return await next();
        }
    }
}
