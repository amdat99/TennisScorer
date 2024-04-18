using TennisScoring.Models;
namespace TennisScoring.Interfaces;
public interface IMatchScoreList
{
    public List<MatchScore> Scores { get; }
    public void StartNewMatch();
    public void AddCurMatchSetToPlayerA();
    public void AddCurMatchSetToPlayerB();
    public void SetList(List<MatchScore> scores);
    public void CheckWinnerAndSetWinnerIfAny();
    public void ResetMatchList();

}