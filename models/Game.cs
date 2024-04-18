namespace TennisScoring.Models;

public class Game
{   
     public Player PlayerA { get; set; }
    public Player PlayerB { get; set; }
    private readonly MatchScoreList _matchScoreList;
    private readonly Set _set;
    public Game(string playerAName, string playerBName, MatchScoreList matchScoreList)
    {
        PlayerA = new Player { Name = playerAName };
        PlayerB = new Player { Name = playerBName };
        _matchScoreList = matchScoreList;
        _set = new Set(_matchScoreList);
    }
    public List<MatchScore> GetMatchScores()
    {
        return _matchScoreList.Scores;
    }

    public int GetPlayerASets()
    {
        return _set.PlayerASets;
    }

    public int GetPlayerBSets()
    {
        return _set.PlayerBSets;
    }

    public string? GetWinner()
    {
        if(_matchScoreList.WinnerPlayer == null) return null;
        return _matchScoreList.WinnerPlayer == "A" ? PlayerA.Name : PlayerB.Name;
    }
  
    public void AddPointToPlayer(Player player, string playerType)
    {

        if(_matchScoreList.WinnerPlayer != null) return;
    
        var CurPlayer = player;
        var CurrentSets = playerType == "A" ? _set.PlayerASets : _set.PlayerBSets;
        var CurrentScoreList = _matchScoreList.Scores;

        try
        {
            player.Points++;
            player.TennisScore = SetTennisScoreFromPoints(PlayerA.Points, playerType);
            CheckIfGameWon();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            RollbackStateChanges(CurPlayer, CurrentSets, CurrentScoreList, playerType); 
        }
    }

    private void RollbackStateChanges(Player player, int CurrentSets, List<MatchScore> CurrentScoreList, string playerType)
    {
        if( playerType == "A")
        {
            PlayerA = player;
            _set.PlayerASets = CurrentSets;
        
        } 
        else
        {
            PlayerB = player;
            _set.PlayerBSets = CurrentSets;
        }

        _matchScoreList.SetList(CurrentScoreList);
    }

    public void CheckIfGameWon()
    {
        if (PlayerA.Points >= 4 && PlayerA.Points >= PlayerB.Points + 2)
        {
            _set.AddSetToPlayerA();
            ResetPoints();
        }
        else if (PlayerB.Points >= 4 && PlayerB.Points >= PlayerA.Points + 2)
        {
            _set.AddSetToPlayerB();
            ResetPoints();
        }
    }

    private string SetTennisScoreFromPoints(int points, string playerType)
    {
        //If both players have 3 points or more, check if the score is duece or advantage
        if(PlayerA.Points >= 3 && PlayerB.Points >= 3)
        {
            if(PlayerA.Points == PlayerB.Points)
            {
                SetDuece();
                return "Deuce";
            }
            else if(playerType  == "A")
            {
                return PlayerA.Points > PlayerB.Points ? "Advantage" : "Deuce";
            }
            else
            {
                return PlayerB.Points > PlayerA.Points ? "Advantage" : "Deuce";
            }
        }
        
        return points switch
        {
            0 => "0",
            1 => "15",
            2 => "30",
            3 => "40",
            _ => "",
        };
    }
    
    //When the score is 40-40, set the prevoius 40 score to Deuce
    private void SetDuece()
    {          
        PlayerA.TennisScore = "Deuce";
        PlayerB.TennisScore = "Deuce";
    }
    private void ResetPoints()
    {
        PlayerA.Points = 0;
        PlayerB.Points = 0;
        PlayerA.TennisScore = "0";
        PlayerB.TennisScore = "0";
    }

    public void ResetAll()
    {
       ResetPoints();
         _set.ResetSets();
         _matchScoreList.ResetMatchList();
         _matchScoreList.WinnerPlayer = null;
    }
}