using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="HeroInformation", menuName ="Hero/HeroInformation")]
public class HeroInformation : ScriptableObject
{
    //���� ������ ������ ������ ��ũ���ͺ� ������Ʈ.
    //������ �ʿ��� ������ [���� �̸�, ���� ����, ���� ����, ���� ��� ���ݷ�, ü��, ����]
    [Header("Hero Info")]
    public string heroName;
    public HeroType heroType;
    public HeroRaise heroRaise;
    public GameObject heroPrefab;
    public Sprite heroHalfImage;
    
    
    [Header("Hero Status")]
    public int heroLevel;
    public ItemGrade heroGrade;
    public float attackPoint;
    public float deffensePoint;
    public float HealthPoint;

}
