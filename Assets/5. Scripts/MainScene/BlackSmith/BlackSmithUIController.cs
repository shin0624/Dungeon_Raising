using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithUIController : MonoBehaviour
{
    //대장간 UI 컨트롤러. 대장간은 2개의 업그레이드 패널 위에서 캐릭터 강화, 영웅 강화 두 기능을 수행한다.
    // 캐릭터 업그레이드 패널 : 버튼 4개 ( 장비 이미지 그리드는 프리팹으로 슬롯 불러오기를 구현할 것이므로 본 클래스에서 버튼을 구현하지 않음.)
    // 영웅 업그레이드 패널 : 버튼 4개 ( 영웅 이미지 그리드는 프리팹으로 슬롯 불러오기를 구현할 것이므로 본 클래스에서 버튼을 구현하지 않음.)
    // 각 패널에는 서브패널(레벨업 / 승급) 2개가 존재.
    // 메인 패널은 각각 관리, 서브 패널은 메인 패널 당 1개씩 배열을 생성하여 관리. -> 캐릭터 메인 패널[캐릭터 레벨업, 캐릭터 승급], 영웅 메인 패널[영웅 레벨업, 영웅 승급]
    //본 클래스는 BlackSmithPanel에 할당한다.
    [SerializeField] private Button[] characterPanelButtons = new Button[4];//캐릭터 업그레이드 패널에 존재할 버튼 (0 : 레벨업, 1 : 승급, 2 : 레벨업 수행, 3 : 승급 수행)
    [SerializeField] private Button[] heroPanelButtons = new Button[4];//영웅 업그레이드 패널에 존재할 버튼 (0 : 레벨업, 1 : 승급, 2 : 레벨업 수행, 3 : 승급 수행)
    [SerializeField] private GameObject[] characterPanels = new GameObject[2];//캐릭터창 서브패널. [0] : 레벨업, [1] : 승급
    [SerializeField] private GameObject[] heroPanels = new GameObject[2];//영웅 창 서브패널. [0] : 레벨업, [1] : 승급급
    [SerializeField] private GameObject characterMainPanel;
    [SerializeField] private GameObject heroMainPanel;
    [SerializeField] private Button characterButton;
    [SerializeField] private Button heroButton;

    //추후 재화 부족 텍스트 출력

    private void OnEnable() 
    {
        RemoveAllListeners();//이벤트 중복 등록을 방지하기 위해, 처음 활성화 시 모든 리스너 초기화.
        characterMainPanel.SetActive(true);//처음 대장간 창이 오픈되면 캐릭터 메인패널이 출력됨.
        AttachAllListeners();//각 패널의 버튼에 리스너를 등록.
    }
    private void OnDisable() 
    {
        RemoveAllListeners();//각 패널의 버튼 리스너들을 모두 제거.
        DeActiveAllPanels();//모든 패널을 비활성화.
    }

    private void AttachAllListeners()//리스너 등록
    {
        characterButton.onClick.AddListener(OnCharacterButtonClicked);
        heroButton.onClick.AddListener(OnHeroButtonClicked);  

        characterPanelButtons[0].onClick.AddListener(CharacterLevelUpPanelButtonClicked);
        characterPanelButtons[1].onClick.AddListener(CharacterUpgradePanelButtonClicked);
        characterPanelButtons[2].onClick.AddListener(PerformCharacterLevelUp);
        characterPanelButtons[3].onClick.AddListener(PerformCharacterUpgrade);

        heroPanelButtons[0].onClick.AddListener(HeroLevelUpPanelButtonClicked);
        heroPanelButtons[1].onClick.AddListener(HerorUpgradePanelButtonClicked);
        heroPanelButtons[2].onClick.AddListener(PerformHeroLevelUp);
        heroPanelButtons[3].onClick.AddListener(PerformHeroUpgrade);

    }

    private void RemoveAllListeners()//OnDisable()에서 호출할 모든 리스너 제거 메서드
    {
        foreach (Button button in characterPanelButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        foreach (Button button in heroPanelButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        characterButton.onClick.RemoveListener(OnCharacterButtonClicked);
        heroButton.onClick.RemoveListener(OnHeroButtonClicked);
        
    }

    private void DeActiveAllPanels()//OnDisable()에서 호출할 모든 패널 비활성화 메서드
    {
        characterMainPanel.SetActive(false);
        heroMainPanel.SetActive(false);
        foreach(GameObject subPanel in characterPanels)
        {
            subPanel.SetActive(false);
        }
        foreach(GameObject subPanel in heroPanels)
        {
            subPanel.SetActive(false);
        }
    }

//----------------메인 패널 전환 버튼 이벤트-------------------------------------

    private void OnCharacterButtonClicked()
    {
        if(!characterMainPanel.activeSelf)
        {
            characterMainPanel.SetActive(true);
            heroMainPanel.SetActive(false);
        }
    }

    private void OnHeroButtonClicked()
    {
        if(!heroMainPanel.activeSelf)
        {
            heroMainPanel.SetActive(true);
            characterMainPanel.SetActive(false);
        }
    }
//----------------캐릭터 업그레이드 패널 버튼 이벤트-------------------------------------

    private void CharacterLevelUpPanelButtonClicked()//캐릭터버튼[0] : 캐릭터 레벨업 패널(interactionPanels[0,1]) 출력
    {
        if(characterMainPanel.activeSelf && !characterPanels[0].activeSelf)
        {
            characterPanels[0].SetActive(true);
            characterPanels[1].SetActive(false);
        }
    }

    private void CharacterUpgradePanelButtonClicked()//캐릭터버튼[1] : 캐릭터 승급 패널(interactionPanels[0,2]) 출력
    {
        if(characterMainPanel.activeSelf && !characterPanels[1].activeSelf)
        {
            characterPanels[1].SetActive(true);
            characterPanels[0].SetActive(false);
        }
    }

    private void PerformCharacterLevelUp()//캐릭터 패널 레벨 업
    {
        Debug.Log("캐릭터 레벨업");
    }

    private void PerformCharacterUpgrade()//캐릭터 패널 승급
    {
        Debug.Log("캐릭터 승급");

    }

    //----------------영웅 업그레이드 패널 버튼 이벤트-------------------------------------

    private void HeroLevelUpPanelButtonClicked()//영웅버튼[0] : 영웅 레벨업 패널(interactionPanels[1,1]) 출력
    {
        if(heroMainPanel.activeSelf && !heroPanels[0].activeSelf)
        {
            heroPanels[0].SetActive(true);
            heroPanels[1].SetActive(false);
        }
    }

    private void HerorUpgradePanelButtonClicked()//영웅버튼[1] : 영웅 승급 패널(interactionPanels[1,2]) 출력
    {
        if(heroMainPanel.activeSelf && !heroPanels[1].activeSelf)
        {
            heroPanels[1].SetActive(true);
            heroPanels[0].SetActive(false);
        }
    }

    private void PerformHeroLevelUp()//영웅 패널 레벨 업
    {
        Debug.Log("영웅 레벨업");
    }

    private void PerformHeroUpgrade()//영웅 패널 승급
    {
        Debug.Log("영웅 승급");
    }
}