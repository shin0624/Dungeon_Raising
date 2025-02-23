using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "Item/ItemDataBase")]
public class ItemDatabase : ScriptableObject
{
    //보유한 아이템 개수에 따라 인벤토리 슬롯을 동적 생성하도록 하기 위해, 아이템 정보를 저장하는 아이템 데이터베이스를 스크립터블 오브젝트로 선언한다.
    public List<ConsumableItem> consumableItems;
    public List<ArmorItem> armorItems;
}
