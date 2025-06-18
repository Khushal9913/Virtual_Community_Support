using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Entities;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository : IMissionRepository
    {
        private readonly MissionDbContext _dbContext;
        public MissionRepository(MissionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<MissionRequestViewModel>> GetAllMissionAsync()
        {
            return _dbContext.Missions.Where(m => !m.IsDeleted).Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionThemeName = m.MissionTheme.ThemeName,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).ToListAsync();
        }

        public async Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return await _dbContext.Missions.Where(m => m.Id == id && !m.IsDeleted).Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddMission(Missions mission)
        {
            _dbContext.Missions.Add(mission);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<MissionDetailResponseModel>> GetClientSideMissionList(int userId)
        {
            var dateToCompare = DateTime.Now.Date.AddDays(-1);
            return await _dbContext.Missions
                .Where(m => !m.IsDeleted)
                .Select(m => new MissionDetailResponseModel()
                {
                    Id = m.Id,
                    MissionTitle = m.MissionTitle,
                    MissionDescription = m.MissionDescription,
                    CountryId = m.CountryId,
                    CityId = m.CityId,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    TotalSheets = m.TotalSheets,
                    RegistrationDeadLine = m.RegistrationDeadLine,
                    MissionThemeId = m.MissionThemeId,
                    MissionSkillId = m.MissionSkillId,
                    MissionImages = m.MissionImages,
                    CountryName = m.Country.CountryName,
                    CityName = m.City.CityName,
                    MissionThemeName = m.MissionTheme.ThemeName,
                    MissionSkillName = string.Join(",", _dbContext.MissionSkills
                        .Where(ms => m.MissionSkillId.Contains(ms.Id.ToString()))
                        .Select(ms => ms.SkillName)
                        .ToList()),
                    MissionStatus = m.RegistrationDeadLine < dateToCompare ? "Closed" : "Available",
                    MissionApplyStatus = _dbContext.MissionApplications.Any(ma => ma.MissionId == m.Id && ma.UserId == userId) ? "Applied" : "Apply",
                    MissionApproveStatus = _dbContext.MissionApplications.Any(ma => ma.MissionId == m.Id && ma.UserId == userId && ma.Status) ? "Approved" : "Applied",
                    MissionDateStatus = m.EndDate <= dateToCompare ? "MissionEnd" : "MissionRunning",
                    MissionDeadLineStatus = m.RegistrationDeadLine <= dateToCompare ? "Closed" : "Running",
                }).ToListAsync();
        }

        public async Task<bool> UpdateMission(MissionRequestViewModel mission)
        {
            var missionInDb = _dbContext.Missions.Find(mission.Id);

            if (missionInDb == null)
                return false;

            missionInDb.MissionTitle = mission.MissionTitle;
            missionInDb.MissionDescription = mission.MissionDescription;
            missionInDb.CityId = mission.CityId;
            missionInDb.CountryId = mission.CountryId;
            missionInDb.MissionSkillId = mission.MissionSkillId;
            missionInDb.MissionThemeId = mission.MissionThemeId;
            missionInDb.MissionImages = $"{missionInDb.MissionImages},{mission.MissionImages}";
            missionInDb.StartDate = mission.StartDate;
            missionInDb.EndDate = mission.EndDate;
            missionInDb.TotalSheets = mission.TotalSeats;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMission(int missionId)
        {
            var missionInDb = _dbContext.Missions.Find(missionId);

            if (missionInDb == null)
                return false;

            missionInDb.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<(HttpStatusCode statusCode, string message)> ApplyMission(ApplyMissionRequestModel model)
        {
            try
            {
                var missionInDb = _dbContext.Missions.Find(model.MissionId);

                if (missionInDb == null)
                    return (HttpStatusCode.NotFound, "Not Found");

                if (missionInDb.TotalSheets == 0)
                    return (HttpStatusCode.BadRequest, "Seats not available!");

                var missionApplication = new MissionApplication()
                {
                    MissionId = model.MissionId,
                    UserId = model.UserId,
                    AppliedDate = model.AppliedDate
                };

                _dbContext.MissionApplications.Add(missionApplication);

                missionInDb.TotalSheets--;

                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw;
            }

            return (HttpStatusCode.OK, "Success");
        }

        public async Task<List<MissionApplicationResponseModel>> GetMissionApplicationList()
        {
            return await _dbContext.MissionApplications.Select(ma => new MissionApplicationResponseModel()
            {
                Id = ma.Id,
                MissionId = ma.MissionId,
                MissionTitle = ma.Missions.MissionTitle,
                MissionTheme = ma.Missions.MissionTheme.ThemeName,
                AppliedDate = ma.AppliedDate,
                Status = ma.Status,
                UserName = $"{ma.User.FirstName} {ma.User.LastName}"
            }).ToListAsync();
        }

        public async Task<bool> MissionApplicationApprove(int missionApplicationId, bool newStatus)
        {
            var missionApplication = await _dbContext.MissionApplications
                .Include(ma => ma.Missions)
                .FirstOrDefaultAsync(ma => ma.Id == missionApplicationId);

            if (missionApplication == null)
                return false;

            var mission = missionApplication.Missions;

            if (mission == null)
                return false;

            // Approved → Rejected = +1 seat
            if (missionApplication.Status == true && newStatus == false)
            {
                mission.TotalSheets = (mission.TotalSheets ?? 0) + 1;
            }

            // Rejected → Approved = -1 seat
            if (missionApplication.Status != true && newStatus == true)
            {
                if ((mission.TotalSheets ?? 0) > 0)
                {
                    mission.TotalSheets--;
                }
                else
                {
                    return false; // Cannot approve if no seats available
                }
            }

            missionApplication.Status = newStatus;
            missionApplication.Status = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> MissionApplicationDelete(int missionApplicationId)
        {
            var missionApplication = await _dbContext.MissionApplications
                .Include(ma => ma.Missions)
                .FirstOrDefaultAsync(ma => ma.Id == missionApplicationId);

            if (missionApplication == null)
                return false;

            var mission = missionApplication.Missions;

            if (mission != null)
            {
                // No matter if approved or rejected, deleting an application frees a seat
                mission.TotalSheets = (mission.TotalSheets ?? 0) + 1;
            }

            _dbContext.MissionApplications.Remove(missionApplication);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
