using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SoldierDatabase", menuName ="Soldier/SoldierDatabase")]
public class SoldierDatabase : ScriptableObject
{
    //병사 유닛 정보를 관리하기 위한 스크립터블 오브젝트.
     public List<SoldierInformation> soldierInformationList;
}
