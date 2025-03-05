using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDataManager : MonoBehaviour
{
    public SoldierInformation soldierInformation;

    void Start()
    {
        Debug.Log($"SoldierUnit Init - {gameObject.name}, {soldierInformation.soldierType}, {soldierInformation.soldierLevel}");
    }
}
