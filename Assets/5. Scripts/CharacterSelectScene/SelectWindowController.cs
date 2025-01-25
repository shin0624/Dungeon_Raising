using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectWindowController : MonoBehaviour
{
    //캐릭터 선택창 활/비활성화를 제어하는 스크립트.
    // [캐릭터 선택 창] -> 남자 캐릭터, 여자 캐릭터 버튼 존재. 둘 중 하나를 클릭하지 않았을 경우 "다음"버튼 비활성화
    // [직업 선택 창] -> 남자 캐릭터 선택 시 검사, 궁수 선택 가능. 마법사는 비활성화 | 여자 캐릭터 선택 시 궁수, 마법사 선택 가능. 검사는 비활성화 | 둘 중 하나를 클릭하지 않았을 경우 "다음"버튼 비활성화
    // [종족 선택 창]
    [SerializeField] private GameObject characterWindow;
    [SerializeField] private GameObject jobWindow;
    [SerializeField] private GameObject raceWindow;
    [SerializeField] private GameObject nicknameWindow;
    [SerializeField] private Image characterButton;
    [SerializeField] private Image jobButton;
    [SerializeField] private Image raceButton;
    //---------250126 캐릭터 속성 별 직업 버튼 활/비활 여부 추가
    [SerializeField] private Button knightButton;//검사 버튼
    [SerializeField] private Button archerButton;//궁수 버튼
    [SerializeField] private Button magicianButton;//마법사 버튼
    [SerializeField] private Button maleCharacterButton;//남성 캐릭터 버튼
    [SerializeField] private Button femaleCharacterButton;//여성 캐릭터 버튼
    [SerializeField] private GameObject failureText;//성별 선택하지 않은 채 "다음"버튼을 눌렀을 때 출력되는 경고팝업
    private string playerGender = "";//플레이어 성별
    //-------------------------------------------------------------------
    private Color activeColor = new Color(0.6862745f, 0.5686275f, 0.1176471f, 1f );//활성화 시 컬러
    private Color inActiveColor = Color.white;//기본 색
    void Start()
    {
        ActivateWindow("Character");
        failureText.SetActive(false);
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
                StartCoroutine(LoadPanelMaleOrFemale());
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
        ChoiceCheck();//성별 선택 여부 검사 후 창 변경 로직 수행
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

    public void OnMaleButtonClicked()//남자 캐릭터 버튼 클릭 시 true
    {
        playerGender = "Male";
        PlayerInfo.Instance.PlayerGenderCheck(playerGender);
        Debug.Log("Male Clicked. PlayerGender : " + playerGender);
    }

    public void OnFemaleButtonClicked()//여자 캐릭터 버튼 클릭 시 false
    {
        playerGender = "Female";
        PlayerInfo.Instance.PlayerGenderCheck(playerGender);
        Debug.Log("Female Clicked. PlayerGender : " + playerGender);
    }

    private IEnumerator LoadPanelMaleOrFemale()//[캐릭터 선택 창]에서 선택한 성별에 따라서 [직업 선택 창]에서 선택할 수 있는 직업의 버튼이 달라진다.
    {
        yield return null;
        if(PlayerInfo.Instance.GetPlayerGender()=="Male")
        {
            knightButton.interactable = true;
            archerButton.interactable = true;
            magicianButton.interactable = false;
        }
        if(PlayerInfo.Instance.GetPlayerGender()=="Female")
        {
            knightButton.interactable = false;
            archerButton.interactable = true;
            magicianButton.interactable = true;
        }
        else
        {
            Debug.Log("PlayerGender is not set.");
        }
    }

    private void ChoiceCheck()//성별 버튼이 눌린 상태에서 "다음"버튼을 클릭했다면 동작. 성별 버튼을 누르지 않았다면 "다음" 버튼 비활성화. 비활성화 상태에서 버튼 클릭 시 FailureText 출력
    {
        if(PlayerInfo.Instance.GetPlayerGender()!=null)
        {
            ActivateWindow("Job");
        }
        else
        {
            Debug.Log("PlayerGender is not set.");
            StartCoroutine(ActiveFailureText());
        }
    }

    private IEnumerator ActiveFailureText()//경고 팝업 출력 코루틴
    {
        failureText.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        failureText.SetActive(false);
    }
}
