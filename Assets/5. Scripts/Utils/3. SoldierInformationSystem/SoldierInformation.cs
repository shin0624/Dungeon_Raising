using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "SoldierInformation", menuName ="Soldier/SoldierInformation")]
public class SoldierInformation : ScriptableObject
{
    //병사 정보 관리 클래스. 병사 유닛은 영웅 유닛처럼 각각의 데이터를 가진 객체로 이루어지지 않는다.
    //병사 객체는 해당 던전에서 maxAmount로 지정한 숫자만큼 스폰될 것이며, 모두 같은 데이터를 공유한다.


    [Header("Soldier Info")]
    public GameObject soldierPrefab;//병사 프리팹
    public Image soldierHalfImage;
    public string soldierType;//병사 종류 : 검사(SwordMan), 궁수(Archer)


    [Header("Soldier Status")]
    public int soldierLevel;
    public int soldierHP;
    public int soldierDP;//방어력
    public int soldierAttackSpeed;
    public int soldierMoveSpeed;


}
