using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerProgressData//��ü 50���� ���� ��Ȳ�� ������ Ŭ����.
{
    public Dictionary<int, DungeonFloorData> floorProgress = new Dictionary<int, DungeonFloorData>();//�� ��ȣ�� ���� ���� Ŭ���� ���θ� �����ϴ� ��ųʸ�. �� ��ȣ�� Ű�� ����ϰ�, ���� ���� Ŭ���� ���θ� ������ ����Ѵ�.
}
