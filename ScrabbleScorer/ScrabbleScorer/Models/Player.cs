using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrabbleScorer.Models
{
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public int FinalScore { get; set; }
        [Ignore]
        public List<int> Scores { get; set; }
        [Ignore]
        public int TotalScore
        {
            get { return (Scores != null) ? Scores.Sum() : 0; }
        }
    }
}