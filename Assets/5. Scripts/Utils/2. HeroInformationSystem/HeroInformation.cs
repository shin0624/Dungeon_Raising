using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="HeroInformation", menuName ="Hero/HeroInformation")]
public class HeroInformation : ScriptableObject
{
    //영웅 유닛의 정보를 저장할 스크립터블 오브젝트.
    //영웅에 필요한 정보는 [영웅 이름, 영웅 유형, 영웅 종족, 영웅 등급 공격력, 체력, 방어력]
    [Header("Hero Info")]
    public string heroName;
    public HeroType heroType;
    public HeroRaise heroRaise;
    public GameObject heroPrefab;
    public Sprite heroHalfImage;
    
    
    [Header("Hero Status")]
    public int heroLevel;//레벨
    public Grade heroGrade;//등급
    public float attackPoint;//공격력
    public float defensePoint;//방어력
    public float healthPoint;//체력
    public float attackSpeed;//공격속도
    public float moveSpeed;//이동속도
    public float skillDamage;//스킬 피해량

}
