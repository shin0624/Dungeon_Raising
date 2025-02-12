using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithPlayerImageSetting : MonoBehaviour
{
    //대장간 UI의 CharacterImagePanel을 플레이어 정보와 동기화하는 클래스.
    [SerializeField] private Image characterImageParent;
    [SerializeField] private CharacterInfoProfile characterInfoProfile;

    private void OnEnable()
    {
        SetPlayerSpriteImageInDetailPanel();
    }

    private void SetPlayerSpriteImageInDetailPanel()//플레이어의 스프라이트 이미지를 CharacterInfoProfile의 SetCharacterProfile()의 리턴값인 sprite객체로 동기화.
    {
        if(characterImageParent!=null)
        {
           characterImageParent.sprite = characterInfoProfile.SetPlayerImageInBlackSmith();
        }
    }

    
}
