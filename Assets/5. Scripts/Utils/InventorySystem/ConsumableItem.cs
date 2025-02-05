using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable Item")]
public class ConsumableItem : ScriptableObject, IInventoryItem
{
    //소모성 아이템 데이터를 저장하기 위한 스크립터블 오브젝트. 
    public string ItemName => itemName;
    public int ItemID => itemID;
    public Sprite ItemSprite => itemSprite;
    public ItemType Type => ItemType.Consumable;

    [Header("Basic Info")]
    public string itemName;//아이템 명
    public int itemID;//아이템의 고유한 ID. 소모성 아이템은 1000부터 넘버링되며, 아이템 아이디는 한 종류의 아이템을 나타내므로 중복된 동일 아이템이 있어도 ItemID는 동일하다. 
    public Sprite itemSprite;//ItemSlot에 표시될 아이템의 이미지 스프라이트.

    [Header("Figure")]
    public int itemAmount;//아이템 수량

}
