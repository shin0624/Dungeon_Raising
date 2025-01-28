using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoPanelSetting : MonoBehaviour
{
    // MainScene의 CharacterInfoPanel 세팅을 위한 클래스. 플레이어창의 닉네임, 레벨, 클리어 층 수, 전투력, 경험치, 이미지를 현재 플레이어에 맞게 세팅한다.
    [SerializeField] private TextMeshProUGUI characterName; 
    [SerializeField] private Image characterImage;//캐릭터인포패널에서 캐릭터 이미지가 들어갈 빈 스프라이트 객체를 할당.
    [SerializeField] private CharacterInfoProfile characterInfoProfile;
    private const int MaxCharacterLimit = 10;
    private const float FontSizeLarge = 34.7f;
    private const float FontSizeSmall = 30.0f;

    void Awake()
    {
        MainSceneInit();
    }

    private void MainSceneInit()
    {
        StartCoroutine(DelaySetCharacterName());
        characterImage.sprite = characterInfoProfile.SetCharacterProfile();//CharacterInfoProfile의 참조변수로 스프라이트 설정 메서드에 접근한다. 싱글톤으로 접근 시 인스펙터에서 스프라이트 배열 할당이 불가함.
    }
    
    private void SetCharacterName()//플레이어 명 = 플레이어 입력 값으로 설정하는 메서드
    {
        if(PlayerInfo.Instance==null)//만약 PlayerInfo의 닉네임이 정상적으로 설정되지 않았을 때는 임시값을 저장
        {
            Debug.LogError("PlayerInfo is not Initialized.");
            characterName.text = "Unknown Player";
            return;
        }
        characterName.text =PlayerInfo.Instance.GetPlayerNickname();//MainScene의 플레이어 명 = 캐릭터 선택 씬에서 플레이어가 입력했던 닉네임으로 설정
    }

    private IEnumerator DelaySetCharacterName()//닉네임 세팅을 일반 메서드로 작성 시, 씬 전환 시 PlayerInfo의 데이터가 설정되기 이전에 UI가 업데이트되어, 닉네임에 빈 값이 전달되는 경우를 확인함. 이를 예방하기 위해 코루틴을 사용하여 씬 로딩 후 PlayerInfo 데이터가 완전히 설정될 때 까지 기다린 후 UI를 갱신하도록 함.
    {
        yield return new WaitForSeconds(0.5f);
        SetCharacterName();
    }





}
