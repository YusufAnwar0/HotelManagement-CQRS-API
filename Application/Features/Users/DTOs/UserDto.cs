namespace Application.Features.Users.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> RoleNames { get; set; }
    }
}
