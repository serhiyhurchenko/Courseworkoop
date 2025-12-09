namespace Courseworkoop.Service.DTO;

// результат, який повертає сервіс після завершення гри
public class GameResultDto
{
    public bool IsWin { get; set; }
    public bool IsDraw { get; set; }
    public int RatingChanged { get; set; }
    public string Message { get; set; }
}