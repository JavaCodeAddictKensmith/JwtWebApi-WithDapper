using JWTWebApiONE.Models;

namespace JWTWebApiONE.Interfaces
{
    public interface IHelpers
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public string CreateToken(User user);
    }
}
