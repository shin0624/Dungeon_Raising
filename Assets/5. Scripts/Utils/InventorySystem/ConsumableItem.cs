using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable Item")]
public class ConsumableItem : ScriptableObject, IInventoryItem
{
    //�Ҹ� ������ �����͸� �����ϱ� ���� ��ũ���ͺ� ������Ʈ. 
    public string ItemName => itemName;
    public int ItemID => itemID;
    public Sprite ItemSprite => itemSprite;
    public ItemType Type => ItemType.Consumable;

    [Header("Basic Info")]
    public string itemName;//������ ��
    public int itemID;//�������� ������ ID. �Ҹ� �������� 1000���� �ѹ����Ǹ�, ������ ���̵�� �� ������ �������� ��Ÿ���Ƿ� �ߺ��� ���� �������� �־ ItemID�� �����ϴ�. 
    public Sprite itemSprite;//ItemSlot�� ǥ�õ� �������� �̹��� ��������Ʈ.

    [Header("Figure")]
    public int itemAmount;//������ ����

}
