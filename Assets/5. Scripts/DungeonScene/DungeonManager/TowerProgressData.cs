using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerProgressData//전체 50층의 진행 상황을 저장할 클래스.
{
    public Dictionary<int, DungeonFloorData> floorProgress = new Dictionary<int, DungeonFloorData>();//층 번호와 층의 던전 클리어 여부를 저장하는 딕셔너리. 층 번호를 키로 사용하고, 층의 던전 클리어 여부를 값으로 사용한다.
}
