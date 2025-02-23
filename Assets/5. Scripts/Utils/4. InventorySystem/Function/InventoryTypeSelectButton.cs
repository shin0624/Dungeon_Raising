using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTypeSelectButton : MonoBehaviour
{
 //본 스크립트에서 정의된 버튼 메서드들을 통해서 인벤토리 타입이 변화한다. (Item / Armor) -> InventoryType에 따라 인벤토리 패널에 출력되는 리스트가 변화.
    [SerializeField] private Button itemButton;
    [SerializeField] private Button armorButton;
    [SerializeField] private Sprite buttonClckedSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject[] panels;//0 : itmeList, 1 : ArmorList

    private void OnEnable()
    {
        //인벤토리 창을 열었을 때 항상 ItemList가 홀드다운 되어있다.
        StartCoroutine(DelayActive()); 
    }

    private IEnumerator DelayActive()
    {
         yield return new WaitUntil(() => //인스턴스 초기화 순서 제어
        InventoryCategoryManager.Instance != null &&
        InventoryUIManager.Instance != null &&
        InventoryManager.Instance != null
            );
        panels[0].SetActive(true);       
        panels[1].SetActive(true);
        
        itemButton.image.sprite = defaultSprite;
        armorButton.image.sprite = defaultSprite;
        panels[1].SetActive(false);
        InventoryCategoryManager.Instance.SwitchConsumableCategory();//처음 인벤토리가 로드되면 아이템 카테고리가 선택되도록 초기화
    }
    private void Start() 
    {
        itemButton.onClick.AddListener(OnItemButtonClicked);
        armorButton.onClick.AddListener(OnArmorButtonClicked);    
    }
    private void OnItemButtonClicked()
    {
        if (InventoryCategoryManager.Instance == null)
        {
            Debug.LogError("InventoryCategoryManager 인스턴스 없음!");
            return;
        }
        if(panels[0].activeSelf) return;//이미 활성화된 경우 무시

        InventoryCategoryManager.Instance.SwitchConsumableCategory();
        UpdateUI(true);//카테고리 전환
    }

    private void OnArmorButtonClicked()
    {
        if (InventoryCategoryManager.Instance == null)
        {
            Debug.LogError("InventoryCategoryManager 인스턴스 없음!");
            return;
        }
        if(panels[1].activeSelf) return;//이미 활성화된 경우 무시

        InventoryCategoryManager.Instance.SwitchArmorCategory();
        UpdateUI(false);//카테고리 전환
      
    }

    private void UpdateUI(bool isItemPanelActive)
    {
        //패널 활성, 비활성 전환
        panels[0].SetActive(isItemPanelActive);
        panels[1].SetActive(!isItemPanelActive);

        //버튼 스프라이트 업데이트
        itemButton.image.sprite = isItemPanelActive ? buttonClckedSprite : defaultSprite;
        armorButton.image.sprite = !isItemPanelActive ? buttonClckedSprite : defaultSprite;

        // 슬롯 초기화
        if (InventoryUIManager.Instance != null)
        {
            InventoryUIManager.Instance.InitSlots();
        }
        else
        {
            Debug.LogError("InventoryUIManager 인스턴스 없음!");
        }
    }
}
