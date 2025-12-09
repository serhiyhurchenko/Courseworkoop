namespace Courseworkoop.Service.DTO;

public class GameHistoryDto
{
    public string Result { get; set; }
    public int RatingChange { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        string sign = RatingChange > 0 ? "+" : "";
        return $"{Date:yyyy-MM-dd HH:mm} | Result: {Result,-5} | Rating: {sign}{RatingChange}";
    }
}