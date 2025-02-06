using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    //�κ��丮�� ItemSlot UI�� �����ϴ� ��ũ��Ʈ.
    //--���� �����տ��� �Ҵ��� ��--
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Button slotButton;
    //---������ Ÿ��
    private IInventoryItem currentItem;
    private ConsumableItem consumableItem;
    private ArmorItem armorItem;
    //--���� ��--
    private string itemName;
    private ItemType type;
    private int itemId;
    //--Consumable ��--
    private int itemAmount;
    //--Armor ��--
    private ItemGrade[] itemGrade;
    private ItemPart[] itemPart;
    private int itemLevel;
    private int levelUpCost;
    private int gradeUpCost;
    private float offensivePower;
    private float defensivePower;


    public void SetUp(IInventoryItem item)//�κ��丮 ����Ʈ �¾�. InventoryUIManager�� UpdateInventoryUI()���� ȣ��.
    {   
        CommonDataSetiing(item);
        gameObject.SetActive(true);
        switch(item.Type)
        {
            case ItemType.Consumable:
                consumableItem = (ConsumableItem)item;
                ConsumableSetting(consumableItem);
                Debug.Log("�¾� �޼��� ȣ�� : ������ Ÿ�� Consumable");
                break;
            case ItemType.Armor:
                armorItem = (ArmorItem)item;
                ArmorSetting(armorItem);
                Debug.Log("�¾� �޼��� ȣ�� : ������ Ÿ�� Armor");
                break;
        }
        slotButton.onClick.AddListener(OnSlotClicked);//���� ��ư Ŭ�� �̺�Ʈ �Ҵ�
        Debug.Log("���� ��ư �̺�Ʈ �Ҵ� �Ϸ�, �¾� �޼��� ���� ����");
    }

    private void CommonDataSetiing(IInventoryItem item)//���� ������ ����
    {
        itemName = item.ItemName;
        itemIcon.sprite = item.ItemSprite;//slotPrefab�� ��������Ʈ�� item�� ��������Ʈ�� ����
        type = item.Type;
        itemId = item.ItemID;
        itemIcon.gameObject.SetActive(true);
    }

    private void ConsumableSetting(ConsumableItem a)//�Һ� �������� ���� ����ȭ
    {
       itemAmount =  a.itemAmount;
       amountText.text = itemAmount.ToString();
       amountText.gameObject.SetActive(true);
    }

    private void ArmorSetting(ArmorItem b)// ��� ������ ����
    {
        amountText.text = "";
        itemGrade = b.itemGrade;
        itemPart = b.itemParts;
        itemLevel = b.itemLevel;
        levelUpCost = b.levelUpCost;
        gradeUpCost = b.gradeUpCost;
        offensivePower = b.offensivePower;
        defensivePower = b.defensivePower;
    
        amountText.gameObject.SetActive(false);//ArmorItem�̸� ���� ����� ���ʿ��ϹǷ� �ؽ�Ʈ�� ��Ȱ��ȭ.
    }

    public void Clear()//�κ��丮 ����Ʈ Ŭ����.
    {
        if (itemIcon != null) itemIcon.gameObject.SetActive(false);
        if (amountText != null) amountText.gameObject.SetActive(false);
        if (slotButton != null) slotButton.onClick.RemoveAllListeners();
    }

    private void OnSlotClicked() => InventoryUIManager.Instance.ShowItemInfo(currentItem);//�����۽��� �������� ��ư Ŭ�� �� Ŭ���� �������� ������ ���.
}
