using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="EnemyInformation", menuName = "Enemy/EnemyInformation")]
public class EnemyInformation : ScriptableObject
{
    //���ʹ��� ������ ������ ��ũ���ͺ� ������Ʈ.

    [Header("Enemy Info")]
    public GameObject enemyPrefab;
    public EnemyType  enemyType;

    [Header("Enemy Status")]
    public int enemyLevel;
    public float attackPoint;
    public float healthPoint;
    public float defensePoint;
    public float attackSpeed;
    public float moveSpeed;
    public float skillDamage;
}
