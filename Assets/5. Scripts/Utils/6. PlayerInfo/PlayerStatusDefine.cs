using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusDefine 
{
    public struct PlayerStatus
    {
        public int [] pvpReport { get; set; }
        public int pvpRanking { get; set; }
        public float attackPoint { get; set; }
        public float defensePoint { get; set; }
        public float healthPoint { get; set; }
        public float attackSpeed { get; set; }
        public float moveSpeed { get; set; }
        public float skillDamage { get; set; }
    };
}
