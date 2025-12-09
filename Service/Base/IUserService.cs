using Courseworkoop.Service.DTO;

namespace Courseworkoop.Service.Base;

public interface IUserService
{
    void Register(string username, string password);
    UserDto Login(string username, string password);
    UserDto GetUserInfo(int userId);
}