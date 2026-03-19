using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;

namespace Presentation.Attributes
{
    public class HasPermissionAttribute : TypeFilterAttribute
    {
        public Permissions Permission { get; }
        public HasPermissionAttribute(Permissions permission) : base(typeof(PermissionAuthorizationFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}
