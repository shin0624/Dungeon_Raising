using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanelController : MonoBehaviour//���� �й� �� �������� losePanel�� ��Ʈ�ѷ�.
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

    private void OnTowerReturnButtonClicked()//�й��Ͽ� �ٽ� ������ �ִ� ������ ���ư��� ��ư.
    {
        StartCoroutine(DOTWeenUIAnimation.PopupDownCoroutineInUI(gameObject, Vector3.zero, 0.2f, "DungeonScene"));//�г��� �Ʒ��� ������ �ִϸ��̼�.
        // ������ ���������� ���ư��� �س�����, singleplayscene�� 10�� ������ ������ ��ȭ�ϱ� ������, ������ ������ �ʿ�.
    }

    private void OnMainSceneButtonClicked()//���� ������ ���ư��� ��ư.
    {
        StartCoroutine(DOTWeenUIAnimation.PopupDownCoroutineInUI(gameObject, Vector3.zero, 0.2f, "MainScene"));//�г��� �Ʒ��� ������ �ִϸ��̼�.
    }

    private void OnRetryButtonClicked()//���� �� ���� ������ �絵���ϴ� ��ư.
    {

    }
}

