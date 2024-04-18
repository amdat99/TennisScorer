using  TennisScoring.Models;
namespace TennisScoring.Interfaces;
public interface IGame 
{
    public void AddPointToPlayer(IPlayer player, string playerType);
    public List<MatchScore> GetMatchScores();
    public int? GetPlayerASets();
    public int? GetPlayerBSets();
    public void ResetAll();

    public IPlayer PlayerA { get; set; }

    public IPlayer PlayerB { get; set; }

    public string? Winner { get; }
}