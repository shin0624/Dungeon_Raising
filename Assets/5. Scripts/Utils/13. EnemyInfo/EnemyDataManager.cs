using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    public EnemyInformation enemyInformation;

    void Start()
    {
        Debug.Log($"EenemyUnit Init -{gameObject.name}, {enemyInformation.enemyType}, {enemyInformation.enemyLevel}");
    }

}
