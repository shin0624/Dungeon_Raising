using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProgressManager : MonoBehaviour// 던전 진행 상태를 관리하는 매니저. 던전 클리어 상태는 게임이 실행되는 내내 같은 데이터를 유지해야 하므로, 싱글톤으로 선언.
{
    public static TowerProgressManager Instance {get; private set;}//타워 진행 매니저의 인스턴스. 다른 클래스에서 접근할 수 있도록 public으로 설정.

    private TowerProgressData towerProgress = new TowerProgressData();//타워 진행 상태를 저장하는 데이터. TowerProgressData 클래스의 인스턴스.

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;//타워 진행 매니저의 인스턴스를 현재 인스턴스로 설정.
            DontDestroyOnLoad(gameObject);//씬이 바뀌어도 파괴되지 않도록 설정. 
        }
        else
        {
            Destroy(gameObject);//타워 진행 매니저의 인스턴스가 이미 존재하면 현재 인스턴스를 파괴.
        }
    }

    public void SetDungeonClear(int floor, string dungeonID)//특정 층의 던전 클리어 여부를 true로 설정하는 메서드.
    {
        if(!towerProgress.floorProgress.ContainsKey(floor))//floor에 해당하는 층이 floorProgress 딕셔너리에 없다면 새로운 DungeonFloorData를 생성하여 추가.
        {
            towerProgress.floorProgress[floor] = new DungeonFloorData();
        }
        towerProgress.floorProgress[floor].clearedDungeons[dungeonID] = true;//floor를 키로 하는 DungeonFloorData클래스의 clearedDungeons딕셔너리에서, dungeonID 키에 맞는 값을 true로 설정.
        //즉, 1층의 001던전이 클리어되었다고 하면 SetDungeonClear(1, "001") 호출 -> floorProgress의 요소 중 1층을 키로 갖는 DungeonFloorData로 이동 -> 001을 키로 갖는 clearedDungeons의 값을 true로 설정.
    }


}
