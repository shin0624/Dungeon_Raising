using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoSpawnerHero : MonoBehaviour
{
    // SinglePlayScene에 처음 진입 시 자동으로 병사 유닛이 스폰되도록 하는 스크립트.
    // 레이어는 [병사 레이어, 캐릭터 레이어, 영웅 레이어] 총 3개가 존재하며, SinglePlayScene에 처음 진입 시 각각의 레이어에서 유닛들이 자동 스폰된다.
    // 이후 터치를 사용하여 유닛의 위치를 바꾸는 기능은 AutoSpawner__.cs 이외의 다른 스크립트에서 제어한다.
    // 3개의 레이어는 총 25칸(5*5)으로 구성되어 있고, 영웅과 캐릭터가 각각 1칸씩, 병사는 23칸에 배치된다.
    
    [SerializeField] private Tilemap heroTilemapLayer;
    [SerializeField] private int maxAmount = 1;//한 번의 singlePlay에 사용되는 영웅 수는 1개. 정해진 칸에 스폰되고, UI의 SelectButton으로 교체할 수 있다.
    [SerializeField] private Transform prefabParent;//스폰될 프리팹을 자식으로 둘 부모 오브젝트
    [SerializeField] private List<GameObject> heroList = new List<GameObject>();//플레이어가 보유 중인 영웅들의 리스트. 최초에 스폰되는 영웅은 리스트[0]. UI의 SelectButton으로 영웅을 골라 바꿀 수 있다.
    //추후에 플레이어 정보와 연동.
    private Vector3Int heroSpawnPosition = new Vector3Int(0,0,0);//영웅 유닛이 최초에 스폰될 타일의 그리드 좌표.
    private int nowSpawnedHeroCount = 0;//현재 스폰된 영웅의 수. 최대 1개.
    private Quaternion rotation = Quaternion.Euler(0,-180,0);//플레이어측의 프리팹은 기본적으로 왼쪽을 보고 있으므로, 180도 회전시켜서 상대 측을 바라보게 한다.

    private void OnEnable() 
    {
        FindSpawnPosition();//씬이 활성화될 때 스폰 가능한 위치 찾기
    }

    private void Start()//활성화 초기화가 끝나면 영웅을 소환한다.
    {
        SpawnHero();//캐릭터 프리팹을 타일맵 위 영웅 위치에 스폰한다.
    }

    private void FindSpawnPosition()//영웅 프리팹을 타일맵 위 영웅 위치에 스폰하는 메서드.
    {
        BoundsInt bounds = heroTilemapLayer.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(heroTilemapLayer.HasTile(position))
            {
                heroSpawnPosition = position;
                break;
            }
        }
        if(heroSpawnPosition == new Vector3Int(0,0,0))//타일이 없다면 경고출력.
        {
            Debug.LogWarning("There is no tile to spawn hero.");
        }
    }

    private void SpawnHero()//영웅의 소환 가능 여부를 체크하고 영웅 전용 타일에 소환한다. 최초 스폰용 메서드.
    {
        if(nowSpawnedHeroCount >= maxAmount)
        {
            Debug.Log("maxAmount of hero is spawned.");
            return;
        }
        if(heroList.Count == 0)
        {
            Debug.LogWarning("There is no hero in the list.");
            return;
        }
        Vector3Int spawnTile = heroSpawnPosition;//FindSpawnPosition()에서 찾은 타일 위치를 불러온다.
        Vector3 worldPositon = heroTilemapLayer.GetCellCenterWorld(spawnTile);//타일의 그리드 좌표를 월드 좌표로 변환한다.

        GameObject newHero = Instantiate(heroList[0], worldPositon, rotation, prefabParent);//영웅 리스트의 첫번째 영웅을 스폰한다.
        nowSpawnedHeroCount++;
    }
}
