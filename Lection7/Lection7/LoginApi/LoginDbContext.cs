using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LoginApi
{
    public class LoginDbContext : IdentityDbContext<MyUser>
    {
        
    }
}