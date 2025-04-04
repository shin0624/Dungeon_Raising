using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WinPanelController : MonoBehaviour// ���� �¸� �� �������� winPanel�� ��Ʈ�ѷ�.
{
    [SerializeField] private Button nextDungeonButton;//���� ���� ��ư�̱� �ѵ�, Ÿ������ ��ġ�� ���� ���ƴٴϸ� ������ ã�ƾ� �ϱ� ������, DungeonScene���� ���ư����� �Ѵ�.
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

    private void OnTowerReturnButtonClicked()//�й��Ͽ� �ٽ� ������ �ִ� ������ ���ư��� ��ư.
    {
        StartCoroutine(DOTWeenUIAnimation.PopupDownCoroutineInUI(gameObject, Vector3.zero, 0.2f, "DungeonScene"));//�г��� �Ʒ��� ������ �ִϸ��̼�.
        // ������ ���������� ���ư��� �س�����, singleplayscene�� 10�� ������ ������ ��ȭ�ϱ� ������, ������ ������ �ʿ�.
    }

    private void OnMainSceneButtonClicked()//���� ������ ���ư��� ��ư.
    {
        StartCoroutine(DOTWeenUIAnimation.PopupDownCoroutineInUI(gameObject, Vector3.zero, 0.2f, "MainScene"));//�г��� �Ʒ��� ������ �ִϸ��̼�.
    }
}
