using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResultUIController : MonoBehaviour
{
    // Single Play Scene의 자동 전투 결과에 따라 ResultCanvas를 제어하는 스크립트.
    // UnitList와 EnemyList 중 하나의 리스트가 비어있는 상태가 되면 ResultCanvas를 활성화한다.
    // UnitList가 비었을 경우 : 플레이어 패배 -> LostPanel을 활성화.
    // EnemyList가 비었을 경우 : 플레이어 승리 -> WinPanel을 활성화.
    [SerializeField] private Canvas resultCanvas;//resultCanvas는 sortOrder가 모든 캔버스보다 높은 1 이므로 먼저 활성화 해준다.
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GoToCurrentFloor goToCurrentFloor;

    void Start()
    {
        DOTween.Init();

        resultCanvas.gameObject.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        winPanel.transform.localScale = Vector3.one * 0.1f;
        losePanel.transform.localScale = Vector3.one * 0.1f; 
    }

    private void OnDisable()// 씬 비활성화 시 다시 모든 ResultCanvas를 비활성화.
    {
        resultCanvas.gameObject.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        if(Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
    }

    public void PlayerWin()// 플레이어 승리 시 -> winPanel 활성화.
    {
        resultCanvas.gameObject.SetActive(true);
        
        goToCurrentFloor.ClearProcess(PlayerInfo.Instance.GetPlayerFloor());// 현재 던전 클리어 처리, 해당 층 클리어 여부 체크
        
        StartCoroutine(LateActivePanel(winPanel));
    }

    public void PlayerLose()// 플레이어 패배 시 -> LosePanel 활성화.
    {
        resultCanvas.gameObject.SetActive(true);
        StartCoroutine(LateActivePanel(losePanel));
    }

    private void DoTweenTest(GameObject panel)
    {
        panel.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(panel.transform.DOScale(1.2f, 0.5f));
        seq.Append(panel.transform.DOScale(1.0f, 0.2f));

        seq.Play();
    }

    private IEnumerator LateActivePanel(GameObject panel)
    {
        yield return new WaitForSeconds(0.5f);
        
        DoTweenTest(panel);

        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0.0f;
    }


}
