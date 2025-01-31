using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoDetailSetting : MonoBehaviour
{
    //CharacterInfoPanel을 클릭하면 열리는 CharacterInfoDetail에 적용할 스크립트. 닉네임, pvp기록, 잔여 스탯, 전투력 등을 playerInfo의 데이터와 동기화시킨다.
    [SerializeField] private TextMeshProUGUI playerNickname;
    [SerializeField] private Image playerSpriteImage;
    [SerializeField] private CharacterInfoProfile characterInfoProfile;
 
    void Start()
    {
        Init();
    }

    private void Init()//추후 pvp기록, 잔여 스탯, 전투력 등이 설정되면 이곳에 함수 선언
    {
        SetPlayerNicnameInDetailPanel();
        SetPlayerSpriteImageInDetailPanel();
    }

    private void SetPlayerNicnameInDetailPanel()//디테일 패널의 닉네임을 playerInfo에 등록된 닉네임으로 동기화.
    {
        if(playerNickname!=null)
        {
            playerNickname.text = PlayerInfo.Instance.GetPlayerNickname();
        }
    }

    private void SetPlayerSpriteImageInDetailPanel()//닉네임 위에 표시될 플레이어의 스프라이트 이미지를 CharacterInfoProfile의 SetCharacterProfile()의 리턴값인 sprite객체로 동기화.
    {
        if(playerSpriteImage!=null)
        {
            playerSpriteImage.sprite = characterInfoProfile.SetCharacterProfile();
        }
    }
}
