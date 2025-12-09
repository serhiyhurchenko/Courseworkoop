using Courseworkoop.Service.Base;
using Courseworkoop.Service.DTO;

namespace Courseworkoop.UI;

public class ConsoleUserInterface
{
    private readonly IUserService _userService;
    private readonly IGameService _gameService;
    private UserDto _currentUser;

    public ConsoleUserInterface(IUserService userService, IGameService gameService)
    {
        _userService = userService;
        _gameService = gameService;
    }

    public void Run()
    {
        Console.Title = "Tic-Tac-Toe OOP";
        while (true)
        {
            if (_currentUser == null)
            {
                ShowAuthMenu();
            }
            else
            {
                ShowMainMenu();
            }
        }
    }

    private void ShowAuthMenu()
    {
        Console.Clear();
        Console.WriteLine("1. Вхід");
        Console.WriteLine("2. Реєстрація");
        Console.WriteLine("3. Вихід");
        Console.Write("> ");
            
        var input = Console.ReadLine();
        switch (input)
        {
            case "1": Login(); break;
            case "2": Register(); break;
            case "3": Environment.Exit(0); break;
        }
    }

    private void ShowMainMenu()
    {
        // Оновлюємо дані, щоб бачити актуальний рейтинг
        _currentUser = _userService.GetUserInfo(_currentUser.Id);

        Console.Clear();
        Console.WriteLine($"Користувач: {_currentUser.Username} | Рейтинг: {_currentUser.Rating}");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("1. Грати");
        Console.WriteLine("2. Історія ігор");
        Console.WriteLine("3. Вихід з акаунту");
        Console.Write("> ");

        var input = Console.ReadLine();
        switch (input)
        {
            case "1": PlayGameLoop(); break;
            case "2": ShowHistory(); break;
            case "3": _currentUser = null; break;
        }
    }

    private void Register()
    {
        Console.Write("Логін: ");
        string login = Console.ReadLine();
        Console.Write("Пароль: ");
        string pass = Console.ReadLine();

        try
        {
            _userService.Register(login, pass);
            Console.WriteLine("Реєстрація успішна! Натисніть Enter.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        Console.ReadKey();
    }

    private void Login()
    {
        Console.Write("Логін: ");
        string login = Console.ReadLine();
        Console.Write("Пароль: ");
        string pass = Console.ReadLine();

        var user = _userService.Login(login, pass);
        if (user != null)
        {
            _currentUser = user;
        }
        else
        {
            Console.WriteLine("Невірні дані.");
            Console.ReadKey();
        }
    }

    private void ShowHistory()
    {
        Console.Clear();
        var history = _gameService.GetUserHistory(_currentUser.Id);
        foreach (var h in history)
        {
            Console.WriteLine(h.ToString());
        }
        Console.WriteLine("\nНатисніть Enter...");
        Console.ReadKey();
    }

    private void PlayGameLoop()
    {
        char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        int moves = 0;
        bool isPlayerTurn = true;
        bool gameRunning = true;

        while (gameRunning)
        {
            DrawBoard(board);

            if (isPlayerTurn)
            {
                Console.Write("Ваш хід (1-9): ");
                if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= 9)
                {
                    if (board[idx - 1] != 'X' && board[idx - 1] != 'O')
                    {
                        board[idx - 1] = 'X';
                        moves++;
                        isPlayerTurn = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("Хід бота...");
                Thread.Sleep(500);
                int botIdx = _gameService.GetBotMove(board);
                if (botIdx != -1)
                {
                    board[botIdx] = 'O';
                    moves++;
                    isPlayerTurn = true;
                }
            }

            // Перевірка стану
            string winner = _gameService.CheckWinner(board);
            string resultState = null;

            if (winner == "X") resultState = "Win";
            else if (winner == "O") resultState = "Loss";
            else if (moves == 9) resultState = "Draw";

            if (resultState != null)
            {
                DrawBoard(board);
                var resultDto = _gameService.ProcessGameResult(_currentUser.Id, resultState);
                Console.WriteLine(resultDto.Message);
                Console.ReadKey();
                gameRunning = false;
            }
        }
    }

    private void DrawBoard(char[] arr)
    {
        Console.Clear();
        Console.WriteLine("   |   |   ");
        Console.WriteLine($" {arr[0]} | {arr[1]} | {arr[2]} ");
        Console.WriteLine("___|___|___");
        Console.WriteLine("   |   |   ");
        Console.WriteLine($" {arr[3]} | {arr[4]} | {arr[5]} ");
        Console.WriteLine("___|___|___");
        Console.WriteLine("   |   |   ");
        Console.WriteLine($" {arr[6]} | {arr[7]} | {arr[8]} ");
        Console.WriteLine("   |   |   ");
    }
}