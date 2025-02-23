using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HeroInformation", menuName ="Hero/HeroInformation")]
public class HeroInformationSystem : ScriptableObject
{
    //영웅 유닛의 정보를 저장할 스크립터블 오브젝트.
    //영웅에 필요한 정보는 [영웅 이름, 영웅 유형, 영웅 종족, 영웅 등급 공격력, 체력, 방어력]
    [Header("Hero Info")]
    public string heroName;
    public HeroType heroType;
    public HeroRaise heroRaise;
    public GameObject heroPrefab;
    
    [Header("Hero Status")]
    public ItemGrade heroGrade;
    public float attackPoint;
    public float deffensePoint;
    public float HealthPoint;

}
