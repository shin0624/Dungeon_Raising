using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WinPanelController : MonoBehaviour// ���� �¸� �� �������� winPanel�� ��Ʈ�ѷ�.
{
    [SerializeField] private Button towerReturnButton;//���� ���� ��ư�̱� �ѵ�, Ÿ������ ��ġ�� ���� ���ƴٴϸ� ������ ã�ƾ� �ϱ� ������, DungeonScene���� ���ư����� �Ѵ�.
    [SerializeField] private Button mainSceneButton;
    [SerializeField] private GoToCurrentFloor goToCurrentFloor;

    private void OnEnable()
    {
        towerReturnButton.onClick.AddListener(OnTowerReturnButtonClicked);
        mainSceneButton.onClick.AddListener(OnMainSceneButtonClicked);
    }
    private void OnDisable()
    {
        towerReturnButton.onClick.RemoveListener(OnTowerReturnButtonClicked);
        mainSceneButton.onClick.RemoveListener(OnMainSceneButtonClicked);
    }

    private void OnTowerReturnButtonClicked()//�й��Ͽ� �ٽ� ������ �ִ� ������ ���ư��� ��ư.
    {
        string nextDungeon = goToCurrentFloor.GoToNextDungeon(PlayerInfo.Instance.GetPlayerFloor());

        StartCoroutine(DOTWeenUIAnimation.PopupDownCoroutineInUI(gameObject, Vector3.zero, 0.2f, nextDungeon));//�г��� �Ʒ��� ������ �ִϸ��̼�.
    }

    private void OnMainSceneButtonClicked()//���� ������ ���ư��� ��ư.
    {
        StartCoroutine(DOTWeenUIAnimation.PopupDownCoroutineInUI(gameObject, Vector3.zero, 0.2f, "MainScene"));//�г��� �Ʒ��� ������ �ִϸ��̼�.
    }
}
