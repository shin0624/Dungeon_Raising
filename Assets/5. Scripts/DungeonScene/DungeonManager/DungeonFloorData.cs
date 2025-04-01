using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//정보를 json으로 저장하기 위해 Serializable로 설정.
public class DungeonFloorData// 한 층의 던전 클리어 여부를 저장하는 클래스.
{
    public Dictionary<string, bool> clearedDungeons = new Dictionary<string, bool>();//던전ID와와 클리어 여부를 저장하는 딕셔너리. 던전 이름을 키로 사용하고, 클리어 여부를 값으로 사용한다.
}
