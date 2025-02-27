using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HeroDatabase", menuName ="Hero/HeroDataBase")]
public class HeroDatabase : ScriptableObject
{
    public List<HeroInformation> heroInformationList;

}
