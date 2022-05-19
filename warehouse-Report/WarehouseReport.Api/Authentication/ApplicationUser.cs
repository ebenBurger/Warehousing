using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace WarehouseReport.Api.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}