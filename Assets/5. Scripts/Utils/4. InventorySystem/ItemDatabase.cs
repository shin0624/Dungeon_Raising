using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "Item/ItemDataBase")]
public class ItemDatabase : ScriptableObject
{
    //������ ������ ������ ���� �κ��丮 ������ ���� �����ϵ��� �ϱ� ����, ������ ������ �����ϴ� ������ �����ͺ��̽��� ��ũ���ͺ� ������Ʈ�� �����Ѵ�.
    public List<ConsumableItem> consumableItems;
    public List<ArmorItem> armorItems;
}
