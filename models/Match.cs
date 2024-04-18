namespace TennisScoring.Models;
public class MatchScore
{   
    public int PlayerASets { get; set; } = 0;
    public int PlayerBSets { get; set; } = 0;
}

public class MatchScoreList
{
    public List<MatchScore> Scores { get; private set; } = [];

    public string? WinnerPlayer { get; set; } = null;

    public MatchScoreList()
    {
        Scores = [new MatchScore()];
    }
    public void StartNewMatch()
    {
        CheckWinnerAndSetWinnerIfAny();

        if(WinnerPlayer == null)
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
        if(Scores.Count >= 2)
        {
            //check if player A has won 2 matches
            if(Scores.Where(x => x.PlayerASets >= 2).Count() == 2)
            {
                WinnerPlayer = "A";
               
            }
            //check if player B has won 2 matches
            else if(Scores.Where(x => x.PlayerBSets >= 2).Count() == 2)
            {
                WinnerPlayer = "B";
            }
        }
    }
    public void ResetMatchList()
    {
        Scores = [new MatchScore()];
    }
}


