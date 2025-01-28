using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class CharacterInfoProfile : MonoBehaviour
{
    //MainScene의 CharacterInfoPanel 세팅 중 CharacterProfile 이미지 세팅을 위한 클래스. CharacterInfoPanelSetting 클래스에서 본 클래스에서 정의한 메서드와 클래스 인스턴스를 참조하여 CharacterInfoPanel에 적용될 값을 세팅한다.
    //playerInfoDefine에 저장된 playerGender, playerJob에 해당하는 캐릭터 프로필 이미지를 찾아서 패널에 적용한다. => CharacterInfoPanelSetting에서 수행

    [SerializeField] private Image characterProfileImage;
    [SerializeField] private Image[] maleCharacterImage;//남자 캐릭터 [0] : 검사, [1] : 궁수
    [SerializeField] private Image[] femaleCharacterImage;//여자 캐릭터 [0] : 궁수, [1] : 마법사
    private string playertype = "";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private string genderCheck()// 플레이어 성별 체크
    {
        string gender="";
        if(PlayerInfo.Instance!=null)
        {
            gender = PlayerInfo.Instance.GetPlayerGender();
        }
        return gender;
    }

    public void SetCharacterProfile(Image image)
    {
        if(PlayerInfo.Instance==null)
        {
            Debug.LogError("PlayerInfo is not Initialized.");
            image = null;
            return;
        }
        else
        {

        }
        
    }

    private string jobCheck()//플레이어 직업체크
    {
        string job = "";
        if(PlayerInfo.Instance!=null)
        {
            job = PlayerInfo.Instance.GetPlayerJob();
        }
        return job;
    }

    private IEnumerator DelaySetCharacterProfile()
    {
        yield return new WaitForSeconds(0.5f);
        SetCharacterProfile(characterProfileImage);
    }
}
