using JWTStructure.Entity;

namespace Auth.DAL.Entity
{
    public class UserEntity : User
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
