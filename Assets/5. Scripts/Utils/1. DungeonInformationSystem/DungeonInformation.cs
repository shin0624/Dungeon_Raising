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
    public Sprite firstRewardSprite;//첫 번째 보상 스프라이트.
    public Sprite secondRewardSprite;//두 번째 보상 스프라이트.
    public int firstRewardAmount;//첫 번째 보상량.
    public int secondRewardAmount;//두 번째 보상량.
}
