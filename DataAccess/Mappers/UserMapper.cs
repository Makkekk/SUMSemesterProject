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
            CompanyName = user.CustomerCompany?.CompanyName ?? "Ingen virksomhed"
        };
    }
}