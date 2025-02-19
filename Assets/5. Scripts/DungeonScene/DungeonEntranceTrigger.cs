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
        if (popupCanvas != null && gameObject.scene.isLoaded) // ���� ����� ��쿡�� canvas ���� ������ �߻��ϴ� ���� Ȯ���Ͽ�, �̸� �����ϱ� ���� ������ �߰�.
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
