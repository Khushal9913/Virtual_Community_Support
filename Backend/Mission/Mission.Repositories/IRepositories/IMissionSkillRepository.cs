using Mission.Entities.Models;
using System.Collections.Generic;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionSkillRepository
    {
        List<MissionSkillResponseModel> GetMissionSkillList();

        MissionSkillResponseModel GetMissionSkillById(int id);

        string AddMissionSkill(AddMissionSkillRequestModel model);

        string UpdateMissionSkill(UpdateMissionSkillRequestModel model);

        string DeleteMissionSkill(int id);
    }
}
