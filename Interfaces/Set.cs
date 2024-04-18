namespace TennisScoring.Interfaces;
public interface IGameSet 
{
    public int PlayerASets { get; set; }
    public int PlayerBSets { get; set; }
    public void AddSetToPlayerA();
    public void AddSetToPlayerB();
    public void CheckIfSetWon();
    public void ResetSets();
}
