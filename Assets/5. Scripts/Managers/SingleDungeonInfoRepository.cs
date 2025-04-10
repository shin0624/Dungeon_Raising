using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDungeonInfoRepository : MonoBehaviour
{
    // Managers에 할당하는 정보저장용 클래스. EnemyType, BossType 변수를 가지고 있음. 기본값은 Empty라는 더미값으로, SinglePlayScene 진입 시 타입 체크 메서드에서 Empty값이 확인되면 던전 정보 전달이 제대로 되지 않았다는 의미이다.

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
