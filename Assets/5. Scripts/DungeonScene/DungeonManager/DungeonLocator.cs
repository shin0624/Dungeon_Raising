using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLocator : MonoBehaviour// 현재 플레이어의 currentFloor에 해당하는 Scene__이 로드될 때 호출되는 클래스.
                                            // 각 Scene에서 등장할 던전 정보 SO를 저장한 5개의 리스트가 존재함.
                                            // TowerProgressManager의 메서드로 현재 플레이어가 접근하려는 층에 던전 클리어 여부, 층 클리어 여부를 확인한다.
                                            // 현재 접근하고자 하는 층이 클리어되지 않았을 때 && 그 층의 클리어되지 않은 던전이 존재할 때 ==> 해당 층으로 이동 가능.
                                            // currentFloor / 10 값에 해당하는 값은 (0 ~ 5)이고, 5는 보스 플로어이므로 0~4까지 총 5개의 던전 SO 리스트를 준비한다.
                                            // Scene이 로드되면, Scene에 위치한 던전 트랜스폼에 리스트 내의 SO를 랜덤으로 배치한다.
                                            // 던전id는 DungeonInformation.dungeonID(string)
{
    void Start()
    {
        
    }

}
