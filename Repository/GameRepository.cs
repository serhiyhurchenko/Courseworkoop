using Courseworkoop.Data;
using Courseworkoop.Entities;
using Courseworkoop.Repository.Base;

namespace Courseworkoop.Repository;

    public class GameRepository : IRepository<GameHistoryEntity>
    {
        private readonly TicTacToeDbContext _context;

        public GameRepository(TicTacToeDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GameHistoryEntity> GetAll() => _context.GameHistories;

        public GameHistoryEntity GetById(int id) => _context.GameHistories.FirstOrDefault(g => g.Id == id);

        public void Add(GameHistoryEntity entity)
        {
            entity.Id = _context.GameHistories.Any() ? _context.GameHistories.Max(g => g.Id) + 1 : 1;
            _context.GameHistories.Add(entity);
        }

        public void Update(GameHistoryEntity entity)
        {
            var existing = GetById(entity.Id);
            if (existing != null)
            {
                existing.Result = entity.Result;
                existing.RatingChange = entity.RatingChange;
                existing.Date = entity.Date;
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null) _context.GameHistories.Remove(entity);
        }

        // метод для отримання ігор конкретного користувача
        public IEnumerable<GameHistoryEntity> GetByUserId(int userId)
        {
            return _context.GameHistories.Where(g => g.PlayerId == userId);
        }
    }