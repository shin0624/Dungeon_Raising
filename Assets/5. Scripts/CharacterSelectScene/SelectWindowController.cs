using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectWindowController : MonoBehaviour
{
    //캐릭터 선택창 활/비활성화를 제어하는 스크립트.
    [SerializeField] private GameObject characterWindow;
    [SerializeField] private GameObject jobWindow;
    [SerializeField] private GameObject raceWindow;
    [SerializeField] private GameObject nicknameWindow;
    [SerializeField] private Image characterButton;
    [SerializeField] private Image jobButton;
    [SerializeField] private Image raceButton;
    private Color activeColor = new Color(0.6862745f, 0.5686275f, 0.1176471f, 1f );//활성화 시 색
    private Color inActiveColor = Color.white;//기본 색
    void Start()
    {
        ActivateWindow("Character");
    }

    public void ActivateWindow(string windowName)
    {
        // 모든 창을 비활성화하고 버튼 색상을 기본 색으로 설정
        characterWindow.SetActive(false);
        jobWindow.SetActive(false);
        raceWindow.SetActive(false);
        characterButton.color = inActiveColor;
        jobButton.color = inActiveColor;
        raceButton.color = inActiveColor;

        // 특정 창을 활성화하고 버튼 색상을 활성 색으로 변경
        switch (windowName)
        {
            case "Character"://캐릭터 선택 창
                characterWindow.SetActive(true);
                characterButton.color = activeColor;
                break;
            case "Job": // 직업 선택 창
                jobWindow.SetActive(true);
                jobButton.color = activeColor;
                break;
            case "Race"://종족 선택 창
                raceWindow.SetActive(true);
                raceButton.color = activeColor;
                break;
            case "Nickname": //닉네임 입력 창
                nicknameWindow.SetActive(true);
                characterButton.gameObject.SetActive(false);
                jobButton.gameObject.SetActive(false);
                raceButton.gameObject.SetActive(false);
                break;
            case "UserInfoSave":
            //추후 사용자 입력 데이터 저장 구현. 그 전에 닉네임 창에서 유효성 검사 로직 구현
                gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("Invalid window name: " + windowName);
                break;
        }
    }
     // 버튼 클릭 이벤트에 연결할 메서드. 각 윈도우에 있는 다음 버튼들의 OnClick이벤트에 할당한다.
    public void OnCharacterButtonClicked()
    {
        ActivateWindow("Character");
    }

    public void OnJobButtonClicked()
    {
        ActivateWindow("Job");
    }

    public void OnRaceButtonClicked()
    {
        ActivateWindow("Race");
    }

    public void OnNicknameButtonClicked()
    {
        ActivateWindow("Nickname");
    }
    
    public void OnOkayButtonClicked()
    {
        ActivateWindow("UserInfoSave");
    }
}
