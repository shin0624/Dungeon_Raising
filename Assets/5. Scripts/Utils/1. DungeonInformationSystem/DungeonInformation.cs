using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DungeonInformation", menuName = "Dungeon/DungeonInformation")]
public class DungeonInformation : ScriptableObject
{
    //던전의 정보를 저장할 스크립터블 오브젝트.
    //던전에 필요한 정보는 [던전이름, 획득 가능 보상 1, 2의 Sprite, Amount]
    [Header("Dungeon Info")]
    public string dungeonName;//던전 이름.
    public string dungeonID;//던전 ID. 대략 250개의 던전이 필요하므로, 3자리 숫자로 설정. 추후 이벤트 던전 등 추가 시 영문자를 추가해야 하므로 string으로 설정.
    public int floorNumber; //이 던전이 속한 층.
    public Sprite firstRewardSprite;//첫 번째 보상 스프라이트.
    public Sprite secondRewardSprite;//두 번째 보상 스프라이트.
    public int firstRewardAmount;//첫 번째 보상량.
    public int secondRewardAmount;//두 번째 보상량.

    [Header("Dungeon Unit Info")]
    public EnemyType enemyType;
    public BossType bossType;
}
