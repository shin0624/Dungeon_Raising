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
        //InventoryCategoryManager.Instance.SwitchConsumableCategory();//ó�� �κ��丮�� �ε�Ǹ� ������ ī�װ��� ���õ�.

        itemButton.image.sprite = defaultSprite;
        armorButton.image.sprite = defaultSprite;
    }

    private IEnumerator DelayActive()
    {
        panels[0].SetActive(true);
        panels[1].SetActive(true);
        yield return null;
        panels[1].SetActive(false);
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
        if(!panels[0].activeSelf)
        {   
            InventoryCategoryManager.Instance.SwitchConsumableCategory();
            panels[0].SetActive(true);
            panels[1].SetActive(false);
            itemButton.image.sprite = buttonClckedSprite;//Ŭ���� ������ ��������Ʈ�� ����
            armorButton.image.sprite = defaultSprite;
        }
    }

    private void OnArmorButtonClicked()
    {
        if (InventoryCategoryManager.Instance == null)
        {
            Debug.LogError("InventoryCategoryManager �ν��Ͻ� ����!");
            return;
        }
        if(!panels[1].activeSelf)
        {
            InventoryCategoryManager.Instance.SwitchArmorCategory();
            panels[0].SetActive(false);
            panels[1].SetActive(true);
            armorButton.image.sprite = buttonClckedSprite;//Ŭ���� ������ ��������Ʈ�� ����
            itemButton.image.sprite = defaultSprite;
        }
      
    }
}
