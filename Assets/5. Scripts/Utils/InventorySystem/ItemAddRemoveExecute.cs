using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemAddRemoveExecute : MonoBehaviour
{
    [SerializeField] private GameObject itemAlertPanel;
    [SerializeField] private ConsumableItem itemObject;
    [SerializeField] private ArmorItem itemObject2;
    private bool isOpenedOnce;

    void Start()
    {
        isOpenedOnce = false;
        itemAlertPanel.SetActive(false);
        ItemEvent();
    }

    private void ItemEvent()
    {
        itemAlertPanel.SetActive(true);
        if(!isOpenedOnce)
        {
            
            InventoryManager.Instance.AddItem(itemObject);
            InventoryManager.Instance.AddItem(itemObject2);
           
            itemAlertPanel.SetActive(false);
        }
        isOpenedOnce = true;

    }
}
