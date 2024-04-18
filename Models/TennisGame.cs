using TennisScoring.Enums;
using TennisScoring.Interfaces;
namespace TennisScoring.Models;
public class TennisGame : IGame
{   
    public IPlayer PlayerA { get; set; }
    public IPlayer PlayerB { get; set; }
    private readonly TennisMatchScoreList _matchScoreList;
    private readonly TennisSet _TennisSet;

    public string? ErrorMessage { get; private set; } = null;
    public TennisGame(string playerAName, string playerBName, TennisMatchScoreList matchScoreList)
    {
        PlayerA = new TennisPlayer { Name = playerAName };
        PlayerB = new TennisPlayer { Name = playerBName };
        _matchScoreList = matchScoreList;
        _matchScoreList.MatchWinnerSet += OnMatchWinnerSet;
        _TennisSet = new TennisSet(_matchScoreList);
    }
    public List<MatchScore> GetMatchScores() => _matchScoreList.Scores;
    public int? GetPlayerASets() => _TennisSet.PlayerASets;
    public int? GetPlayerBSets() => _TennisSet.PlayerBSets;
    public string? Winner { get; private set; } = null;
    private void OnMatchWinnerSet(object sender, string winner)
    {
        Winner = winner switch
        {
            "A" => PlayerA.Name,
            "B" => PlayerB.Name,
            _ => null
        };
    }
   
    public void AddPointToPlayer(IPlayer player, string playerType)
    {

        if( Winner != null) return;
    
        IPlayer CurPlayer = player;
        var CurrentSets = playerType == "A" ? _TennisSet.PlayerASets : _TennisSet.PlayerBSets;
        var CurrentScoreList = _matchScoreList.Scores;

        try
        {
            player.Points++;
            player.TennisScore = SetTennisScoreFromPoints(player.Points, playerType);
            CheckIfGameWon();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            //Rollback changes if an exception occurs
            RollbackStateChanges(CurPlayer, CurrentSets, CurrentScoreList, playerType); 
            ErrorMessage = "An error occured while adding point to player";
            Task.Delay(5000).ContinueWith(_ => ErrorMessage = null);
        }
    }

    private void RollbackStateChanges(IPlayer player, int currentSets, List<MatchScore> CurrentScoreList, string playerType)
    {
        if( playerType == "A")
        {
            PlayerA = player;
            _TennisSet.PlayerASets = currentSets;
        } 
        else
        {
            PlayerB = player;
            _TennisSet.PlayerBSets = currentSets;
        }

        _matchScoreList.SetList(CurrentScoreList);
    }

    private void CheckIfGameWon()
    {
        if (PlayerA.Points >= 4 && PlayerA.Points >= PlayerB.Points + 2)
        {
            _TennisSet.AddSetToPlayerA();
            ResetPoints();
        }
        else if (PlayerB.Points >= 4 && PlayerB.Points >= PlayerA.Points + 2)
        {
            _TennisSet.AddSetToPlayerB();
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
                return TennisGameScore.Duece.ToString();
            }
            else if(playerType  == "A")
            {
                return PlayerA.Points > PlayerB.Points ? TennisGameScore.Advantage.ToString() : TennisGameScore.Duece.ToString();
            }
            else
            {
                return PlayerB.Points > PlayerA.Points ? TennisGameScore.Advantage.ToString() : TennisGameScore.Duece.ToString();
            }
        }
        
        return points switch
        {
            0 => "0",
            1 => "15",
            2 =>  "30",
            3 => "40",
            _ =>  ""
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
        _TennisSet.ResetSets();
        _matchScoreList.ResetMatchList();
        Winner = null;
    }
}