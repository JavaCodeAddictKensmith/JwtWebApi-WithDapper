using AutoMapper;
using JWTWebApiONE.DTO;
using JWTWebApiONE.Models;

namespace JWTWebApiONE.MappingProfile
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserResponseDTO>();
        }
    }
}
