using Microsoft.AspNetCore.Authorization;

namespace RadiostationWeb.Data
{
    public class AuthorizeRolesAttribute:AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }

}
