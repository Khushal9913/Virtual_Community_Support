using Mission.Entities.Models.CommonModels;
using Mission.Repositories.IRepositories;
using Mission.Entities.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mission.Repositories.Repositories
{
    public class CommonRepository(MissionDbContext cIDbContext) : ICommonRepository
    {
        private readonly MissionDbContext _cIDbContext = cIDbContext;

        public List<DropDownResponseModel> CountryList()
        {
            var countries = _cIDbContext.Country
                .OrderBy(c => c.CountryName)
                .Select(c => new DropDownResponseModel(c.Id, c.CountryName))
                .ToList();

            return countries;
        }

        public List<DropDownResponseModel> CityList(int countryId)
        {
            var cities = _cIDbContext.City
                .Where(c => c.CountryId == countryId)
                .OrderBy(c => c.CityName)
                .Select(c => new DropDownResponseModel(c.Id, c.CityName))
                .ToList();

            return cities;
        }

        public List<DropDownResponseModel> MissionCountryList()
        {
            var countries = _cIDbContext.Missions
                .Select(m => new DropDownResponseModel(m.CountryId, m.Country.CountryName))
                .Distinct()
                .ToList();

            return countries;
        }

        public List<DropDownResponseModel> MissionCityList()
        {
            var cities = _cIDbContext.Missions
                .Where(m => !m.IsDeleted)
                .Select(m => new DropDownResponseModel(m.CityId, m.City.CityName))
                .Distinct()
                .ToList();

            return cities;
        }

        public List<DropDownResponseModel> MissionThemeList()
        {
            var missionThemes = _cIDbContext.MissionThemes
                .Where(mt => mt.Status == "active")
                .Select(mt => new DropDownResponseModel(mt.Id, mt.ThemeName))
                .Distinct()
                .ToList();

            return missionThemes;
        }

        public List<DropDownResponseModel> MissionSkillList()
        {
            var missionSkill = _cIDbContext.MissionSkills
                .Where(ms => ms.Status == "active")
                .Select(ms => new DropDownResponseModel(ms.Id, ms.SkillName))
                .ToList();

            return missionSkill;
        }

        public List<DropDownResponseModel> MissionTitleList()
        {
            var missionSkill = _cIDbContext.Missions
                .Where(m => !m.IsDeleted)
                .Select(m => new DropDownResponseModel(m.Id, m.MissionTitle))
                .ToList();

            return missionSkill;
        }

    }
}
