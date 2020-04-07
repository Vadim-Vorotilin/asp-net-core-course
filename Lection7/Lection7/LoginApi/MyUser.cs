using Microsoft.AspNetCore.Identity;

namespace LoginApi
{
    public class MyUser : IdentityUser
    {
        public string LastName { get; set; }
    }
}