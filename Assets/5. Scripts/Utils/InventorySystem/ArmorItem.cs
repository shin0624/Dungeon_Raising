using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Armor Item")]
public class ArmorItem : ScriptableObject, IInventoryItem
{
    //장비 아이템 데이터를 저장하기 위한 스크립터블 오브젝트.

    public string ItemName => itemName;
    public int ItemID => itemID;
    public Sprite ItemSprite => itemSprite;
    public ItemType Type => ItemType.Armor;

    [Header("Basic Info")]
    public string itemName;//아이템 명
    public int itemID;//아이템 고유의 아이디.장비 아이템은 2000부터 넘버링되며, 아이템 아이디는 한 종류의 아이템을 나타내므로 중복된 동일 아이템이 있어도 ItemID는 동일하다
    public ItemGrade [] itemGrade;//장비 아이템의 업그레이드를 위한 아이템 등급(총 다섯 등급 존재) -> 열거
    public  ItemPart[] itemParts;//아이템의 부위. [머리, 어깨, 가슴, 팔, 다리, 무기]-> 열거. 무기 아이템의 경우 무기(Weapon)로 고정.
    public Sprite itemSprite;//아이템 스프라이트 이미지
    
    [Header("Stats")]
    public int itemLevel;//아이템의 레벨. 장비 레벨은 1부터 10까지 존재하며, 레벨 업을 통해 10레벨의 장비를 완성했을 경우 다음 등급으로 승급이 가능.
    public int levelUpCost;//레벨업에 사용되는 재화의 수
    public int gradeUpCost;//장비레벨 조건 충족 후 등급 업그레이드를 위해 필요한 재화의 수
    public float offensivePower;//무기 아이템의 공격력. 만약 무기 아이템이 아닐 경우 null 또는 0으로 저장.
    public float defensivePower;//무기를 제외한 아이템의 방어력. 만약 무기 아이템일 경우 null 또는 0으로 저장.

}
