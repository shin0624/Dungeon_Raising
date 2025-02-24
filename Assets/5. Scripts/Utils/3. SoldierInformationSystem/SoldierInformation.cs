using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoldierInformation", menuName ="Soldier/SoldierInformation")]
public class SoldierInformation : ScriptableObject
{
    //���� ���� ���� Ŭ����. ���� ������ ���� ����ó�� ������ �����͸� ���� ��ü�� �̷������ �ʴ´�.
    //���� ��ü�� �ش� �������� maxAmount�� ������ ���ڸ�ŭ ������ ���̸�, ��� ���� �����͸� �����Ѵ�.


    [Header("Soldier Info")]
    public GameObject soldierPrefab;//���� ������
    public string soldierType;//���� ���� : �˻�(SwordMan), �ü�(Archer)


    [Header("Soldier Status")]
    public int soldierLevel;
    public int soldierHP;
    public int soldierDP;//����
    public int soldierAttackSpeed;
    public int soldierMoveSpeed;


}
