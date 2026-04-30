using DTO;
using Models;

namespace DataAcces.Mappers;

public static class RegisterMapper
{
    public static User ToEntity(this RegisterDto registerDto)
    {
        if (registerDto == null)
        {
            return null;
        }

        return new User
        {
            UserName = registerDto.UserName,
            UserEmail = registerDto.UserEmail,
            UserPhoneNumber = registerDto.UserPhoneNumber,
            Password = registerDto.Password,
        };
    }
}