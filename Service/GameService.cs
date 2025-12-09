using Courseworkoop.Entities;
using Courseworkoop.Repository;
using Courseworkoop.Service.Base;
using Courseworkoop.Service.DTO;

namespace Courseworkoop.Service;

public class GameService : IGameService
{
    private readonly GameRepository _gameRepository;
    private readonly UserRepository _userRepository;

    public GameService(GameRepository gameRepository, UserRepository userRepository)
    {
        _gameRepository = gameRepository;
        _userRepository = userRepository;
    }

    public int GetBotMove(char[] board)
    {
        // Проста логіка бота: шукаємо першу вільну клітинку або рандом
        var emptyIndices = new List<int>();
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] != 'X' && board[i] != 'O') emptyIndices.Add(i);
        }

        if (emptyIndices.Count == 0) return -1;
        Random rnd = new Random();
        return emptyIndices[rnd.Next(emptyIndices.Count)];
    }

    public string CheckWinner(char[] arr)
    {
        // Горизонталі
        if (arr[0] == arr[1] && arr[1] == arr[2]) return arr[0].ToString();
        if (arr[3] == arr[4] && arr[4] == arr[5]) return arr[3].ToString();
        if (arr[6] == arr[7] && arr[7] == arr[8]) return arr[6].ToString();
        // Вертикалі
        if (arr[0] == arr[3] && arr[3] == arr[6]) return arr[0].ToString();
        if (arr[1] == arr[4] && arr[4] == arr[7]) return arr[1].ToString();
        if (arr[2] == arr[5] && arr[5] == arr[8]) return arr[2].ToString();
        // Діагоналі
        if (arr[0] == arr[4] && arr[4] == arr[8]) return arr[0].ToString();
        if (arr[2] == arr[4] && arr[4] == arr[6]) return arr[2].ToString();

        return null; // Немає переможця
    }

    public GameResultDto ProcessGameResult(int userId, string resultState)
    {
        int ratingChange = 0;
        string message = "";
        bool isWin = false;
        bool isDraw = false;

        if (resultState == "Win")
        {
            ratingChange = 25;
            message = "Перемога! +25 до рейтингу.";
            isWin = true;
        }
        else if (resultState == "Loss")
        {
            ratingChange = -20;
            message = "Поразка. -20 до рейтингу.";
        }
        else
        {
            ratingChange = 5;
            message = "Нічия. +5 до рейтингу.";
            isDraw = true;
        }

        // Оновлюємо рейтинг користувача (робота з User Repo через Id)
        var user = _userRepository.GetById(userId);
        if (user != null)
        {
            user.Rating += ratingChange;
            _userRepository.Update(user); // Викликаємо Update згідно CRUD
        }

        // Зберігаємо історію
        var history = new GameHistoryEntity
        {
            PlayerId = userId,
            Result = resultState,
            RatingChange = ratingChange,
            Date = DateTime.Now
        };
        _gameRepository.Add(history);

        return new GameResultDto
        {
            IsWin = isWin,
            IsDraw = isDraw,
            RatingChanged = ratingChange,
            Message = message
        };
    }

    public List<GameHistoryDto> GetUserHistory(int userId)
    {
        var histories = _gameRepository.GetByUserId(userId);
        return histories.Select(h => new GameHistoryDto
        {
            Result = h.Result,
            RatingChange = h.RatingChange,
            Date = h.Date
        }).OrderByDescending(x => x.Date).ToList();
    }
}