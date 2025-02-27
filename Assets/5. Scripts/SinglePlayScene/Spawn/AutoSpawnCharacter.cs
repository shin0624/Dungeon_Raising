using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoSpawnCharacter : MonoBehaviour
{
  // SinglePlayScene에 처음 진입 시 자동으로 캐릭터 유닛이 스폰되도록 하는 스크립트.
    // 레이어는 [병사 레이어, 캐릭터 레이어, 영웅 레이어] 총 3개가 존재하며, SinglePlayScene에 처음 진입 시 각각의 레이어에서 유닛들이 자동 스폰된다.
    // 이후 터치를 사용하여 유닛의 위치를 바꾸는 기능은 AutoSpawner__.cs 이외의 다른 스크립트에서 제어한다.
    // 3개의 레이어는 총 25칸(5*5)으로 구성되어 있고, 영웅과 캐릭터가 각각 1칸씩, 병사는 23칸에 배치된다.
    
    [SerializeField] private Tilemap characterTilemapLayer;
    [SerializeField] private int maxAmount = 1;//한 번의 singlePlay에 사용되는 캐릭터터 수는 1개. 정해진 칸에 스폰된다. 캐릭터의 직업은 한 종류로 고정된다.
    [SerializeField] private Transform prefabParent;//스폰될 프리팹을 자식으로 둘 부모 오브젝트
    [SerializeField] private UnitManager unitManager;
    private GameObject playerCharacter;//현재 플레이어의 성별과 직업에 맞는 캐릭터 프리팹. Managers의 컴포넌트인 PlayerCharacterManager.cs에서 가져온다.
    public GameObject newCharacter;
    private Vector3Int characterSpawnPosition = new Vector3Int(0,0,0);//캐릭터 유닛이 최초에 스폰될 타일의 그리드 좌표.
    private int nowSpawnedCharacterCount = 0;//현재 스폰된 캐릭터의 수. 최대 1개.
    private Quaternion rotation = Quaternion.Euler(0,-180,0);//플레이어측의 프리팹은 기본적으로 왼쪽을 보고 있으므로, 180도 회전시켜서 상대 측을 바라보게 한다.

    private void OnEnable() 
    {
        playerCharacter = unitManager.GetPlayerCharacter();//플레이어의 성별과 직업에 맞는 캐릭터 프리팹을 가져온다.
        FindSpawnPosition();//씬이 활성화될 때 스폰 가능한 위치 찾기
    }

    private void Start()//활성화 초기화가 끝나면 캐릭터를 소환한다.
    {
        SpawnPlayerCharacter();//캐릭터 프리팹을 타일맵 위 캐릭터 위치에 스폰한다.
    }


    private void FindSpawnPosition()//캐릭터 프리팹을 스폰할 타일맵 위치를 탐색하는 메서드
    {
        bool founded = false;
        BoundsInt bounds = characterTilemapLayer.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(characterTilemapLayer.HasTile(position))
            {
                characterSpawnPosition = position;
                founded = true;
                break;
            }
        }
        if(!founded)//타일이 없다면 경고출력.
        {
            Debug.LogWarning("There is no tile to spawn Playercharacter.");
        }
    }

    private void SpawnPlayerCharacter()//캐릭터의 소환 가능 여부를 체크하고 전용 타일에 소환한다. 최초 스폰용 메서드.
    {
        if(nowSpawnedCharacterCount >= maxAmount)
        {
            Debug.Log("maxAmount of Playercharacter is spawned.");
            return;
        }
        if(playerCharacter == null)
        {
            Debug.LogWarning("PlayerCharacter is not initialized.");
            return;
        }
        Vector3Int spawnTile = characterSpawnPosition;//FindSpawnPosition()에서 찾은 타일 위치를 불러온다.
        Vector3 worldPositon = characterTilemapLayer.GetCellCenterWorld(spawnTile);//타일의 그리드 좌표를 월드 좌표로 변환한다.
        
        newCharacter = Instantiate(playerCharacter, worldPositon, rotation, prefabParent);//플레이어 정보와 일치하는 플레이어 캐릭터 프리팹을 소환한다.
        nowSpawnedCharacterCount++;//스폰될 캐릭터는 1개 뿐이므로 카운트를 증가시켜 추가 소환을 제한한다.
    }

    public void ClearSpawnedPlayerCharacter()//배치 방식이 바뀔 때, 기존에 스폰되었던 캐릭터 유닛 인스턴스들을  모두 제거한다.
    {
        Destroy(newCharacter);
    }
}
