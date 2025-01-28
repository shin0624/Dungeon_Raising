using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSelectManager : MonoBehaviour
{
    //직업 선택 창 매니저 : 선택된 캐릭터 성별에 따라 [직업 선택창]에서 클릭할 수 있는 직업이 달라짐. 
    //클릭한 성별에 따른 버튼 제한은 SelectWindwoController의 RestrictJobButtonByGender()에서 구현
    //본 클래스에서는 playerGender값(Male, Female)에 따라 직업 버튼에 각각 다른 값을 전달하고, 전달받은 값에 따라 직업 버튼 클릭 시 그에 맞는 패널이 나오도록 제어.
    // playerGender : Male -> 검사, 궁수 버튼에 Male 전달 -> Male[검사, 궁수]배열로 연결됨. 
    // playerGender : Female -> 궁수, 마법사 버튼에 Female 전달 -> Female[궁수, 마법사]배열로 연결됨
    // Male, Female 배열에는 각 성별 별 직업에 맞는 패널이 저장되어있고, 이 패널의 MotionWindow에 캐릭터 도트가 표시. 스킬 버튼 클릭 시 캐릭터 도트의 파라미터 전환.
    // 검사 버튼이 홀드다운된 상태에서 궁수 버튼을 클릭 시 검사 패널은 비활성화, 궁수 패널이 활성화됨.

    private string genderValue ="";
    [SerializeField] private GameObject[] maleJobPanels;//남성 직업 패널. [0] : 검사, [1] : 궁수
    [SerializeField] private GameObject[] femaleJobPanels;//여성 직업 패널 [0] : 궁수, [1] : 마법사
    [SerializeField] private Button knightButton;
    [SerializeField] private Button archerButton;
    [SerializeField] private Button magicianButton;
    [SerializeField] private GameObject jobWindow;

    void Start()
    {
        DeactiveAllPanels();//하이어라키에 존재하는 남/여 직업별 패널을 모두 비활성화
        StartCoroutine(WaitForGenderSelection());//성별 선택 완료시 까지 대기
    }

    private void OnKnightButtonClicked()
    {
        ActiveSpecificPanel(maleJobPanels, 0);
        PlayerInfo.Instance.SetPlayerJob("Knight");
    }

    private void OnMaleArcherButtonClicked()
    {
        ActiveSpecificPanel(maleJobPanels, 1);
        PlayerInfo.Instance.SetPlayerJob("Archer");
    }

    private void OnFemaleButtonClicked()
    {
        ActiveSpecificPanel(femaleJobPanels, 0);
    }

    private void OnMagicianButtonClicked()
    {
        ActiveSpecificPanel(femaleJobPanels, 1);
        PlayerInfo.Instance.SetPlayerJob("Magician");
    }

    private IEnumerator WaitForGenderSelection()// 성별 선택이 완료될 때 까지 대기한다. 업데이트문에서 성별 선택 완료 감지 로직을 작성하면 성별 선택이 완료된 후에도 조건문 확인이 계속 발생하므로 코루틴으로 처리.
    {
        while(string.IsNullOrEmpty(genderValue) && !jobWindow.activeSelf)//성별 값이 NULL이 아니게 될 때 까지 + [성별 선택 창]에서 [직업 선택 창]으로 넘어올 때 까지 대기
        {
            genderValue = PlayerInfo.Instance.GetPlayerGender();//설정된 성별 값을 저장.
            yield return null;//한 프레임 대기
        }
        SetJobPanel(genderValue);
    }

    private void SetMaleJobPanel()//성별이 남자일 경우
    {
        ActiveJobPanel(maleJobPanels);
          // 기존 리스너 제거 후 새 리스너 추가(이벤트 중복 등록 방지)
        knightButton.onClick.RemoveAllListeners();
        archerButton.onClick.RemoveAllListeners();

        knightButton.onClick.AddListener(OnKnightButtonClicked);
        archerButton.onClick.AddListener(OnMaleArcherButtonClicked);//궁수는 남/여 공통 직업이고, 보여줄 패널만 다르기 때문에 매개변수 패널에 따라서 궁수 버튼에 다른 이벤트를 등록.
    }   

    private void SetFemaleJobPanel()//성별이 여자일 경우
    {
        ActiveJobPanel(femaleJobPanels);

        // 기존 리스너 제거 후 새 리스너 추가(이벤트 중복 등록 방지)
        archerButton.onClick.RemoveAllListeners();
        magicianButton.onClick.RemoveAllListeners();

        archerButton.onClick.AddListener(OnFemaleButtonClicked);
        magicianButton.onClick.AddListener(OnMagicianButtonClicked);
    }

    private void ActiveJobPanel(GameObject[] panels)//선택한 패널을 활성화하는 함수. 이전에 열려있던 패널들을 모두 비활성화 한 후 버튼으로 선택된 성별의 직업 패널만 활성화.
    {
        DeactiveAllPanels();//만약 활성화된 패널이 존재한다면 닫는다. 
        if(panels.Length >0)
        {
            panels[0].SetActive(true);//[직업 선택 창]이 열리면 항상 첫 번째 직업이 액티브 된 상태
        }
    }

    private void ActiveSpecificPanel(GameObject[] panels, int index)// 클릭된 버튼이 가리키는 패널만 활성화하고 다른 패널은 비활성화시키는 메서드. 각 버튼 이벤트에서 호출.
    {
        DeactiveAllPanels();
        if(index >=0 && index < panels.Length)
        {
            panels[index].SetActive(true);//해당하는 패널을 활성화.
        }
    }

    private void DeactiveAllPanels()//열려있는 직업 패널을 비활성화. 성별 선택 시 변경 가능하도록 수정하였기 때문에, 성별 선택 시 이전 모든 성별 패널들을 전부 비활성화시킬 수 있도록 DeactiveAllPanels의 매개변수를 없애고 모든 패널에 대해 반복을 수행한다.
    {
         // 모든 패널 비활성화 (남성, 여성 모두)
        foreach (GameObject panel in maleJobPanels)
        {
            if (panel != null) panel.SetActive(false);
        }
        foreach (GameObject panel in femaleJobPanels)
        {
            if (panel != null) panel.SetActive(false);
        }
    }

    public void SetJobPanel(string text)//성별에 따라 직업 패널을 설정하는 메서드. 인자로는 genderValue값을 전달받음.
    {
        switch(text)
        {
            case "Male":
                SetMaleJobPanel();
                break;

            case "Female":
                SetFemaleJobPanel();
                break;

            default : 
                Debug.LogError("Unknown gender value. Please Check.");
                break;
        }
    }
}
