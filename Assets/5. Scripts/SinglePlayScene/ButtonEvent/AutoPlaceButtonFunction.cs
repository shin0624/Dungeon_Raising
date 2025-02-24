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

    //----------------Queues
    private Queue<Vector3Int> availableTilesForSoldiers = new Queue<Vector3Int>();//병사 유닛들을 이동 배치할 수 있는 타일목록 큐.
    


    private void Start()
    {
       
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
            Debug.LogWarning("No Available Tilse found in soldierPlaceLayer");
        }
    }

    private void MoveSoldiers()//병사 유닛들을 스폰한다.
    {   
        foreach(GameObject unit in autoSpawnerSoldier.spawnedSoldiers)//기존 레이어 위의 병사들을 새로운 타일 위로 옮긴다.
        {
            if(availableTilesForSoldiers.Count == 0)
            {
                Debug.LogWarning("Not Enough available tiles for soldiers.");
                break;
            }

            Vector3Int newTilePos = availableTilesForSoldiers.Dequeue();//자동배치용 병사 타일을 선택하여 빼낸다.
            Vector3 newWorldPos = soldierPlaceLayer.GetCellCenterWorld(newTilePos);//자동 배치용 타일의 중앙 좌표를 가져온다.

            unit.transform.position = newWorldPos;//병사 유닛들을 이동시킨다.
        }
    }

//-----------------캐릭터 유닛 이동 함수-------------------------









}
