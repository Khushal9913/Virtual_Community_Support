using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController(IMissionService missionService) : ControllerBase
    {
        private readonly IMissionService _missionService = missionService;

        [HttpPost]
        [Route("AddMission")]
        public async Task<IActionResult> AddMission(MissionRequestViewModel model)
        {
            var response = await _missionService.AddMission(model);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpGet]
        [Route("MissionList")]
        public async Task<IActionResult> GetAllMissionAsync()
        {
            var response = await _missionService.GetAllMissionAsync();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpGet]
        [Route("MissionDetailById/{id:int}")]
        public async Task<IActionResult> GetMissionById(int id)
        {
            var response = await _missionService.GetMissionById(id);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpPost]
        [Route("UpdateMission")]
        public async Task<IActionResult> UpdateMission(MissionRequestViewModel model)
        {
            var response = await _missionService.UpdateMission(model);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpDelete]
        [Route("DeleteMission/{missionId:int}")]
        public async Task<IActionResult> DeleteMission(int missionId)
        {
            var response = await _missionService.DeleteMission(missionId);
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpGet]
        [Route("MissionApplicationList")]
        public async Task<IActionResult> MissionApplicationList()
        {
            var response = await _missionService.GetMissionApplicationList();
            return Ok(new ResponseResult() { Data = response, Result = ResponseStatus.Success, Message = "" });
        }

        [HttpPost]
        [Route("MissionApplicationApprove")]
        public async Task<IActionResult> MissionApplicationApprove(MissionApplicationResponseModel model)
        {
            if (model.Status == null)
            {
                return BadRequest(new ResponseResult
                {
                    Result = ResponseStatus.Error,
                    Message = "Status must be provided to approve or reject the mission application."
                });
            }

            var response = await _missionService.MissionApplicationApprove(model.Id, (bool)model.Status);
            return Ok(new ResponseResult
            {
                Data = response,
                Result = ResponseStatus.Success,
                Message = model.Status == true ? "Application approved." : "Application rejected."
            });
        }


        [HttpPost]
        [Route("MissionApplicationDelete")]
        public async Task<IActionResult> MissionApplicationDelete([FromBody] MissionApplicationResponseModel model)
        {
            var response = await _missionService.MissionApplicationDelete(model.Id);
            return Ok(new ResponseResult
            {
                Data = response,
                Result = ResponseStatus.Success,
                Message = "Mission application marked as rejected."
            });
        }

    }
}
