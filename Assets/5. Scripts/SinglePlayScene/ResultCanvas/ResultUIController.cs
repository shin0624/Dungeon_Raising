using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResultUIController : MonoBehaviour
{
    // Single Play Scene�� �ڵ� ���� ����� ���� ResultCanvas�� �����ϴ� ��ũ��Ʈ.
    // UnitList�� EnemyList �� �ϳ��� ����Ʈ�� ����ִ� ���°� �Ǹ� ResultCanvas�� Ȱ��ȭ�Ѵ�.
    // UnitList�� ����� ��� : �÷��̾� �й� -> LostPanel�� Ȱ��ȭ.
    // EnemyList�� ����� ��� : �÷��̾� �¸� -> WinPanel�� Ȱ��ȭ.
    [SerializeField] private Canvas resultCanvas;//resultCanvas�� sortOrder�� ��� ĵ�������� ���� 1 �̹Ƿ� ���� Ȱ��ȭ ���ش�.
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

    private void OnDisable()// �� ��Ȱ��ȭ �� �ٽ� ��� ResultCanvas�� ��Ȱ��ȭ.
    {
        resultCanvas.gameObject.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        if(Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
    }

    public void PlayerWin()// �÷��̾� �¸� �� -> winPanel Ȱ��ȭ.
    {
        resultCanvas.gameObject.SetActive(true);
        
        goToCurrentFloor.ClearProcess(PlayerInfo.Instance.GetPlayerFloor());// ���� ���� Ŭ���� ó��, �ش� �� Ŭ���� ���� üũ
        
        StartCoroutine(LateActivePanel(winPanel));
    }

    public void PlayerLose()// �÷��̾� �й� �� -> LosePanel Ȱ��ȭ.
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
