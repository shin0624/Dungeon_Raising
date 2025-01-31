using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoDefine
{
    public struct PlayerInformations
    {   
        public string playerNickname { get; set; }
        public string playerGender { get; set; }
        public string playerJob { get; set; }
        public string playerRace { get; set; }
        public string towerFloor { get; set; }
        public float playerExp { get; set; }
        public int playerLevel { get; set; }
        public int playerGold { get; set; }
        public int playerActivity { get; set; }
        
    };
}
