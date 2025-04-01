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

    public bool IsDungeonCleared(int floor, string dungeonID)// 특정 층의 던전 클리어 여부를 확인하는 함수.
    {
        //딕셔너리에 해당 floor와 dungeonID가 존재하는지 확인 후, 클리어 여부를 반환한다.
        return towerProgress.floorProgress.ContainsKey(floor) && //전체 50층 진행상황 딕셔너리에서 해당 층이 존재하는지 여부.
        towerProgress.floorProgress[floor].clearedDungeons.ContainsKey(dungeonID) && //해당 층의 던전 클리어 여부 딕셔너리에서 dungeonID에 해당하는 던전이 존재하는지 여부.
        towerProgress.floorProgress[floor].clearedDungeons[dungeonID];//해당 층의 dungeonID에 해당하는 던전이 클리어 되었는지 여부.
    }

    public bool IsFloorCleared(int floor, List<string> dungeonIDs)//해당 층의 모든 던전이 클리어 되었는지 확인하는 메서드.
    {
        if(!towerProgress.floorProgress.ContainsKey(floor))//아직 floor층이 해금되지 않았다면 false 리턴.
        {            
            Debug.Log($"{floor}층은 해금되지 않았습니다.");
            return false;
        }
        
        foreach(var dungeonID in dungeonIDs)//던전 id 리스트를 순회하며, 각 던전의 클리어 여부를 확인.
        {
            if(!IsDungeonCleared(floor, dungeonID))//floor층의 dungeonID에 해당하는 던전이 클리어되지 않았다면 false 리턴.
            {
                Debug.Log($"{floor}층의 {dungeonID} 던전은 클리어되지 않았습니다.");
                return false;
            }
        }
        return true;//모든 던전이 클리어되었다면 true 리턴.
    }


}
