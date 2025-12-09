using Courseworkoop.Entities;
using Courseworkoop.Repository;
using Courseworkoop.Service.Base;
using Courseworkoop.Service.Mappers;
using Courseworkoop.Service.DTO;

namespace Courseworkoop.Service;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Register(string username, string password)
    {
        if (_userRepository.GetByUsername(username) != null)
        {
            throw new Exception("Користувач вже існує.");
        }

        var user = new UserEntity
        {
            Username = username,
            Password = password,
            Rating = 1000 // Початковий рейтинг
        };
        _userRepository.Add(user);
    }

    public UserDto Login(string username, string password)
    {
        var user = _userRepository.GetByUsername(username);
        if (user != null && user.Password == password)
        {
            return UserMapper.ToDto(user);
        }
        return null;
    }

    public UserDto GetUserInfo(int userId)
    {
        return UserMapper.ToDto(_userRepository.GetById(userId));
    }
}