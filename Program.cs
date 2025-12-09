using Courseworkoop.Data;
using Courseworkoop.Repository;
using Courseworkoop.Service;
using Courseworkoop.UI;
using System.Text;

namespace Courseworkoop;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
            
        var dbContext = TicTacToeDbContext.Instance;
            
        var userRepo = new UserRepository(dbContext);
        var gameRepo = new GameRepository(dbContext);
            
        var userService = new UserService(userRepo);
        var gameService = new GameService(gameRepo, userRepo);
            
        // Запуск UI
        var app = new ConsoleUserInterface(userService, gameService);
        app.Run();
    }
}