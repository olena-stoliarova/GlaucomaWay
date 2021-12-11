using System.Text.Json.Serialization;
using GlaucomaWay.Models;
using Microsoft.AspNetCore.Identity;

namespace GlaucomaWay.Users
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public string PatientId { get; set; }

        public PatientModel Patient { get; set; }
    }
}
