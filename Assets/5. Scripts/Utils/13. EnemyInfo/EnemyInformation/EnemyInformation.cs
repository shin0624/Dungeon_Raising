using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="EnemyInformation", menuName = "Enemy/EnemyInformation")]
public class EnemyInformation : ScriptableObject
{
    //에너미의 정보를 저장할 스크립터블 오브젝트.

    [Header("Enemy Info")]
    public GameObject enemyPrefab;
    public EnemyType  enemyType;

    [Header("Enemy Status")]
    public int enemyLevel;
    public float enemyHP;
    public float enemyDP;//방어력
    public float enemyAttackSpeed;
    public float enemyMoveSpeed;
}
