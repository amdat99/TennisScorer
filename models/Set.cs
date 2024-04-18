namespace TennisScoring.Models;
public class Set(MatchScoreList matchScoreList)
{
    public int PlayerASets { get; set; }
    public int PlayerBSets { get; set; }
    private readonly MatchScoreList _matchScoreList = matchScoreList;

    public void AddSetToPlayerA()
    {
        PlayerASets++;
        _matchScoreList.AddCurMatchSetToPlayerA();
        CheckIfSetWon();
     }

    public void AddSetToPlayerB()
    {
        PlayerBSets++;
        _matchScoreList.AddCurMatchSetToPlayerB();
        CheckIfSetWon();
    }
    public void CheckIfSetWon()
    {
        if (PlayerASets >= 6 && PlayerASets >= PlayerBSets + 2)
        {
            _matchScoreList.StartNewMatch();
            ResetSets();
        }
        else if (PlayerBSets >= 6 && PlayerBSets >= PlayerASets + 2)
        {
            _matchScoreList.StartNewMatch();
            ResetSets();
        }
    }
    public void ResetSets()
    {
        PlayerASets = 0;
        PlayerBSets = 0;
    }
}