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
    [SerializeField] private GameObject[] panels;

    private void OnEnable()
    {
        //�κ��丮 â�� ������ �� �׻� ItemList�� Ȧ��ٿ� �Ǿ��ִ�.
        panels[0].SetActive(true);
        panels[1].SetActive(false);
        itemButton.image.sprite = defaultSprite;
        armorButton.image.sprite = defaultSprite;
    }
    private void Start() 
    {
        itemButton.onClick.AddListener(OnItemButtonClicked);
        armorButton.onClick.AddListener(OnArmorButtonClicked);    
    }
    private void OnItemButtonClicked()
    {
        if(!panels[0].activeSelf)
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);
            
            itemButton.image.sprite = buttonClckedSprite;//Ŭ���� ������ ��������Ʈ�� ����
            armorButton.image.sprite = defaultSprite;
        }
    }

    private void OnArmorButtonClicked()
    {
        if(!panels[1].activeSelf)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
           
            armorButton.image.sprite = buttonClckedSprite;//Ŭ���� ������ ��������Ʈ�� ����
            itemButton.image.sprite = defaultSprite;
        }
      
    }
}
