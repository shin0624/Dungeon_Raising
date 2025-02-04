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
    [SerializeField] private GameObject[] panels;

    private void OnEnable()
    {
        //인벤토리 창을 열었을 때 항상 ItemList가 홀드다운 되어있다.
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
            
            itemButton.image.sprite = buttonClckedSprite;//클릭된 상태의 스프라이트로 유지
            armorButton.image.sprite = defaultSprite;
        }
    }

    private void OnArmorButtonClicked()
    {
        if(!panels[1].activeSelf)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
           
            armorButton.image.sprite = buttonClckedSprite;//클릭된 상태의 스프라이트로 유지
            itemButton.image.sprite = defaultSprite;
        }
      
    }
}
