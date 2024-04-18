namespace TennisScoring.Interfaces;
public interface IPlayer
{
    public string Name { get; set; }
    public int Points { get; set; }
    public string? TennisScore { get; set; }
}
