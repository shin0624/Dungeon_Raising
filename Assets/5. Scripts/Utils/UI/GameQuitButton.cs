using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameQuitButton : MonoBehaviour
{
    //로그아웃 버튼 클릭 시 : logOutPanel 활성화
    //예 -> 게임 종료 / 아니오 -> logOutPanel 비활성화
    //본 클래스는 SettingPanel에 붙는다.
    [SerializeField] private Button logOutButton;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    [SerializeField] private GameObject logOutPanel;
    private void OnEnable()//SettingPanel이 활성화 될 때 마다 로그아웃패널을 비활성화하여 시작 -> 만약 로그아웃패널이 활성화된 상태에서 closeButton을 클릭한 후 다시 SettingPanel을 활성화시켰을 때 로그아웃패널 활성화상태를 방지하기 위함
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
