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

    void Start()
    {
        InventoryManager.Instance.AddItem(itemObject);
        InventoryManager.Instance.AddItem(itemObject2);
        StartCoroutine(ItemEvent());
    }

    private IEnumerator ItemEvent()
    {
        itemAlertPanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        itemAlertPanel.SetActive(false);
    }
}
