using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusDefine 
{
    public struct PlayerStatus
    {
        public int [] pvpReport { get; set; }
        public int pvpRanking { get; set; }
        public int playerPower { get; set; }
        public int attackPower { get; set; }
        public int healthPoint { get; set; }
        public int defensePoint { get; set; }
        public int speedPoint { get; set; }
        public int manaPoint { get; set; }
    };
}
