using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TowerProgressManager : MonoBehaviour// 던전 진행 상태를 관리하는 매니저. 던전 클리어 상태는 게임이 실행되는 내내 같은 데이터를 유지해야 하므로, 싱글톤으로 선언.
{
    public static TowerProgressManager Instance {get; private set;}//타워 진행 매니저의 인스턴스. 다른 클래스에서 접근할 수 있도록 public으로 설정.

    private TowerProgressData towerProgress = new TowerProgressData();//타워 진행 상태를 저장하는 데이터. TowerProgressData 클래스의 인스턴스.
    
    private Dictionary<int, int> floorDungeonCount = new Dictionary<int, int>{ {1,3}, {10,4}, {20,5}, {30,5}, {40,6}, {50,1}};// 층 별 던전 개수 규칙 딕셔너리. 각 층에 존재할 던전의 개수이다.

    public List<DungeonInformation> allDungeons;// 각 층에 들어갈 던전 SO 리스트.
    [SerializeField] private DungeonDatabase dungeonDatabase;//던전 데이터베이스. 던전 정보를 담고 있는 SO.
    
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

    private void Start()
    {
        InputToAllDungeons();//던전 DB의 각 층 리스트 원소인 던전 정보들을 모두 allDungeons에 추가한다.
        InitProgress(50, allDungeons);// 총 50층의 타워, 그 안에 있는 던전들의 클리어 여부를 False로 초기화.
        SceneManager.sceneLoaded+=OnSceneLoaded;//씬이 로드될 때마다 OnSceneLoaded 메서드를 호출.
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//씬이 변경될 때 마다 던전 클리어 여부 및 현재 층 수, 클리어 던전 수를 업데이트한다.
    {
        //PlayerInfo.Instance.SetPlayerFloor();// 타워 층 수 테스트.
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

    private void InitProgress(int totalFloors, List<DungeonInformation> allDungeons)// 각 층의 모든 던전 클리어 상태를 초기화하는 메서드. 각 층에 존재하는 던전의 ID를 clearedDungeons에 저장하고, 기본값으로 false를 설정.
    {
        towerProgress.floorProgress.Clear();//기존 진행 데이터 초기화.

        Dictionary<int, List<string>> dungeonData = new Dictionary<int, List<string>>();// 층 별 던전ID를 저장할 딕셔너리. 층 번호를 키로, 그 층에 속한 던전들의 id리스트를 값으로 설정.

        foreach(DungeonInformation dungeon in allDungeons)//  던전 정보를 dungeonData 딕셔너리에 분류한다.
        {
            if(!dungeonData.ContainsKey(dungeon.floorNumber))// dungeonData딕셔너리에 floorNumber 층이 없다면 (즉, 해당 층이 처음 등장하면)
            {
                dungeonData[dungeon.floorNumber] = new List<string>();// 빈 리스트를 생성.
            }
            dungeonData[dungeon.floorNumber].Add(dungeon.dungeonID);//해당 층에 포함된 던전의 ID를 리스트에 추가한다.          
        }

        for(int floor = 1; floor <= totalFloors; floor++)//1층부터 반복
        {
            towerProgress.floorProgress[floor] = new DungeonFloorData();// 각 층의 던전 클리어 정보를 저장하는 객체를 생성.

            int maxDungeonCount = GetDungeonCountForFloor(floor);// 각 층에 위치할 수 있는 최대 던전 수를 저장.

            if(dungeonData.ContainsKey(floor))//해당 층에 던전이 존재하는 경우.
            {
                int dungeonCount = Mathf.Min(dungeonData[floor].Count, maxDungeonCount);//그 층에 있는 던전 수와 floorDungeonCount의 규칙을 비교하여 최대 배정 가능한 던전의 개수를 결정.
                for(int i = 0; i< dungeonCount; i++)//최대 배정 가능한 던전 개수만큼 반복하며
                {
                    towerProgress.floorProgress[floor].clearedDungeons[dungeonData[floor][i]] = false;//던전 ID를 clearedDungeons에 저장한다. 각 던전의 클리어 여부는 false.
                }
            }
            else//만약 해당 층에 해당하는 던전SO가 없을 경우. 즉, 아직 그 층에 어떤 던전을 넣을 지 몰라서 던전 SO를 만들어놓지 않았다면
            {
                towerProgress.floorProgress[floor].clearedDungeons = new Dictionary<string, bool>();// 빈 층으로 만들어서 놓는다. 추후 추가 가능하도록.
            }
        }  
    }

    private int GetDungeonCountForFloor(int floor)// 층에 따라 최대 던전의 개수를 반환하는 메서드. 1층~9층은 3개의 던전이 존재하고, 10층~29층까지는 4개..
    {
        foreach(var entry in floorDungeonCount.OrderByDescending(e => e.Key))//층수 별 던전 수 규칙 딕셔너리의 원소 중 키(층수)를 내림차순으로 정렬한 후 뽑는다.
        {
            if(floor >= entry.Key) return entry.Value;// floor는 타워 층 수 => floorDungeonCount의 key인 {50,40,30,20,10,1}순서로 비교되며, floor에 해당하는 value인 "각 층별 던전수"가 반환됨.
        } // --> 즉, floor = 1 -> floorDungeonCount{ {1, 3} } => 1층의 최대 던전 수 3 반환 / floor = 28 -> floorDungeonCount{ {20, 5} } => 28층의 최대 던전 수 5를 반환.

        return 3;//기본값은 3(1층)
    }

    private void InputToAllDungeons()//던전 DB의 각 층 리스트 원소인 던전 정보들을 모두 allDungeons에 추가한다. 리스트 내의 원소들을 풀어서 한 리스트에 넣기 위해 AddRange를 사용하였고, AddRange는 매개변수로 받은 리스트들의 원소들을 타겟 리스트에 넣는 것이므로, allDungeons의 원소는 리스트가 아니라 DungeonInformation객체들이다.
    {
        allDungeons = new List<DungeonInformation>();//던전 정보를 담을 리스트 초기화.
        allDungeons.AddRange(dungeonDatabase.undergroundDungeonList);//지하 던전 리스트 추가.
        allDungeons.AddRange(dungeonDatabase.hellDungeonList);//연옥 던전 리스트 추가.
        allDungeons.AddRange(dungeonDatabase.silvanDungeonList);//실반 던전 리스트 추가.
        allDungeons.AddRange(dungeonDatabase.KrisosDungeonList);//크리소스 던전 리스트 추가.
        allDungeons.AddRange(dungeonDatabase.skyDungeonList);//천궁 던전 리스트 추가.
        allDungeons.AddRange(dungeonDatabase.lastFloorDungeonList);//보스 플로어 던전 리스트 추가.
    }
    


}

