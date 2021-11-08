using GlaucomaWay.Models;
using Microsoft.AspNetCore.Identity;

namespace GlaucomaWay.Users
{
    public class User : IdentityUser
    {
        public PatientModel Patient { get; set; }
    }
}
