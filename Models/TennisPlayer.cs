using TennisScoring.Interfaces;

namespace TennisScoring.Models;
    public class TennisPlayer : IPlayer
    {
        public string Name { get; set; } = "";
        public int Points { get; set; } = 0;
        public string? TennisScore { get; set; } = "0";
    }