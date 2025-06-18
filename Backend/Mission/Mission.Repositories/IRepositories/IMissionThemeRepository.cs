using Mission.Entities.Entities;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionThemeRepository
    {
        Task<List<MissionThemeViewModel>> GetAllMissionTheme();

        Task<MissionThemeViewModel?> GetMissionThemeById(int missionThemeId);

        Task<bool> AddMissionTheme(MissionTheme missionTheme);

        Task<bool> UpdateMissionTheme(MissionTheme missionTheme);

        Task<bool> DeleteMissionTheme(int missionThemeId);
    }
}
