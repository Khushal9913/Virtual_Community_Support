using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories
{
    public class LoginRepository(MissionDbContext cIDbContext) : ILoginRepository
    {
        private readonly MissionDbContext _cIDbContext = cIDbContext;

        public LoginUserResponseModel LoginUser(LoginUserRequestModel model)
        {
            var existingUser = _cIDbContext.User
                .FirstOrDefault(u => u.EmailAddress.ToLower() == model.EmailAddress.ToLower() && !u.IsDeleted);

            if (existingUser == null)
            {
                return new LoginUserResponseModel() { Message = "Email Address and Password Not Found." };
            }
            if (existingUser.Password != model.Password)
            {
                return new LoginUserResponseModel() { Message = "Email Address and Password Not Found." };
            }

            return new LoginUserResponseModel
            {
                Id = existingUser.Id,
                FirstName = existingUser.FirstName ?? string.Empty,
                LastName = existingUser.LastName ?? string.Empty,
                PhoneNumber = existingUser.PhoneNumber,
                EmailAddress = existingUser.EmailAddress,
                UserType = existingUser.UserType,
                UserImage = existingUser.UserImage != null ? existingUser.UserImage : string.Empty,
                Message = "Login Successfully"
            };
        }


        public async Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest)
        {
            var isEmailExist = await _cIDbContext.User.FirstOrDefaultAsync(u => u.EmailAddress.ToLower() == registerUserRequest.EmailAddress.ToLower());

            if (isEmailExist != null)  throw new Exception("User already exist");

            User user = new User()
            {
              FirstName = registerUserRequest.FirstName,
              LastName = registerUserRequest.LastName,
              EmailAddress=registerUserRequest.EmailAddress,
              Password = registerUserRequest.Password,
              PhoneNumber   =registerUserRequest.PhoneNumber,
              UserType  =registerUserRequest.UserType,
              CreatedDate = DateTime.UtcNow,
            };

            await _cIDbContext.User.AddAsync(user);
            await _cIDbContext.SaveChangesAsync();
            return "User registered!";
        }
        public UserResponseModel LoginUserDetailById(int id)
        {
            var userDetail = _cIDbContext.User
                .Where(u => u.Id == id && !u.IsDeleted)
                .Select(user => new UserResponseModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    EmailAddress = user.EmailAddress,
                    UserType = user.UserType,
                    ProfileImage = user.UserImage
                })
                .FirstOrDefault() ?? throw new Exception("User not found.");

            return userDetail;
        }
        public async Task<string> UpdateUser(UpdateUserRequestModel model, string imageUploadPath)
        {
            var existingUserDetail = _cIDbContext.User.Where(u => u.Id == model.Id && !u.IsDeleted).FirstOrDefault() ?? throw new Exception("User not found.");

            if (!string.IsNullOrEmpty(existingUserDetail.UserImage))
            {
                string oldImageFullPath = Path.Combine(imageUploadPath, existingUserDetail.UserImage.Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (File.Exists(oldImageFullPath))
                    File.Delete(oldImageFullPath);
            }

            string imagePath = await Helpers.UploadFile.SaveImageAsync(model.ProfileImage, "UploadMissionImage/Images", imageUploadPath);

            if (existingUserDetail != null)
            {
                existingUserDetail.FirstName = model.FirstName;
                existingUserDetail.LastName = model.LastName;
                existingUserDetail.PhoneNumber = model.PhoneNumber;
                existingUserDetail.ModifiedDate = DateTime.UtcNow;
                existingUserDetail.UserImage = imagePath;
                _cIDbContext.User.Update(existingUserDetail);
            }

            _cIDbContext.SaveChanges();

            return "User Updated Successfully..";
        }
    }
}
