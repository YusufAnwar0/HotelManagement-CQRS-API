namespace Domain.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        private string _email;
        public string Email
        {
            get => _email;
            set => _email = value?.Trim().ToLowerInvariant();
        }
        public string PasswordHash { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}