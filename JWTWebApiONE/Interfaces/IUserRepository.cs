using JWTWebApiONE.DTO;
using JWTWebApiONE.Models;

namespace JWTWebApiONE.Interfaces
{
    public interface IUserRepository
    {
        public string RegisterAUser(UserRegisterDTO userRegisterDTO);
        public string LoginUser(UserLoginDTO userLoginDTO);
        public List<User> GetAllUsers();
    }
}
