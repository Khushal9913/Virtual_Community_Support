using System.Net;
using Mission.Entities;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        Task<List<MissionRequestViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(Missions mission);

        Task<List<MissionDetailResponseModel>> GetClientSideMissionList(int userId);
        Task<bool> UpdateMission(MissionRequestViewModel mission);
        Task<bool> DeleteMission(int missionId);

        Task<(HttpStatusCode statusCode, string message)> ApplyMission(ApplyMissionRequestModel model);

        Task<List<MissionApplicationResponseModel>> GetMissionApplicationList();

        Task<bool> MissionApplicationApprove(int missionApplicationId, bool newStatus);

        Task<bool> MissionApplicationDelete(int missionApplicationId);
    }
}
