using System.Net;
using Mission.Entities.Models;

namespace Mission.Services.IServices
{
    public interface IMissionService
    {
        Task<List<MissionRequestViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(MissionRequestViewModel model);

        Task<List<MissionDetailResponseModel>> GetClientSideMissionList(int userId);
        Task<bool> UpdateMission(MissionRequestViewModel model);
        Task<bool> DeleteMission(int missionId);

        Task<(HttpStatusCode statusCode, string message)> ApplyMission(ApplyMissionRequestModel model);

        Task<List<MissionApplicationResponseModel>> GetMissionApplicationList();

        Task<bool> MissionApplicationApprove(int missionApplicationId, bool newStatus);

        Task<bool> MissionApplicationDelete(int missionApplicationId);
    }
}
