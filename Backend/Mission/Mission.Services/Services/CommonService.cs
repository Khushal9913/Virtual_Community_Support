using Mission.Entities.Models.CommonModels;
using Mission.Repositories.IRepositories;
using Mission.Service.IServices;
using System.Collections.Generic;

namespace Mission.Service.Services
{
    public class CommonService(ICommonRepository commonRepository) : ICommonService
    {
        private readonly ICommonRepository _commonRepository = commonRepository;

        public List<DropDownResponseModel> CountryList()
        {
            return _commonRepository.CountryList();
        }

        public List<DropDownResponseModel> CityList(int countryId)
        {
            return _commonRepository.CityList(countryId);
        }

        public List<DropDownResponseModel> MissionCountryList()
        {
            return _commonRepository.MissionCountryList();
        }

        public List<DropDownResponseModel> MissionCityList()
        {
            return _commonRepository.MissionCityList();
        }

        public List<DropDownResponseModel> MissionThemeList()
        {
            return _commonRepository.MissionThemeList();
        }

        public List<DropDownResponseModel> MissionSkillList()
        {
            return _commonRepository.MissionSkillList();
        }

        public List<DropDownResponseModel> MissionTitleList()
        {
            return _commonRepository.MissionTitleList();
        }

    }
}
