using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="BossInformation", menuName = "Enemy/BossInformation")]
public class BossInformation : ScriptableObject
{
    //�� ������ ���� ������ ������ ��ũ���ͺ� ������Ʈ.

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
