using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithItemList : MonoBehaviour
{
    //대장간 ui의 장비 아이템 버튼 클릭 시 ItemDatabase에 등록된 해당 부위 아이템 목록을 ItemList에 출력한다. ItemList프리팹은 ItemListElement(아이콘 Image, 아이콘 명 string)으로 구성.
    // BlackSmithPlayerItemInfo.cs에서 클릭 메서드의 매개변수 ItemType 값과 일치하는 type의 아이템이 존재하면 -> 그 타입을 가진 아이템의 sprite와 text를 가져와 출력한다.
    // --> 일단 ScrollView 객체인 ItemList가 ItemDatabase 내 아이템 개수 만큼만 ItemListElement를 출력하게 해야 한다.

    // ItemList에 출력된  ItemListElement를 클릭하면 그 장비의 Sprite, Level, Offence/DeffenceAttackRightPivot의 두 이미지에 
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private GameObject itemListElementPrefab;
    [SerializeField] private Transform content;//스크롤 뷰의 Content객체. 즉, 아이템리스트 엘리먼트가 생성될 부모.
    [SerializeField] private BlackSmithItemElementClicker blackSmithItemElementClicker;
    private List<GameObject> instantiatedElements = new List<GameObject>();//생성된 아이템 리스트를 관리.

    public void DelayPopulateItemList(ItemPart part)
    {
        StartCoroutine(PopulateItemList(part));
    }

    private IEnumerator PopulateItemList(ItemPart part)// 아이템 리스트 UI가 그려진 후에 UI요소가 업데이트될 수 있도록 코루틴으로 0.01초의 딜레이를 준다.
    {
        bool isExist = itemDatabase.armorItems.Exists(i => i.itemParts == part);//해당 파트의 아이템이 있다면 true, 없으면 false.
        
        foreach(GameObject item in instantiatedElements)
        {
            Destroy(item);//기존에 생성되었던 아이템 리스트를 일단 제거하여 초기화.
        }
        instantiatedElements.Clear();

        if(isExist)//part에 해당하는 아이템이 존재할 경우
        {
            Debug.Log($"아이템 리스트가 열렸습니다. : {part}");
            yield return new WaitForSeconds(0.01f);

            foreach(ArmorItem armorItem in itemDatabase.armorItems)//ArmorItem 리스트를 순회하며 새로운 요소들을 추가한다.
            {
                if(armorItem.itemParts == part)//아이템 db에 존재하는 armorItem 중 매개변수로 전달받은 part와 동일한 part로 등록된 armorItem만 해당 아이템 리스트에 출력한다.
                {
                    GameObject newItem = Instantiate(itemListElementPrefab, content);//content의 자식으로 아이템 리스트 엘리먼트를 생성.

                    newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite = armorItem.ItemSprite;//아이템 리스트 엘리먼트의 UI를 업데이트.
                    newItem.transform.Find("ArmorName").GetComponent<TextMeshProUGUI>().text = armorItem.itemName;
                    instantiatedElements.Add(newItem);//리스트에 추가하여 추적한다.
                    
                    blackSmithItemElementClicker.AddListenerToElementButton(newItem, armorItem);
                } 
            }
        }
        else//part에 해당하는 아이템이 존재하지 않을 경우
        {
            Debug.Log($"{part}에 해당하는 아이템을 보유하고 있지 않습니다.");
        }
    }
}
