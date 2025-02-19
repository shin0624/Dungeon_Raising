using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceTrigger : MonoBehaviour
{
    [SerializeField] private Canvas popupCanvas;

    private void OnEnable()
    {
        if (popupCanvas != null)
        {
            popupCanvas.gameObject.SetActive(false);
        }
    }
    private void OnDisable() 
    {
        if (popupCanvas != null && gameObject.scene.isLoaded) // 씬이 변경된 경우에도 canvas 참조 오류가 발생하는 것을 확인하여, 이를 방지하기 위해 제한을 추가.
        {
            popupCanvas.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
           if(popupCanvas != null && !popupCanvas.gameObject.activeSelf)
            {
                Time.timeScale = 0;
                popupCanvas.gameObject.SetActive(true);
            }     
        }
    }
}
