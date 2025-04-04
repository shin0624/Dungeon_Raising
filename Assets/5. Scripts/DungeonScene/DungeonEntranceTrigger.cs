using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceTrigger : MonoBehaviour
{
    [SerializeField] private Canvas popupCanvas;
    [SerializeField] private GameObject dungeonEnterPanel;
    private DungeonInfoPrintToPanel dungeonInfoPrintToPanel;


    private void Start()
    {
        dungeonInfoPrintToPanel = gameObject.GetComponent<DungeonInfoPrintToPanel>();//�ش� ������Ʈ�� ������ DungeonInfoPrintToPanel ��ũ��Ʈ�� �����´�. �� ������Ʈ���� ������ ��ũ���ͺ� ������Ʈ�� �ʿ��ϱ⿡, SerializeField�� �Ҵ����� �ʰ� ���� ������Ʈ ���������� �����ִ� DungeonInfoPrintToPanel Ŭ������ GetComponent�� �����´�.
        Time.timeScale = 1;
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
            
            dungeonInfoPrintToPanel.ClearDungeonInfo();//�˾� ĵ���� ��Ȱ��ȭ �� ��µǾ��� ���� ���� �ʱ�ȭ.
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Unit_Player"))
        {
           if(popupCanvas != null && !popupCanvas.gameObject.activeSelf)
            {  
                StartCoroutine(PopUI());
            } 
        }
    }

    private IEnumerator PopUI()//���� �Ա� Ʈ���� ĵ������ TimeScale�� ���� ���� ����ȭ�� �ʿ��ϱ� ������, �ڷ�ƾ���� ������ ���� Ÿ�̹��� �����Ѵ�.
    {
        popupCanvas.gameObject.SetActive(true);
        dungeonEnterPanel.SetActive(true);//���� �Ա� �г� Ȱ��ȭ.
        yield return new WaitForEndOfFrame();         
        dungeonInfoPrintToPanel.SynchronizeDungeonInfo();//���� ���� ���.
        yield return new WaitForEndOfFrame();
        DOTWeenUIAnimation.PopupAnimationInUI(dungeonEnterPanel, dungeonEnterPanel.transform.localScale * 0.2f, 0.001f, dungeonEnterPanel.transform.localScale, 0.2f);
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 0;
    }
}
