using Domain.Enums;
namespace Domain.Models
{
    public class RolePermission : BaseModel
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Permissions Permission { get; set; }
    }
}