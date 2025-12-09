namespace Courseworkoop.Entities;

public class GameHistoryEntity
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public string Result { get; set; } // "Win", "Loss", "Draw"
    public int RatingChange { get; set; }
    public DateTime Date { get; set; }
}
