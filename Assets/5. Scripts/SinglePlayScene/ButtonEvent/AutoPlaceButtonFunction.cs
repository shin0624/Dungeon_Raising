using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoPlaceButtonFunction : MonoBehaviour
{
     // SinglePlayScene에서 자동 배치 버튼 클릭 시 실행되는 활성화되는 클래스.
     // 자동 배치는 영웅과 플레이어 캐릭터의 상성을 기준으로, 적 몬스터(또는 적 플레이어)의 상성에 따라 자동 배치된다. 병사는 플레이어와 영웅 유닛 뒤에 배치된다.
     // 유닛은 5*5 그리드 위에 배치된다. 영웅과 플레이어 캐릭터는 각각 1칸씩 배치되며, 병사는 10칸에 배치된다.
     // 유닛 유형 : 근접형, 원거리형, 마법형
     // 검사 : 근접형 || 궁수 : 원거리형 || 마법사 : 마법형
     // 영웅 타입 : 근접형 / 원거리형 / 마법형
     // 근접형 > 마법형에게 강하고 원거리형에게 약함. || 원거리형 > 근접형에게 강하고 마법형에게 약함 || 마법형 > 원거리형에게 강하고 근접형에게 약함.
     // 자동배치에서의 유닛 배치는 영웅 유닛이 선두, 그 뒤 타일로 캐릭터 유닛이 배치되고, 학익진 형태로 나머지 병사 유닛들이 배치된다.
     // 현재 적 유닛이 배치된 타일에서 선두에 있는 유닛의 유형에 강한 유닛이 플레이어 타일에 있을 경우 그 유닛을 선두에 배치. 상성 우위인 유닛이 플레이어에게 없다면, 동일한 상성을 가진 유닛을 선두에 배치. 
     // 자동 배치 최종 로직 : 자동배치 선택 시, 기존 타일 레이어에 존재하던 유닛들을 전부 자동배치용 레이어로 옮긴다. 유닛 인스턴스를 제거한 후 새로 생성하는 것 보다 효율 면에서 나은 선택.
    [SerializeField] private Tilemap soldierPlaceLayer;
    [SerializeField] private Tilemap heroPlaceLayer;
    [SerializeField] private Tilemap characterPlaceLayer;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private AutoSpawnerSoldier autoSpawnerSoldier;
    [SerializeField] private AutoSpawnCharacter autoSpawnCharacter;
    [SerializeField] private AutoSpawnerHero autoSpawnerHero;
    private Vector3Int characterSpawnPosition = new Vector3Int(0,0,0);//캐릭터 유닛이 자동배치용 레이어 타일에 스폰될 그리드 좌표.
    private Vector3Int heroSpawnPosition = new Vector3Int(0,0,0);//영웅 유닛이 자동배치용 레이어 타일에 스폰될 그리드 좌표.
    //----------------Queues
    private Queue<Vector3Int> availableTilesForSoldiers = new Queue<Vector3Int>();//병사 유닛들을 이동 배치할 수 있는 타일목록 큐.
    
    private void Start()//AutoPlaceButtonFunction은 PlaceUILeftPanelController.cs에서 OnAutoPlaceButtonClicked()가 호출되기 전까지 컴포넌트 비활성화 상태이며, OnAutoPlaceButtonClicked()가 호출되었을 때 실행된다.
    {
        StartAutoPlace();
    }
    public void StartAutoPlace()//자동배치 버튼을 2회 이상 클릭 시 유닛들이 다시한번 자동배치된다.
    {
        StartCoroutine(AutoPlaceFunction());
    }

    private IEnumerator AutoPlaceFunction()
    {
        FindUnitsPosition();
        yield return null;
        MoveAllUnits();
    }

    private void FindUnitsPosition()
    {
        FindCharacterPosition();
        FindHeroPosition();
        FindSoldiersPosition();
        Debug.Log("Find Position for AutoPlace Completed.");
    }

    private void MoveAllUnits()
    {
        MoveCharacter();
        MoveHero();
        MoveSoldiers();
        Debug.Log("AutoPlace Completed.");
    }



//--------------병사 유닛 이동 함수----------------------------
    private void FindSoldiersPosition()//병사 유닛들의 스폰 장소를 찾는다.
    {
        BoundsInt bounds = soldierPlaceLayer.cellBounds;
        foreach(Vector3Int pos in bounds.allPositionsWithin)//자동배치용 병사타일에서 배치 가능한 위치를 찾는다.
        {
            if(soldierPlaceLayer.HasTile(pos))
            {
                availableTilesForSoldiers.Enqueue(pos);//타일목록 큐에 삽입.
            }
        }
        if(availableTilesForSoldiers.Count == 0 )
        {
            Debug.LogWarning("No Available Tiles found in soldierPlaceLayer");
        }
    }

    private void MoveSoldiers()//병사 유닛들을 스폰한다.
    {   
        foreach(GameObject unit in autoSpawnerSoldier.spawnedSoldiers)//기존 레이어 위의 병사들을 새로운 타일 위로 옮긴다.
        {
            if(availableTilesForSoldiers.Count == 0)//타일 큐가 비어있을 경우를 방지.
            {
                Debug.LogWarning("Not Enough available tiles for soldiers.");
                return;//break를 사용하면 루프 종료 후 코드가 계속 실행될 테니, 타일 큐가 비어있을 때는 return으로 함수를 종료시킨다.
            }

            Vector3Int newTilePos = availableTilesForSoldiers.Dequeue();//자동배치용 병사 타일을 선택하여 빼낸다.
            Vector3 newWorldPos = soldierPlaceLayer.GetCellCenterWorld(newTilePos);//자동 배치용 타일의 중앙 좌표를 가져온다.

            unit.transform.position = newWorldPos;//병사 유닛들을 이동시킨다.
        }
    }

//-----------------캐릭터 유닛 이동 함수-------------------------

    private void FindCharacterPosition()//캐릭터 유닛의 스폰 장소를 찾는다.
    {
        bool founded = false;//타일이 없을 경우를 알리는 플래그. 
        BoundsInt bounds = characterPlaceLayer.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(characterPlaceLayer.HasTile(position))
            {
                characterSpawnPosition = position;
                founded = true;
                break;
            }
        }
        if(!founded)//타일이 없다면 경고출력. 조건을 characterSpawnPosition = Vector3 (0,0,0)일 때로 해버리면 실행되지 않기에, 타일이 전혀 없을 경우를 감지해야 함.
        {
            Debug.LogWarning("There is no tile to spawn Playercharacter.");
        }
    }

    private void MoveCharacter()//기존 레이어의 캐릭터 유닛을 자동배치용 레이어 타일로 이동시키는 함수.
    {
        Vector3Int newTilePos = characterSpawnPosition;//FindCharacterPosition()에서 찾은 자동배치용 타일 좌표를 새로운 좌표로 등록.
        Vector3 newWorldPos = characterPlaceLayer.GetCellCenterWorld(newTilePos);//Vector3Int형 그리드좌표를 실제 타일맵 월드좌표로 변환.

        if(autoSpawnCharacter.newCharacter!=null)
        {
            autoSpawnCharacter.newCharacter.transform.position = newWorldPos;//캐릭터 유닛을 이동.
        }
        else
        {
            Debug.LogWarning("autoSpawnCharacter.newCharacter is NULL");
        }
    }

    //--------------영웅 유닛 이동 함수-------------------------

    private void FindHeroPosition()//영웅 유닛의 스폰 장소를 찾는다.
    {
        bool founded = false;
        BoundsInt bounds = heroPlaceLayer.cellBounds;//영웅 유닛이 위치할 자동배치용 레이어 상의 위치를 찾는다.
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(heroPlaceLayer.HasTile(position))
            {
                heroSpawnPosition = position;
                founded = true;
                break;
            }
        }
        if(!founded)//타일이 없다면 경고출력.
        {
            Debug.LogWarning("There is no tile to spawn Hero.");
        }
    }

    private void MoveHero()//기존 레이어의 영웅 유닛을 자동배치용 레이어 타일로 이동시키는 함수.
    {
        Vector3Int newTilePos = heroSpawnPosition;
        Vector3 newWorldPos = heroPlaceLayer.GetCellCenterWorld(newTilePos);
     
        if(autoSpawnerHero.newHero!=null)
        {
            autoSpawnerHero.newHero.transform.position = newWorldPos;
        }
        else
        {
            Debug.LogWarning("autoSpawnerHero.newHero is NULL");
        }
    }

}
