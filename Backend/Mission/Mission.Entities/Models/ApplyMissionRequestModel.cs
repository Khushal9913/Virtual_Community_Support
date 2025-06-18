namespace Mission.Entities.Models
{
    public class ApplyMissionRequestModel
    {
        public int MissionId { get; set; }
        public int UserId { get; set; }
        public DateTime AppliedDate { get; set; }
    }
}
