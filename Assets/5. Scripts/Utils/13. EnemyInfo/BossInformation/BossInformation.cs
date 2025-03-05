using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="BossInformation", menuName = "Enemy/BossInformation")]
public class BossInformation : ScriptableObject
{
    //각 던전의 보스 정보를 저장할 스크립터블 오브젝트.

    [Header("Boss Info")]
    public GameObject bossPrefab;
    public BossType  bossType;

    [Header("Boss Status")]
    public int bossLevel;
    public float attackPoint;
    public float healthPoint;
    public float defensePoint;
    public float attackSpeed;
    public float moveSpeed;
    public float skillDamage;
}
