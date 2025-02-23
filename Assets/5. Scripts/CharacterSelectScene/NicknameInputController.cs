using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NicknameInputController : MonoBehaviour
{
    //닉네임을 입력 받으면 비속어 체크를 거쳐서 characterName을 업데이트 시키는 클래스
    [SerializeField] TMP_InputField inputNickname;//인풋필드 
    [SerializeField] TextMeshProUGUI characterName;//background 캔버스에 위치한 캐릭터 이름 텍스트
    [SerializeField] TextMeshProUGUI alertText;// 비속어 여부에 따라 경고 or 허용을 나타내는 텍스트
    [SerializeField] GameObject selectWindowPanel;//닉네임 입력 종료 시 기존 패널들 비활성화 후 인게임 화면으로.
    [SerializeField] private Button acceptButton;//닉네임 입력 완료 버튼
    [SerializeField] private GameObject yesOrNoPanel;//닉네임 사용 여부 확인 패널
    [SerializeField] private Button yesButton;//닉네임 사용 확인 버튼
    [SerializeField] private Button noButton;//닉네임 사용 취소 버튼
    
    [SerializeField] private BadWordFilter badWordFilter;
    [SerializeField] private InputFieldSetting inputFieldSetting;
    //[SerializeField] private AsyncSceneLoader asyncSceneLoader;
     
    void Start()
    {
        StartCoroutine(DelayInitBadWordFilter());
        inputNickname.onSubmit.AddListener(OnNicknameSubmit);//인풋 필드에서 Enter 키 입력 시 비속어 체크 이벤트를 시작
        acceptButton.onClick.AddListener(()=>OnNicknameSubmit(inputNickname.text));//닉네임 입력 완료 버튼 클릭 시 비속어 체크 이벤트를 시작
        yesOrNoPanel.SetActive(false);//닉네임 사용 여부 확인 패널 비활성화
    }

    private IEnumerator DelayInitBadWordFilter()//비속어 필터링 sdk의 초기화여부 체크를 0.5초 지연시킨다. BadWordFilter에서의 초기화가 먼저 진행된 후 초기화 여부를 체크해야 하기 때문. 두 메서드 사이의 순서를 정하지 않으면 초기화 여부가 먼저 False가 된 후 초기화가 진행될 수 있어 에러가 유발될 수 있으므로.
    {
        yield return new WaitForSeconds(0.5f);
        Init();
    }

    private void Init()
    {
        if(badWordFilter==null)//badwordfilter컴포넌트 유무 체크
        {
            badWordFilter = gameObject.GetComponent<BadWordFilter>();
        }
        if(!badWordFilter.IsInitialized())//SDK 초기화 유무 체크
        {
            Debug.LogError("비속어 필터링 SDK가 초기화되지 않았습니다. 확인이 필요합니다.");
            return;
        }
    }

    private void OnNicknameSubmit(string text)//닉네임의 비속어 여부에 따라 submit할 것인지 결정. 인풋 필드에서 입력받은 inputNickname.text를 인자로 받는다.
    {
        if(IsBadWord(text))//비속어일 경우(isbadword=true)
        {
            StartCoroutine(AcceptButtonColorChange());//완료 버튼 색상 변경 및 비활성화
            ShowMessage("비속어는 사용할 수 없습니다.");//경고출력
            ReActiveInputField();//인풋 필드 초기화. 다시 작성
        }
        else if(string.IsNullOrEmpty(text))//닉네임이 비어있을 경우
        {
            StartCoroutine(AcceptButtonColorChange());//완료 버튼 색상 변경 및 비활성화
            ShowMessage("닉네임을 입력해주세요.");//경고출력
            ReActiveInputField();//인풋 필드 초기화. 다시 작성
        }
        else
        {
            ShowMessage("사용 가능한 닉네임 입니다. 사용하시겠습니까?");//사용 여부를 확인하는 패널이 활성화된다. 이후 yes버튼을 누르면 GoToInGame()메서드가 실행된다. no버튼을 누르면 인풋필드가 초기화된다.
            ActivateYesOrNoPanel(text);
        }
    }

    private void ActivateYesOrNoPanel(string text)
    {
        acceptButton.gameObject.SetActive(false);
        yesOrNoPanel.SetActive(true);
        yesButton.onClick.AddListener(() => OnYesButtonClicked(text));
        noButton.onClick.AddListener(OnNoButtonClicked);
    }

    private void OnYesButtonClicked(string text)
    {
        yesOrNoPanel.SetActive(false);
        UpdateCharacterName(text);
        GoToInGame();
    }
    private void OnNoButtonClicked()
    {
        yesOrNoPanel.SetActive(false);
        ShowMessage("닉네임을 입력해주세요.");//
        acceptButton.gameObject.SetActive(true);
        ReActiveInputField();
    }

    private bool IsBadWord(string text)//닉네임의 비속어 여부를 확인
    {
        return badWordFilter.FilterFunc(text);//뒤끝 sdk의 비속어 필터 기능을 사용하여 비속어 여부를 확인한다. FilterFunc에서 비속어가 걸리면 true, 비속어가 없으면 false가 전달된다.
    }

    private void UpdateCharacterName(string text)
    {
        inputFieldSetting.UpdateFontSizeAndText(text);//캐릭터 이름을 인풋필드에 작성된 이름으로 업데이트
        PlayerInfo.Instance.SetPlayerNickname(text);//작성한 닉네임을 PlayerInfo로 전송
    }

    private void ShowMessage(string alertMessage)//비속어 발견여부 메세지 출력
    {
        alertText.text = alertMessage;
    }
    private void GoToInGame()
    {
        selectWindowPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
        //asyncSceneLoader.LoadMainSceneAsync();//비동기 로드 시도
    }

    private void ReActiveInputField()
    {
        inputNickname.text = "";// 인풋 필드 텍스트 초기화
        inputNickname.ActivateInputField();//인풋 필드 다시 활성화
    }

    private IEnumerator AcceptButtonColorChange()//완료 버튼 색상 변경 및 비활성화
    {
        acceptButton.interactable = false;//닉네임 입력 완료 버튼 비활성화
        acceptButton.image.color = Color.Lerp(Color.red, Color.white, 0.5f);
        yield return new WaitForSeconds(0.5f);
        acceptButton.image.color = new Color(0.09f, 0.5f, 1.0f, 1.0f);
        acceptButton.interactable = true;//닉네임 입력 완료 버튼 활성화
    }
}
