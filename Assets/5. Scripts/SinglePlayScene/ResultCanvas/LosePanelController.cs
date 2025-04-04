using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanelController : MonoBehaviour//게임 패배 시 보여지는 losePanel의 컨트롤러.
{
    [SerializeField] private Button towerReturnButton;
    [SerializeField] private Button mainSceneButton;
    [SerializeField] private Button retryButton;
    
    private void OnEnable()
    {
        towerReturnButton.onClick.AddListener(OnTowerReturnButtonClicked);
        mainSceneButton.onClick.AddListener(OnMainSceneButtonClicked);
        retryButton.onClick.AddListener(OnRetryButtonClicked);
    }
    private void OnDisable()
    {
        towerReturnButton.onClick.RemoveListener(OnTowerReturnButtonClicked);
        mainSceneButton.onClick.RemoveListener(OnMainSceneButtonClicked);
        retryButton.onClick.RemoveListener(OnRetryButtonClicked);
    }

    private void OnTowerReturnButtonClicked()//패배하여 다시 던전이 있던 층으로 돌아가는 버튼.
    {
        StartCoroutine(DOTWeenUIAnimation.PopupDownCoroutineInUI(gameObject, Vector3.zero, 0.2f, "DungeonScene"));//패널을 아래로 내리는 애니메이션.
        // 지금은 던전씬으로 돌아가게 해놓지만, singleplayscene은 10층 단위로 컨셉이 변화하기 때문에, 적절한 변경이 필요.
    }

    private void OnMainSceneButtonClicked()//메인 씬으로 돌아가는 버튼.
    {
        StartCoroutine(DOTWeenUIAnimation.PopupDownCoroutineInUI(gameObject, Vector3.zero, 0.2f, "MainScene"));//패널을 아래로 내리는 애니메이션.
    }

    private void OnRetryButtonClicked()//현재 층 현재 던전을 재도전하는 버튼.
    {

    }
}

