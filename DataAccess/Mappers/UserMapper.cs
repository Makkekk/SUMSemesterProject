using DTO;
using Models;

namespace DataAcces.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User user)
    {
        if (user == null)
            return null;

        return new UserDto
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserEmail = user.UserEmail,
            UserPhoneNumber = user.UserPhoneNumber,
            CompanyId = user.CompanyId,
            IsAdmin = user.IsAdmin,
        };
    }

    public static User ToEntity(this UserDto userDto)
    {
        if (userDto == null)
            return null;

        return new User
        {
            UserId = userDto.UserId == Guid.Empty ? Guid.NewGuid() : userDto.UserId,
            UserName = userDto.UserName,
            UserEmail = userDto.UserEmail,
            UserPhoneNumber = userDto.UserPhoneNumber,
            CompanyId = userDto.CompanyId,
        };
    }
}
