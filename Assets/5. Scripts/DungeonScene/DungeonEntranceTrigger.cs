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
        dungeonInfoPrintToPanel = gameObject.GetComponent<DungeonInfoPrintToPanel>();//해당 오브젝트에 부착된 DungeonInfoPrintToPanel 스크립트를 가져온다. 각 오브젝트마다 독립된 스크립터블 오브젝트가 필요하기에, SerializeField로 할당하지 않고 던전 오브젝트 개별적으로 갖고있는 DungeonInfoPrintToPanel 클래스를 GetComponent로 가져온다.
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
        if (popupCanvas != null && gameObject.scene.isLoaded) // 씬이 변경된 경우에도 canvas 참조 오류가 발생하는 것을 확인하여, 이를 방지하기 위해 제한을 추가.
        {
            Time.timeScale = 1;
            
            dungeonInfoPrintToPanel.ClearDungeonInfo();//팝업 캔버스 비활성화 시 출력되었던 던전 정보 초기화.
           
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

    private IEnumerator PopUI()//던전 입구 트리거 캔버스는 TimeScale과 던전 정보 동기화가 필요하기 때문에, 코루틴으로 프레임 단위 타이밍을 조절한다.
    {
        popupCanvas.gameObject.SetActive(true);
        dungeonEnterPanel.SetActive(true);//던전 입구 패널 활성화.
        yield return new WaitForEndOfFrame();         
        dungeonInfoPrintToPanel.SynchronizeDungeonInfo();//던전 정보 출력.
        yield return new WaitForEndOfFrame();
        DOTWeenUIAnimation.PopupAnimationInUI(dungeonEnterPanel, dungeonEnterPanel.transform.localScale * 0.2f, 0.001f, dungeonEnterPanel.transform.localScale, 0.2f);
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 0;
    }
}
