using Dapper;
using JWTWebApiONE.DTO;
using JWTWebApiONE.Interfaces;
using JWTWebApiONE.Models;
using System.Data.SqlClient;

namespace JWTWebApiONE.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHelpers _helpers;
        private readonly string conn;

        public UserRepository(IConfiguration configuration, IHelpers helpers)
        {
            _configuration = configuration;
            _helpers = helpers;
            conn = _configuration.GetSection("ConnectionStrings:JWTREGISTER").Value;
        }
        public string LoginUser(UserLoginDTO userLoginDTO)
        {
            string message = string.Empty;
            List<User> users = new List<User>();
            User currentUser = new User();
            SqlConnection connection = new SqlConnection(conn);
            users = connection.Query<User>("Select * From  UserRegister").ToList();
            currentUser = users.FirstOrDefault(x => x.Email == userLoginDTO.Email);
                if (currentUser == null)
                {
                    message = "No User Found";
                    return message;
                }         
            if(!_helpers.VerifyPasswordHash(userLoginDTO.Password, currentUser.PasswordHash, currentUser.PasswordSalt))
            {
                message = "Wrong password";
                return message;
            }
            var token = _helpers.CreateToken(currentUser);
            return token.ToString();
            
        }

        public string RegisterAUser(UserRegisterDTO userRegisterDTO)
        {
            string message = string.Empty;
            User user = new User();
            _helpers.CreatePasswordHash(userRegisterDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Id = Guid.NewGuid().ToString();
            user.UserName = userRegisterDTO.UserName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Email = userRegisterDTO.Email;
            SqlConnection connection = new SqlConnection(conn);
            int i = connection.Execute("Insert into UserRegister(Id, UserName, PasswordHash, PasswordSalt, Email) Values(@Id, @UserName, @PasswordHash, @PasswordSalt, @Email)", user);
            if (i > 0)
            {
                message = "User Added";
            }
            else
            {
                message = "Error";
            }
            return message;
        }
        public List<User> GetAllUsers()
        {    
            SqlConnection connection = new SqlConnection(conn);
            var Users = connection.Query<User>("Select * From UserRegister");  
            return Users.ToList();
        }
    }
}
