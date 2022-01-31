using Auth.Business.Abstract;
using Auth.DAL.Context;
using Auth.DAL.Dto.Role;
using Auth.DAL.Dto.User;
using Auth.DAL.Entity;
using JWTStructure.Entity;
using JWTStructure.LoginSecurity.Entity;
using JWTStructure.LoginSecurity.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IAuthDbContext _authDbContext;
        private readonly ITokenHelper _tokenHelper;

        public AuthService(IAuthDbContext authDbContext, ITokenHelper tokenHelper)
        {
            _authDbContext = authDbContext;
            _tokenHelper = tokenHelper;
        }

        public async Task<List<GetRoleDto>> GetRoles()
        {
            return await _authDbContext.Roles.Where(p => !p.IsDeleted)
                .Select(p => new GetRoleDto
                {
                    RoleId = p.Id,
                    Name = p.Name,
                }).ToListAsync();
        }

        public async Task<Guid> AddRole(string name)
        {
            var role = new Role
            {
                Name = name,
            };
            _authDbContext.Roles.Add(role);
            await _authDbContext.SaveChangesAsync();
            return role.Id;
        }

        public async Task<List<GetUserDto>> GetUsers()
        {
            return await _authDbContext.Users.Where(p => !p.IsDeleted)
                .Select(p => new GetUserDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Surname = p.Surname,
                    Email = p.Email,
                    Username = p.Username,
                    Roles = p.UserRoles.Select(p => new GetRoleDto
                    {
                        RoleId = p.RoleId,
                        Name = p.Role.Name,
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<int> Register(UserRegisterDto userRegisterDto)
        {
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out var passwordHash, out var passwordSalt);
            var user = new UserEntity
            {
                Name = userRegisterDto.Name,
                Surname = userRegisterDto.Surname,
                Username = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                CreatedUserId = new Guid("53bc3e1f-0622-4e36-3aef-08d9c9f3d747"),
                UserRoles = new List<UserRole>
                {
                    new() {RoleId=userRegisterDto.RoleId}
                }
            };
            await _authDbContext.Users.AddAsync(user);
            return await _authDbContext.SaveChangesAsync();
        }

        public async Task<User> GetLoginUser(UserLoginDto userLoginDto)
        {
            var currentUser = await _authDbContext.Users
                .Where(p => !p.IsDeleted && p.Username == userLoginDto.Username).FirstOrDefaultAsync();
            if (currentUser == null) return null;

            var passwordMatchResult = HashingHelper.VerifyPasswordHash(userLoginDto.Password, currentUser.PasswordHash, currentUser.PasswordSalt);
            if (passwordMatchResult)
            {
                return currentUser;
            }
            else
            {
                return new UserEntity();
            }
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            var currentUserRoles = await GetUserRolesByUserId(user.Id);
            return currentUserRoles == null ? null : _tokenHelper.CreateToken(user, currentUserRoles);
        }

        private async Task<IEnumerable<Role>> GetUserRolesByUserId(Guid id)
        {
            return await _authDbContext.UserRoles.Where(p => !p.IsDeleted && p.UserId == id)
                .Include(p => p.Role).Select(p => p.Role).ToListAsync();
        }
    }
}
