using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "BossDatabase", menuName ="Enemy/BossDatabase")]
public class BossDatabase : ScriptableObject
{
    public List<BossInformation> bossInformationList;

}
