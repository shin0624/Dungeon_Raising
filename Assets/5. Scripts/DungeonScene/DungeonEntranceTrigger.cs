using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceTrigger : MonoBehaviour
{
    [SerializeField] private Canvas popupCanvas;
    private DungeonInfoPrintToPanel dungeonInfoPrintToPanel;

    private void Start()
    {
        dungeonInfoPrintToPanel = gameObject.GetComponent<DungeonInfoPrintToPanel>();//�ش� ������Ʈ�� ������ DungeonInfoPrintToPanel ��ũ��Ʈ�� �����´�. �� ������Ʈ���� ������ ��ũ���ͺ� ������Ʈ�� �ʿ��ϱ⿡, SerializeField�� �Ҵ����� �ʰ� ���� ������Ʈ ���������� �����ִ� DungeonInfoPrintToPanel Ŭ������ GetComponent�� �����´�.
    }

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
            Time.timeScale = 1;
            popupCanvas.gameObject.SetActive(false);
            dungeonInfoPrintToPanel.ClearDungeonInfo();//�˾� ĵ���� ��Ȱ��ȭ �� ��µǾ��� ���� ���� �ʱ�ȭ.
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("UNIT"))
        {
           if(popupCanvas != null && !popupCanvas.gameObject.activeSelf)
            {
                Time.timeScale = 0;
                popupCanvas.gameObject.SetActive(true);
                dungeonInfoPrintToPanel.SynchronizeDungeonInfo();//���� ���� ���.
            }     
        }
    }
}
