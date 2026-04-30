using DTO;
using Models;

namespace DataAcces.Mappers;

public static class RegisterMapper
{
    public static User ToEntity(this RegisterUserDto registerDto)
    {
        if (registerDto == null)
        {
            return null;
        }

        return new User
        {
            UserName = registerUserDto.UserName,
            UserEmail = registerUserDto.UserEmail,
            UserPhoneNumber = registerUserDto.UserPhoneNumber,
            Password = registerUserDto.Password,
        };
    }
}