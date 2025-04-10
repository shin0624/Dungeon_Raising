using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDungeonInfoRepository : MonoBehaviour
{
    // Managers�� �Ҵ��ϴ� ��������� Ŭ����. EnemyType, BossType ������ ������ ����. �⺻���� Empty��� ���̰�����, SinglePlayScene ���� �� Ÿ�� üũ �޼��忡�� Empty���� Ȯ�εǸ� ���� ���� ������ ����� ���� �ʾҴٴ� �ǹ��̴�.

    public EnemyType enemyType;
    public BossType bossType;
    public DungeonInformation dungeonInformation;

    private void Start()
    {
        enemyType = EnemyType.Empty;
        bossType = BossType.Empty;
        dungeonInformation.dungeonID = "";
    }




}
