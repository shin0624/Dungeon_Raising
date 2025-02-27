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
    public float enemyHP;
    public float enemyDP;//����
    public float enemyAttackSpeed;
    public float enemyMoveSpeed;
}
