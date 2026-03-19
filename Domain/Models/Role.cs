namespace Domain.Models
{
    public class Role : BaseModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value?.ToUpperInvariant().Trim();
        }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();

    }
}