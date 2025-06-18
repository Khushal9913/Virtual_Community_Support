
namespace Mission.Entities.Models
{
    public class RegisterUserRequestModel
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; } = "Admin";
    }
}
