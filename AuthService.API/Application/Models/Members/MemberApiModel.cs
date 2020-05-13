using Newtonsoft.Json;
using System;

namespace AuthService.API.Application.Models.Members
{
    public class MemberApiModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string PostCode { get; set; }
    }
}
