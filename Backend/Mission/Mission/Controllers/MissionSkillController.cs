using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;
using Mission.Services.Services;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionSkillController(IMissionSkillService missionSkillService) : ControllerBase
    {
        private readonly IMissionSkillService _missionSkillService = missionSkillService;
        ResponseResult result = new ResponseResult();

        [HttpGet]
        [Route("GetMissionSkillList")]
        [Authorize]
        public ResponseResult GetMissionSkillList()
        {
            try
            {
                result.Data = _missionSkillService.GetMissionSkillList();
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpGet]
        [Route("GetMissionSkillById/{id}")]
        [Authorize]
        public ResponseResult GetMissionSkillById(int id)
        {
            try
            {
                result.Data = _missionSkillService.GetMissionSkillById(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("AddMissionSkill")]
        [Authorize]
        public ResponseResult AddMissionSkill(AddMissionSkillRequestModel model)
        {
            try
            {
                result.Data = _missionSkillService.AddMissionSkill(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpPost]
        [Route("UpdateMissionSkill")]
        [Authorize]
        public ResponseResult UpdateMissionSkill(UpdateMissionSkillRequestModel model)
        {
            try
            {
                result.Data = _missionSkillService.UpdateMissionSkill(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        [HttpDelete]
        [Route("DeleteMissionSkill/{id}")]
        [Authorize]
        public ResponseResult DeleteMissionSkill(int id)
        {
            try
            {
                result.Data = _missionSkillService.DeleteMissionSkill(id);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
