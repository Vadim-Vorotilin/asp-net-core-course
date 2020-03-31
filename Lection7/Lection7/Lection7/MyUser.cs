using Microsoft.AspNetCore.Identity;

namespace Lection7
{
    public class MyUser : IdentityUser
    {
        public string LastName { get; set; }
    }
}