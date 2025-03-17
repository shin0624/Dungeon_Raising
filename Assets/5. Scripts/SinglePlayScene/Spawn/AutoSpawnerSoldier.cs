using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoSpawnerSoldier : MonoBehaviour
{
    // SinglePlayScene에 처음 진입 시 자동으로 병사 유닛이 스폰되도록 하는 스크립트.
    // 레이어는 [병사 레이어, 캐릭터 레이어, 영웅 레이어] 총 3개가 존재하며, SinglePlayScene에 처음 진입 시 각각의 레이어에서 유닛들이 자동 스폰된다.
    // 이후 터치를 사용하여 유닛의 위치를 바꾸는 기능은 AutoSpawner__.cs 이외의 다른 스크립트에서 제어한다.
    // 3개의 레이어는 총 25칸(5*5)으로 구성되어 있고, 영웅과 캐릭터가 각각 1칸씩, 병사는 23칸에 배치된다.
    
    [SerializeField] private Tilemap soldierTilemapLayer;
    [SerializeField] public int maxAmount = 10;//한 번의 SinglePlay에 사용되는 최대 병사 수는 10개. 10개의 병사를 23칸 중 랜덤으로 배치.
    [SerializeField] private Transform prefabParent;//스폰될 프리팹을 자식으로 둘 부모 오브젝트
    [SerializeField] private UnitManager unitManager;
    
    //Vector3Int : 타일맵은 Grid좌표 시스템을 기반으로 동작하므로, 프리팹이 배치될 각 타일의 정수 좌표((0,0) 등)를 정확하게 참조하기 위해 Vector3Int를 사용.
    //Vector3는 실수형 좌표라서 숫자 뒤에 f를 붙여야 하며 타일맵에서는 지원하지 않음.
    //타일맵에서 위치를 찾을 때는 Vector3Int를 사용하고, 실제 유닛을 월드 좌표에 배치할 때는 Vector3를 사용. 주로 Tilemap클래스의 GetCellCenterWorld()메서드 등에 Vector3Int 변수값(타일맵의 좌표)을 넣어 변환한다.
    private Queue<Vector3Int> soldierSpawnPositions = new Queue<Vector3Int>();//병사가 스폰될 타일의 좌표를 저장할 큐. 병사를 스폰한 후 해당 병사의 타일좌표를 제거해야 동일 좌표 중복 소환을 방지할 수 있기에, 첫번째 요소를 제거하는 연산에 O(1)이 걸리는 Dequeue를 쓰기 위해 큐 형태로 선언. 리스트의 Remove는 O(n)이 걸리므로 큐를 쓰는게 더 효율적일 것이다.
    private List<GameObject> mySoldiers;
    public List<GameObject> spawnedSoldiers = new List<GameObject>();
    private GameObject newSoldier;
    private int spawnedCount = 0;
    private Quaternion rotation = Quaternion.Euler(0,-180,0);//플레이어측의 프리팹은 기본적으로 왼쪽을 보고 있으므로, 180도 회전시켜서 상대 측을 바라보게 한다.


    private void OnEnable()
    {
        mySoldiers = unitManager.GetSoliderList();
        FindSpawnPosition();//씬이 활성화될 때 스폰 가능한 위치 찾기
    }

    private void Start()
    {
        if(mySoldiers == null || mySoldiers.Count==0)
        {
            Debug.LogError("No Soldiers prefab found in UnitManager");
            return;
        }
        if(soldierSpawnPositions.Count < maxAmount)
        {
            Debug.LogWarning("Not enough spawn Positions available");
        }
        while(spawnedCount < maxAmount && soldierSpawnPositions.Count > 0)
        {
            SpawnSoldier();//병사 프리팹을 타일맵 위에 보유 숫자만큼 스폰한다.
        }
    }

    private void FindSpawnPosition()//병사 프리팹을 타일맵 위 위치에 보유 숫자만큼 스폰한다.
    {
        //BoundsInt는 주로 타일맵 내의 유효한 cell 영역을 나타내는 데 사용. 각 cell은 하나의 tile을 나타낸다.
        BoundsInt bounds = soldierTilemapLayer.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {   
            if(soldierTilemapLayer.HasTile(position))//타일맵 셀에 존재하는 모든 위치를 꺼내어 반복하며 타일이 존재하는 위치를 저장
            {   
                soldierSpawnPositions.Enqueue(position);//병사 소환용 레이어에서 타일이 존재하는 위치만 좌표저장 큐에 삽입.
                //Debug.Log($"soldierSpawnPositions.Count = {soldierSpawnPositions.Count}");
            }
        }
        if(soldierSpawnPositions.Count == 0)
        {
            Debug.LogWarning("No Available Tiles found in soldierPlaceLayer");
        }
    }

    private void SpawnSoldier()//병사의 소환 가능 여부를 체크하고 소환 가능한 타일에 병사 프리팹을 Instantiate()하는 함수
    {
        if(spawnedCount >=maxAmount)
        {
            Debug.LogWarning("maxAmount of soldier is spawned.");
            return;
        }
        /*
            캐릭터가 스폰될 타일은 FindSpawnPosition()에서 큐에 넣어진 타일이며, 순서대로 유닛이 배치된다.
            Dequeue로 첫 좌표부터 꺼내고 나면 캐릭터가 위치한 타일을 제거하여 사용 불가하게 함. (같은 타일에 유닛이 중복 소환되는 현상 방지)
        */
        if(soldierSpawnPositions.Count > 0)
        {
            Vector3Int spawnTile = soldierSpawnPositions.Dequeue();
            Vector3 worldPosition = soldierTilemapLayer.GetCellCenterWorld(spawnTile);//해당 타일 중심 위치를 가져와서 그곳에 캐릭터를 배치.

            GameObject newSoldier = Instantiate(mySoldiers[0], worldPosition, rotation, prefabParent);//병사 유닛 리스트의 첫번째 요소를 스폰. 
            spawnedSoldiers.Add(newSoldier);
            Debug.Log($"soldierSpawnPositions.Count = {soldierSpawnPositions.Count}");
            spawnedCount++;
        }
        else
        {
            Debug.LogWarning("No Available Tiles found in soldierPlaceLayer");
        }
    }

    public void ClearSpawnedSoldiers()//배치 방식이 바뀔 때, 기존에 스폰되었던 병사 유닛 인스턴스들을  모두 제거한다.
    {
        foreach(var solider in spawnedSoldiers)
        {
            Destroy(solider);
        }
        spawnedSoldiers.Clear();
        spawnedCount = 0; // 스폰된 병사 수 초기화
    }

    public int GetSpawnedCount()
    {
        return spawnedCount;
    }

    public int GetMaxAmount()
    {
        return maxAmount;
    }
}
