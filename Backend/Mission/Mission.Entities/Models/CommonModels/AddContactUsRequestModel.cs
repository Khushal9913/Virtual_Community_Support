namespace Mission.Entities.Models.CommonModels
{
    public class AddContactUsRequestModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
