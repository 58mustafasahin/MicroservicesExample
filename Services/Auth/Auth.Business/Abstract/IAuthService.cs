using Auth.DAL.Dto.Role;
using Auth.DAL.Dto.User;
using JWTStructure.Entity;
using JWTStructure.LoginSecurity.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.Business.Abstract
{
    public interface IAuthService
    {
        Task<List<GetRoleDto>> GetRoles();
        Task<Guid> AddRole(string name);
        Task<List<GetUserDto>> GetUsers();
        Task<int> Register(UserRegisterDto userRegisterDto);
        Task<User> GetLoginUser(UserLoginDto userLoginDto);
        Task<AccessToken> CreateAccessToken(User user);

        ///// <summary>
        /////     Kullanıcı şifre yenileme
        ///// </summary>
        ///// <param name="resetPasswordDto">şifre ve userId</param>
        ///// <returns>SaveChanges sonucu</returns>
        //Task<int> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
