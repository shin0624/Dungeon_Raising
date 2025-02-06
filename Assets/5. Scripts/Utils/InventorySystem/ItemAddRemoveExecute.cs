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
        StartCoroutine(ItemEvent());
    }

    private IEnumerator ItemEvent()
    {
        if(!isOpenedOnce)
        {
            itemAlertPanel.SetActive(true);
            InventoryManager.Instance.AddItem(itemObject);
            InventoryManager.Instance.AddItem(itemObject2);
            yield return new WaitForSeconds(2.0f);
            itemAlertPanel.SetActive(false);
        }
        isOpenedOnce = true;

    }
}
