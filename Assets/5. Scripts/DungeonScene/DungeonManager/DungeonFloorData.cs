using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//������ json���� �����ϱ� ���� Serializable�� ����.
public class DungeonFloorData// �� ���� ���� Ŭ���� ���θ� �����ϴ� Ŭ����.
{
    public Dictionary<string, bool> clearedDungeons = new Dictionary<string, bool>();//����ID�Ϳ� Ŭ���� ���θ� �����ϴ� ��ųʸ�. ���� �̸��� Ű�� ����ϰ�, Ŭ���� ���θ� ������ ����Ѵ�.
}
