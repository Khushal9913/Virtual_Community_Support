using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            try
            {
                var res = await _userService.DeleteUser(id);
                return Ok(new ResponseResult() { Data = "User deleted successfully.", Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to delete user." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            try
            {
                var res = await _userService.GetUserById(id);
                return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "" });
            }
            catch
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to find user." });
            }
        }

        [HttpGet]
        [Route("UserDetailList")]
        public async Task<IActionResult> GetAllUsers()
        {
            var res = await _userService.GetAllUsers();
            return Ok(new ResponseResult() { Data = res, Result = ResponseStatus.Success, Message = "" });
        }

        //[HttpPost("UpdateUser")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> UpdateUser([FromForm] UpdateUserRequestModel model)
        //{
        //    var response = new ResponseResult();

        //    try
        //    {
        //        var isUpdated = await _userService.UpdateUser(model);

        //        if (isUpdated)
        //        {
        //            response.Data = "User updated successfully.";
        //            response.Result = ResponseStatus.Success;
        //        }
        //        else
        //        {
        //            response.Result = ResponseStatus.Error;
        //            response.Message = "Failed to update user.";
        //        }

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Result = ResponseStatus.Error;
        //        response.Message = $"An error occurred while updating user: {ex.Message}";
        //        return StatusCode(500, response);
        //    }
        //}
    }
}
