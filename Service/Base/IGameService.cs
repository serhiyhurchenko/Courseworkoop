using Courseworkoop.Service.DTO;

namespace Courseworkoop.Service.Base;

public interface IGameService
{
    GameResultDto ProcessGameResult(int userId, string resultState); // resultState: "Win", "Loss", "Draw"
    
    int GetBotMove(char[] board);
    
    string CheckWinner(char[] board);
        
    List<GameHistoryDto> GetUserHistory(int userId);
}