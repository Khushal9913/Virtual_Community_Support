using System.Text.Json.Serialization;

namespace Mission.Entities.Models
{
    public class MissionRequestViewModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string MissionTitle { get; set; }
        public int MissionThemeId { get; set; }
        public string MissionThemeName { get; set; } = string.Empty;
        public string MissionDescription { get; set; }

        [JsonPropertyName("totalSheets")]
        public int TotalSeats { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string MissionImages { get; set; }
        public string MissionSkillId { get; set; }
    }
}
