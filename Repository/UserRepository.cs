using Courseworkoop.Data;
using Courseworkoop.Entities;
using Courseworkoop.Repository.Base;

namespace Courseworkoop.Repository;

public class UserRepository : IRepository<UserEntity>
{
    private readonly TicTacToeDbContext _context;

    public UserRepository(TicTacToeDbContext context)
    {
        _context = context;
    }

    public IEnumerable<UserEntity> GetAll() => _context.Users;

    public UserEntity GetById(int id) => _context.Users.FirstOrDefault(u => u.Id == id);

    public UserEntity GetByUsername(string username) => _context.Users.FirstOrDefault(u => u.Username == username);

    public void Add(UserEntity entity)
    {
        entity.Id = _context.Users.Any() ? _context.Users.Max(u => u.Id) + 1 : 1;
        _context.Users.Add(entity);
    }

    public void Update(UserEntity entity)
    {
        var existing = GetById(entity.Id);
        if (existing != null)
        {
            existing.Username = entity.Username;
            existing.Password = entity.Password;
            existing.Rating = entity.Rating;
        }
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null) _context.Users.Remove(entity);
    }
}
