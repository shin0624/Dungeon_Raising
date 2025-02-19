using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DungeonInformation", menuName = "Dungeon/DungeonInformation")]
public class DungeonInformation : ScriptableObject
{
    //������ ������ ������ ��ũ���ͺ� ������Ʈ.
    //������ �ʿ��� ������ [�����̸�, ȹ�� ���� ���� 1, 2�� Sprite, Amount]
    [Header("Dungeon Info")]
    public string dungeonName;//���� �̸�.
    public Sprite firstRewardSprite;//ù ��° ���� ��������Ʈ.
    public Sprite secondRewardSprite;//�� ��° ���� ��������Ʈ.
    public int firstRewardAmount;//ù ��° ����.
    public int secondRewardAmount;//�� ��° ����.
}
