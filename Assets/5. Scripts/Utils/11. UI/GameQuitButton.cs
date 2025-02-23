using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameQuitButton : MonoBehaviour
{
    //�α׾ƿ� ��ư Ŭ�� �� : logOutPanel Ȱ��ȭ
    //�� -> ���� ���� / �ƴϿ� -> logOutPanel ��Ȱ��ȭ
    //�� Ŭ������ SettingPanel�� �ٴ´�.
    [SerializeField] private Button logOutButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private GameObject logOutPanel;
    private void OnEnable()//SettingPanel�� Ȱ��ȭ �� �� ���� �α׾ƿ��г��� ��Ȱ��ȭ�Ͽ� ���� -> ���� �α׾ƿ��г��� Ȱ��ȭ�� ���¿��� closeButton�� Ŭ���� �� �ٽ� SettingPanel�� Ȱ��ȭ������ �� �α׾ƿ��г� Ȱ��ȭ���¸� �����ϱ� ����
    {
        logOutPanel.SetActive(false);
    }
    void Start()
    {
        logOutButton.onClick.AddListener(OnLogOutButtonClicked);
        yesButton.onClick.AddListener(YesButtonClicked);
        noButton.onClick.AddListener(NoButtonClicked);
    }

    private void OnLogOutButtonClicked()
    {
        if(!logOutPanel.activeSelf)
        {
            logOutPanel.SetActive(true);
        }
        else
        {
            logOutPanel.SetActive(false);
        }
    }

    private void YesButtonClicked()
    {
        Application.Quit();
    }

    private void NoButtonClicked()
    {
        if(logOutPanel.activeSelf)
        {
            logOutPanel.SetActive(false);
        }
    }

}
