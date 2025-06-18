using Microsoft.EntityFrameworkCore;
using Mission.Entities.Models;
using Mission.Entities;
using Mission.Repositories.IRepositories;
using System.Data;
using Mission.Entities.Context;

namespace Mission.Repositories.Repositories
{
    public class MissionSkillRepository(MissionDbContext cIDbContext) : IMissionSkillRepository
    {
        private readonly MissionDbContext _cIDbContext = cIDbContext;

        public List<MissionSkillResponseModel> GetMissionSkillList()
        {
            var missionSkill = _cIDbContext.MissionSkills
                .Where(ms => !ms.IsDeleted)
                .Select(ms => new MissionSkillResponseModel()
                {
                    Id = ms.Id,
                    SkillName = ms.SkillName,
                    Status = ms.Status
                })
                .ToList();

            return missionSkill;
        }

        public MissionSkillResponseModel GetMissionSkillById(int id)
        {
            var missionSkillDetail = _cIDbContext.MissionSkills
                .Where(ms => ms.Id == id && !ms.IsDeleted)
                .Select(ms => new MissionSkillResponseModel()
                {
                    Id = ms.Id,
                    SkillName = ms.SkillName,
                    Status = ms.Status
                })
                .FirstOrDefault() ?? throw new Exception("Mission skill not found.");

            return missionSkillDetail;
        }

        public string AddMissionSkill(AddMissionSkillRequestModel model)
        {
            var skillExist = _cIDbContext.MissionSkills.Any(ms => ms.SkillName.ToLower() == model.SkillName.ToLower() && !ms.IsDeleted);

            if (skillExist) throw new Exception("Skill Name Already Exist.");

            var newSkill = new MissionSkill()
            {
                SkillName = model.SkillName,
                Status = model.Status,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = null,
                IsDeleted = false
            };

            _cIDbContext.MissionSkills.Add(newSkill);
            _cIDbContext.SaveChanges();

            return "Save Skill Successfully..";
        }

        public string UpdateMissionSkill(UpdateMissionSkillRequestModel model)
        {
            var skillToUpdate = _cIDbContext.MissionSkills.FirstOrDefault(ms => ms.Id == model.Id && !ms.IsDeleted) ?? throw new Exception("Mission Skill not found.");

            bool skillAlreadyExists = _cIDbContext.MissionSkills
                .Any(ms => ms.Id != model.Id
                    && ms.SkillName.ToLower() == model.SkillName.ToLower()
                    && !ms.IsDeleted);

            if (skillAlreadyExists) throw new Exception("Skill Name Already Exist.");

            skillToUpdate.SkillName = model.SkillName;
            skillToUpdate.Status = model.Status;
            skillToUpdate.ModifiedDate = DateTime.UtcNow;

            _cIDbContext.MissionSkills.Update(skillToUpdate);
            _cIDbContext.SaveChanges();

            return "Update Mission Skill Successfully..";
        }

        public string DeleteMissionSkill(int id)
        {
            _cIDbContext.MissionSkills
                .Where(ms => ms.Id == id)
                .ExecuteUpdate(ms => ms.SetProperty(property => property.IsDeleted, true));

            return "Delete Mission Skill Successfully..";
        }
    }
}
