using Mission.Entities.Models;
using System.Collections.Generic;

namespace Mission.Services.IServices
{
    public interface IMissionSkillService
    {
        List<MissionSkillResponseModel> GetMissionSkillList();

        MissionSkillResponseModel GetMissionSkillById(int id);

        string AddMissionSkill(AddMissionSkillRequestModel model);

        string UpdateMissionSkill(UpdateMissionSkillRequestModel model);

        string DeleteMissionSkill(int id);
    }
}
