using Mission.Services.IServices;
using System;
using Mission.Repositories.IRepositories;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Repositories.Helpers;

namespace Mission.Services
{
    public class LoginService(ILoginRepository loginRepository, JwtService jwtService) : ILoginService
    {
        private readonly ILoginRepository _loginRepository = loginRepository;
        private readonly JwtService _jwtService = jwtService;
        ResponseResult result = new ResponseResult();

        public ResponseResult LoginUser(LoginUserRequestModel model)
        {
            var userObj = UserLogin(model);

            if (userObj.Message.ToString() == "Login Successfully")
            {
                result.Message = userObj.Message;
                result.Result = ResponseStatus.Success;
                result.Data = _jwtService.GenerateToken(userObj.Id.ToString(), userObj.FirstName, userObj.LastName, userObj.PhoneNumber, userObj.EmailAddress, userObj.UserType, userObj.UserImage);
            }
            else
            {
                result.Message = userObj.Message;
                result.Result = ResponseStatus.Error;
            }
            return result;
        }

        public LoginUserResponseModel UserLogin(LoginUserRequestModel model)
        {
            return _loginRepository.LoginUser(model);
        }

        public async Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest)
        {
            return await _loginRepository.RegisterUser(registerUserRequest);
        }

        public UserResponseModel LoginUserDetailById(int id)
        {
            return _loginRepository.LoginUserDetailById(id);
        }
        public async Task<string> UpdateUser(UpdateUserRequestModel model, string imageUploadPath)
        {
            return await _loginRepository.UpdateUser(model, imageUploadPath);
        }
    }
}
