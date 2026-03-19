using Application.DTOs.ValidationDTOs;

namespace Application.Interface
{
    public interface IValidationResponse
    {
        void AddValidationErrors(IEnumerable<ValidationErrorDto> errors); 
    }
}
