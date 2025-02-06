using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Armor Item")]
public class ArmorItem : ScriptableObject, IInventoryItem
{
    //��� ������ �����͸� �����ϱ� ���� ��ũ���ͺ� ������Ʈ.

    public string ItemName => itemName;
    public int ItemID => itemID;
    public Sprite ItemSprite => itemSprite;
    public ItemType Type => ItemType.Armor;

    [Header("Basic Info")]
    public string itemName;//������ ��
    public int itemID;//������ ������ ���̵�.��� �������� 2000���� �ѹ����Ǹ�, ������ ���̵�� �� ������ �������� ��Ÿ���Ƿ� �ߺ��� ���� �������� �־ ItemID�� �����ϴ�
    public ItemGrade [] itemGrade;//��� �������� ���׷��̵带 ���� ������ ���(�� �ټ� ��� ����) -> ����
    public  ItemPart[] itemParts;//�������� ����. [�Ӹ�, ���, ����, ��, �ٸ�, ����]-> ����. ���� �������� ��� ����(Weapon)�� ����.
    public Sprite itemSprite;//������ ��������Ʈ �̹���
    
    [Header("Stats")]
    public int itemLevel;//�������� ����. ��� ������ 1���� 10���� �����ϸ�, ���� ���� ���� 10������ ��� �ϼ����� ��� ���� ������� �±��� ����.
    public int levelUpCost;//�������� ���Ǵ� ��ȭ�� ��
    public int gradeUpCost;//��񷹺� ���� ���� �� ��� ���׷��̵带 ���� �ʿ��� ��ȭ�� ��
    public float offensivePower;//���� �������� ���ݷ�. ���� ���� �������� �ƴ� ��� null �Ǵ� 0���� ����.
    public float defensivePower;//���⸦ ������ �������� ����. ���� ���� �������� ��� null �Ǵ� 0���� ����.

}
