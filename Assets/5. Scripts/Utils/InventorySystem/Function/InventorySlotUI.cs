using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    //�κ��丮�� ItemSlot UI�� �����ϴ� ��ũ��Ʈ
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Button slotButton;
    private IInventoryItem currentItem;

    public void SetUp(IInventoryItem item)//�κ��丮 ����Ʈ �¾�. InventoryUIManager�� UpdateInventoryUI()���� ȣ��.
    {
        currentItem = item;
        itemIcon.sprite = item.ItemSprite;
        itemIcon.gameObject.SetActive(true);

        if(item is ConsumableItem consumable)//ConsumableItem���� ������ �����ϸ� true, �Ұ����ϸ� False.
        {
            amountText.text = consumable.itemAmount.ToString();//����(int)�� string���� ĳ�����ؼ� tmpro.text�� ����ȭ
            amountText.gameObject.SetActive(true);
        }
        else
        {
            amountText.gameObject.SetActive(false);//ArmorItem�̸� ���� ����� ���ʿ��ϹǷ� �ؽ�Ʈ�� ��Ȱ��ȭ.
        }
        slotButton.onClick.AddListener(OnSlotClicked);//���� ��ư Ŭ�� �̺�Ʈ �Ҵ�
    }

    public void Clear()//�κ��丮 ����Ʈ Ŭ����.
    {
        currentItem = null;
        itemIcon.gameObject.SetActive(false);
        amountText.gameObject.SetActive(false);
        slotButton.onClick.RemoveAllListeners();//��ư�� �����ʸ� ����
    }

    private void OnSlotClicked() => InventoryUIManager.Instance.ShowItemInfo(currentItem);//�����۽��� �������� ��ư Ŭ�� �� Ŭ���� �������� ������ ���.
}
