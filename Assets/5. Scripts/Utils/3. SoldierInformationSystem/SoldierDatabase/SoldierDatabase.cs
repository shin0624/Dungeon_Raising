using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SoldierDatabase", menuName ="Soldier/SoldierDatabase")]
public class SoldierDatabase : ScriptableObject
{
    //���� ���� ������ �����ϱ� ���� ��ũ���ͺ� ������Ʈ.
     public List<SoldierInformation> soldierInformationList;
}
