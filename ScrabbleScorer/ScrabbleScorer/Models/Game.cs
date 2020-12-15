using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ScrabbleScorer.Models
{
    public class Game
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        [Ignore]
        public List<Player> Players { get; set; }        
    }
}