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
    public string dungeonID;//���� ID. �뷫 250���� ������ �ʿ��ϹǷ�, 3�ڸ� ���ڷ� ����. ���� �̺�Ʈ ���� �� �߰� �� �����ڸ� �߰��ؾ� �ϹǷ� string���� ����.
    public int floorNumber; //�� ������ ���� ��.
    public Sprite firstRewardSprite;//ù ��° ���� ��������Ʈ.
    public Sprite secondRewardSprite;//�� ��° ���� ��������Ʈ.
    public int firstRewardAmount;//ù ��° ����.
    public int secondRewardAmount;//�� ��° ����.

    [Header("Dungeon Unit Info")]
    public EnemyType enemyType;
    public BossType bossType;
}
