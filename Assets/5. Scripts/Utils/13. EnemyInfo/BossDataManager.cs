using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDataManager : MonoBehaviour
{
    public BossInformation bossInformation;

    void Start()
    {
        Debug.Log($"BossUnit Init -{gameObject.name}, {bossInformation.bossType}, {bossInformation.bossLevel}");
    }


}
