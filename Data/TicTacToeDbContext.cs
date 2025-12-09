namespace Courseworkoop.Data;

using Courseworkoop.Entities;

public class TicTacToeDbContext
{
    private static TicTacToeDbContext _instance;
    public static TicTacToeDbContext Instance => _instance ??= new TicTacToeDbContext();

    public List<UserEntity> Users { get; set; }
    public List<GameHistoryEntity> GameHistories { get; set; }

    private TicTacToeDbContext()
    {
        Users = new List<UserEntity>();
        GameHistories = new List<GameHistoryEntity>();
    }
}
