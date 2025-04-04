using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonDatabase", menuName = "Dungeon/DungeonDatabase", order = 1)]
public class DungeonDatabase : ScriptableObject//�� ���� ���� ���� SO�� ����Ʈ�� �����Ѵ�. ����Ʈ�� �� 6��.(�� ������ 1����)
{
    public List<DungeonInformation> undergroundDungeonList = new List<DungeonInformation>();//������ ���� ����Ʈ.
    public List<DungeonInformation> hellDungeonList = new List<DungeonInformation>();//������ ���� ����Ʈ.
    public List<DungeonInformation> silvanDungeonList = new List<DungeonInformation>();//�ǹ� ���� ���� ����Ʈ.
    public List<DungeonInformation> KrisosDungeonList = new List<DungeonInformation>();//ũ���ҽ��� ���� ����Ʈ.
    public List<DungeonInformation> skyDungeonList = new List<DungeonInformation>();//õ���� ���� ����Ʈ.
    public List <DungeonInformation> lastFloorDungeonList = new List<DungeonInformation>();//���� �÷ξ� ���� ����Ʈ.

}
