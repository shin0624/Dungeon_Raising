using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithItemList : MonoBehaviour
{
    //���尣 ui�� ��� ������ ��ư Ŭ�� �� ItemDatabase�� ��ϵ� �ش� ���� ������ ����� ItemList�� ����Ѵ�. ItemList�������� ItemListElement(������ Image, ������ �� string)���� ����.
    // BlackSmithPlayerItemInfo.cs���� Ŭ�� �޼����� �Ű����� ItemType ���� ��ġ�ϴ� type�� �������� �����ϸ� -> �� Ÿ���� ���� �������� sprite�� text�� ������ ����Ѵ�.
    // --> �ϴ� ScrollView ��ü�� ItemList�� ItemDatabase �� ������ ���� ��ŭ�� ItemListElement�� ����ϰ� �ؾ� �Ѵ�.

    // ItemList�� ��µ�  ItemListElement�� Ŭ���ϸ� �� ����� Sprite, Level, Offence/DeffenceAttackRightPivot�� �� �̹����� 
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private GameObject itemListElementPrefab;
    [SerializeField] private Transform content;//��ũ�� ���� Content��ü. ��, �����۸���Ʈ ������Ʈ�� ������ �θ�.
    [SerializeField] private BlackSmithItemElementClicker blackSmithItemElementClicker;
    private List<GameObject> instantiatedElements = new List<GameObject>();//������ ������ ����Ʈ�� ����.

    public void DelayPopulateItemList(ItemPart part)
    {
        StartCoroutine(PopulateItemList(part));
    }

    private IEnumerator PopulateItemList(ItemPart part)// ������ ����Ʈ UI�� �׷��� �Ŀ� UI��Ұ� ������Ʈ�� �� �ֵ��� �ڷ�ƾ���� 0.01���� �����̸� �ش�.
    {
        bool isExist = itemDatabase.armorItems.Exists(i => i.itemParts == part);//�ش� ��Ʈ�� �������� �ִٸ� true, ������ false.
        
        foreach(GameObject item in instantiatedElements)
        {
            Destroy(item);//������ �����Ǿ��� ������ ����Ʈ�� �ϴ� �����Ͽ� �ʱ�ȭ.
        }
        instantiatedElements.Clear();

        if(isExist)//part�� �ش��ϴ� �������� ������ ���
        {
            Debug.Log($"������ ����Ʈ�� ���Ƚ��ϴ�. : {part}");
            yield return new WaitForSeconds(0.01f);

            foreach(ArmorItem armorItem in itemDatabase.armorItems)//ArmorItem ����Ʈ�� ��ȸ�ϸ� ���ο� ��ҵ��� �߰��Ѵ�.
            {
                if(armorItem.itemParts == part)//������ db�� �����ϴ� armorItem �� �Ű������� ���޹��� part�� ������ part�� ��ϵ� armorItem�� �ش� ������ ����Ʈ�� ����Ѵ�.
                {
                    GameObject newItem = Instantiate(itemListElementPrefab, content);//content�� �ڽ����� ������ ����Ʈ ������Ʈ�� ����.

                    newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite = armorItem.ItemSprite;//������ ����Ʈ ������Ʈ�� UI�� ������Ʈ.
                    newItem.transform.Find("ArmorName").GetComponent<TextMeshProUGUI>().text = armorItem.itemName;
                    instantiatedElements.Add(newItem);//����Ʈ�� �߰��Ͽ� �����Ѵ�.
                    
                    blackSmithItemElementClicker.AddListenerToElementButton(newItem, armorItem);
                } 
            }
        }
        else//part�� �ش��ϴ� �������� �������� ���� ���
        {
            Debug.Log($"{part}�� �ش��ϴ� �������� �����ϰ� ���� �ʽ��ϴ�.");
        }
    }
}
