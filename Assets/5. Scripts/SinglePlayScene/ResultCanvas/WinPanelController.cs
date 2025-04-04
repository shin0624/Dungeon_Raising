using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WinPanelController : MonoBehaviour// 게임 승리 시 보여지는 winPanel의 컨트롤러.
{
    [SerializeField] private Button nextDungeonButton;//다음 던전 버튼이긴 한데, 타워에서 터치를 통해 돌아다니며 던전을 찾아야 하기 때문에, DungeonScene으로 돌아가도록 한다.
    [SerializeField] private Button mainSceneButton;

    private void OnEnable()
    {
        nextDungeonButton.onClick.AddListener(OnTowerReturnButtonClicked);
        mainSceneButton.onClick.AddListener(OnMainSceneButtonClicked);
    }
    private void OnDisable()
    {
        nextDungeonButton.onClick.RemoveListener(OnTowerReturnButtonClicked);
        mainSceneButton.onClick.RemoveListener(OnMainSceneButtonClicked);
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
}
