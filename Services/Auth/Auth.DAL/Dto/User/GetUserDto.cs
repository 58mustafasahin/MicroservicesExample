using Auth.DAL.Dto.Role;
using System;
using System.Collections.Generic;

namespace Auth.DAL.Dto.User
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public ICollection<GetRoleDto> Roles { get; set; }
    }
}
