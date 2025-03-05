using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataManager : MonoBehaviour
{
    public HeroInformation heroInformation;

    void Start()
    {
        Debug.Log($"HeroUnit Init - {heroInformation.heroName}, {heroInformation.heroRaise}, {heroInformation.heroType}, {heroInformation.heroGrade}, {heroInformation.heroLevel}");
    }

}
