using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Mission.Entities.Models
{
    public class UpdateUserRequestModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public IFormFile? ProfileImage { get; set; } = null;
    }
}
