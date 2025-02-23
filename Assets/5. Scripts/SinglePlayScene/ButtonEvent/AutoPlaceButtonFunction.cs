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
     // 근접형 > 마법형에게 강하고 원거리형에게 약함. || 원거리형 > 근접형에게 강하고 마법형에게 약함 || 마법형 > 원거리형에게 강하고 근접형에게 약함.
     // 자동배치에서의 유닛 배치는 AutoPlaceLayer 상에서 수행되며, 현재 적 유닛이 배치된 타일에서 선두에 있는 유닛의 유형에 강한 유닛이 플레이어 타일에 있을 경우 그 유닛을 선두에 배치. 상성 우위인 유닛이 플레이어에게 없다면, 동일한 상성을 가진 유닛을 선두에 배치. 

    [SerializeField] private Tilemap baseTilemap;
    [SerializeField] private UnitManager unitManager;
    
   

}
