using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonDatabase", menuName = "Dungeon/DungeonDatabase", order = 1)]
public class DungeonDatabase : ScriptableObject//각 층의 던전 정보 SO를 리스트로 관리한다. 리스트는 총 6개.(각 층마다 1개씩)
{
    public List<DungeonInformation> undergroundDungeonList = new List<DungeonInformation>();//지하의 던전 리스트.
    public List<DungeonInformation> hellDungeonList = new List<DungeonInformation>();//연옥의 던전 리스트.
    public List<DungeonInformation> silvanDungeonList = new List<DungeonInformation>();//실반 숲의 던전 리스트.
    public List<DungeonInformation> KrisosDungeonList = new List<DungeonInformation>();//크리소스의 던전 리스트.
    public List<DungeonInformation> skyDungeonList = new List<DungeonInformation>();//천궁의 던전 리스트.
    public List <DungeonInformation> lastFloorDungeonList = new List<DungeonInformation>();//보스 플로어 던전 리스트.

}
