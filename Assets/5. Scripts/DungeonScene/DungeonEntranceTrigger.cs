using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceTrigger : MonoBehaviour
{
    [SerializeField] private Canvas popupCanvas;

    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!popupCanvas.gameObject.activeSelf)
            {
                Time.timeScale = 0;
                popupCanvas.gameObject.SetActive(true);
            }     
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(popupCanvas.gameObject.activeSelf)
            {
                Time.timeScale = 1;
                popupCanvas.gameObject.SetActive(false);
            }     
        }
    }


}
