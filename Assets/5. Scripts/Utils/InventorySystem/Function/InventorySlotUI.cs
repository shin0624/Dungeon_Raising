using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    //인벤토리의 ItemSlot UI를 관리하는 스크립트
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Button slotButton;
    private IInventoryItem currentItem;

    public void SetUp(IInventoryItem item)//인벤토리 리스트 셋업. InventoryUIManager의 UpdateInventoryUI()에서 호출.
    {
        currentItem = item;
        itemIcon.sprite = item.ItemSprite;
        itemIcon.gameObject.SetActive(true);

        if(item is ConsumableItem consumable)//ConsumableItem으로 리턴이 가능하면 true, 불가능하면 False.
        {
            amountText.text = consumable.itemAmount.ToString();//수량(int)을 string으로 캐스팅해서 tmpro.text로 동기화
            amountText.gameObject.SetActive(true);
        }
        else
        {
            amountText.gameObject.SetActive(false);//ArmorItem이면 수량 출력이 불필요하므로 텍스트를 비활성화.
        }
        slotButton.onClick.AddListener(OnSlotClicked);//슬롯 버튼 클릭 이벤트 할당
    }

    public void Clear()//인벤토리 리스트 클리어.
    {
        currentItem = null;
        itemIcon.gameObject.SetActive(false);
        amountText.gameObject.SetActive(false);
        slotButton.onClick.RemoveAllListeners();//버튼의 리스너를 제거
    }

    private void OnSlotClicked() => InventoryUIManager.Instance.ShowItemInfo(currentItem);//아이템슬롯 프리팹의 버튼 클릭 시 클릭된 아이템의 정보를 출력.
}
