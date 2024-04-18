using TennisScoring.Interfaces;
namespace TennisScoring.Models;
public class MatchScore
{   
    public int PlayerASets { get; set; } = 0;
    public int PlayerBSets { get; set; } = 0;
}

public delegate void MatchWinnerSetEventHandler(object sender, string winner);

public class TennisMatchScoreList(int setsToWin) : IMatchScoreList
{
    public event MatchWinnerSetEventHandler? MatchWinnerSet;
    public List<MatchScore> Scores { get; private set; } = [new MatchScore()];

    private bool IsMatchOver = false;

    private readonly int SetsToWin = setsToWin;

    public void StartNewMatch()
    {
        CheckWinnerAndSetWinnerIfAny();
        if(!IsMatchOver)
        {
            Scores.Add(new MatchScore());
        }
    }   
    public void AddCurMatchSetToPlayerA()
    {
        Scores.Last().PlayerASets++;
    }
    public void AddCurMatchSetToPlayerB()
    {
        Scores.Last().PlayerBSets++;
    }

    public void SetList(List<MatchScore> scores)
    {
        Scores = scores;
    }

    public void CheckWinnerAndSetWinnerIfAny()
    {
        if(Scores.Count > 1)
        {
            //check if player A has won 2 matches
            if(Scores.Where(x => x.PlayerASets >= SetsToWin).Count() == SetsToWin)
            {
                IsMatchOver = true;
                OnMatchWinnerSet("A");
               
            }
            //check if player B has won 2 matches
            else if(Scores.Where(x => x.PlayerBSets >= SetsToWin).Count() == SetsToWin)
            {
                IsMatchOver = true;
                OnMatchWinnerSet("B");
            }
        }
    }
    protected virtual void OnMatchWinnerSet(string winner)
    {
        MatchWinnerSet?.Invoke(this, winner);
    }
    public void ResetMatchList()
    {
        Scores = [new MatchScore()];
        IsMatchOver = false;
    }
}


