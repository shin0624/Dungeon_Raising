using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "SoldierInformation", menuName ="Soldier/SoldierInformation")]
public class SoldierInformation : ScriptableObject
{
    //���� ���� ���� Ŭ����. ���� ������ ���� ����ó�� ������ �����͸� ���� ��ü�� �̷������ �ʴ´�.
    //���� ��ü�� �ش� �������� maxAmount�� ������ ���ڸ�ŭ ������ ���̸�, ��� ���� �����͸� �����Ѵ�.


    [Header("Soldier Info")]
    public GameObject soldierPrefab;//���� ������
    public Sprite soldierHalfImage;
    public string soldierType;//���� ���� : �˻�(SwordMan), �ü�(Archer)


    [Header("Soldier Status")]
    public int soldierLevel;
    public float attackPoint;
    public float defensePoint;
    public float healthPoint;
    public float attackSpeed;
    public float moveSpeed;
    public float skillDamage;


}
