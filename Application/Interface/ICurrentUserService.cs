namespace Application.Interface
{
    public interface ICurrentUserService
    {
        Guid? CurrentUserId { get; }
    }
}
