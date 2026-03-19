namespace Application.Features.Users.DTOs
{
    public class UserProfileDto
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }

        public List<string> Roles { get; set; }
    }
}
