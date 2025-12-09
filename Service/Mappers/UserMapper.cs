using Courseworkoop.Entities;
using Courseworkoop.Service.DTO;

namespace Courseworkoop.Service.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(UserEntity entity)
    {
        if (entity == null) return null;
        return new UserDto
        {
            Id = entity.Id,
            Username = entity.Username,
            Rating = entity.Rating
        };
    }
}