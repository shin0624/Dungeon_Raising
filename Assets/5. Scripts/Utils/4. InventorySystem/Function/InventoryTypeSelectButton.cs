using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTypeSelectButton : MonoBehaviour
{
 //�� ��ũ��Ʈ���� ���ǵ� ��ư �޼������ ���ؼ� �κ��丮 Ÿ���� ��ȭ�Ѵ�. (Item / Armor) -> InventoryType�� ���� �κ��丮 �гο� ��µǴ� ����Ʈ�� ��ȭ.
    [SerializeField] private Button itemButton;
    [SerializeField] private Button armorButton;
    [SerializeField] private Sprite buttonClckedSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject[] panels;//0 : itmeList, 1 : ArmorList

    private void OnEnable()
    {
        //�κ��丮 â�� ������ �� �׻� ItemList�� Ȧ��ٿ� �Ǿ��ִ�.
        StartCoroutine(DelayActive()); 
    }

    private IEnumerator DelayActive()
    {
         yield return new WaitUntil(() => //�ν��Ͻ� �ʱ�ȭ ���� ����
        InventoryCategoryManager.Instance != null &&
        InventoryUIManager.Instance != null &&
        InventoryManager.Instance != null
            );
        panels[0].SetActive(true);       
        panels[1].SetActive(true);
        
        itemButton.image.sprite = defaultSprite;
        armorButton.image.sprite = defaultSprite;
        panels[1].SetActive(false);
        InventoryCategoryManager.Instance.SwitchConsumableCategory();//ó�� �κ��丮�� �ε�Ǹ� ������ ī�װ��� ���õǵ��� �ʱ�ȭ
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
            Debug.LogError("InventoryCategoryManager �ν��Ͻ� ����!");
            return;
        }
        if(panels[0].activeSelf) return;//�̹� Ȱ��ȭ�� ��� ����

        InventoryCategoryManager.Instance.SwitchConsumableCategory();
        UpdateUI(true);//ī�װ� ��ȯ
    }

    private void OnArmorButtonClicked()
    {
        if (InventoryCategoryManager.Instance == null)
        {
            Debug.LogError("InventoryCategoryManager �ν��Ͻ� ����!");
            return;
        }
        if(panels[1].activeSelf) return;//�̹� Ȱ��ȭ�� ��� ����

        InventoryCategoryManager.Instance.SwitchArmorCategory();
        UpdateUI(false);//ī�װ� ��ȯ
      
    }

    private void UpdateUI(bool isItemPanelActive)
    {
        //�г� Ȱ��, ��Ȱ�� ��ȯ
        panels[0].SetActive(isItemPanelActive);
        panels[1].SetActive(!isItemPanelActive);

        //��ư ��������Ʈ ������Ʈ
        itemButton.image.sprite = isItemPanelActive ? buttonClckedSprite : defaultSprite;
        armorButton.image.sprite = !isItemPanelActive ? buttonClckedSprite : defaultSprite;

        // ���� �ʱ�ȭ
        if (InventoryUIManager.Instance != null)
        {
            InventoryUIManager.Instance.InitSlots();
        }
        else
        {
            Debug.LogError("InventoryUIManager �ν��Ͻ� ����!");
        }
    }
}
