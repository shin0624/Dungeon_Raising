using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoDetailSetting : MonoBehaviour
{
    //CharacterInfoPanel�� Ŭ���ϸ� ������ CharacterInfoDetail�� ������ ��ũ��Ʈ. �г���, pvp���, �ܿ� ����, ������ ���� playerInfo�� �����Ϳ� ����ȭ��Ų��.
    [SerializeField] private TextMeshProUGUI playerNickname;
    [SerializeField] private Image playerSpriteImage;
    [SerializeField] private CharacterInfoProfile characterInfoProfile;
 
    void Start()
    {
        Init();
    }

    private void Init()//���� pvp���, �ܿ� ����, ������ ���� �����Ǹ� �̰��� �Լ� ����
    {
        SetPlayerNicnameInDetailPanel();
        SetPlayerSpriteImageInDetailPanel();
    }

    private void SetPlayerNicnameInDetailPanel()//������ �г��� �г����� playerInfo�� ��ϵ� �г������� ����ȭ.
    {
        if(playerNickname!=null)
        {
            playerNickname.text = PlayerInfo.Instance.GetPlayerNickname();
        }
    }

    private void SetPlayerSpriteImageInDetailPanel()//�г��� ���� ǥ�õ� �÷��̾��� ��������Ʈ �̹����� CharacterInfoProfile�� SetCharacterProfile()�� ���ϰ��� sprite��ü�� ����ȭ.
    {
        if(playerSpriteImage!=null)
        {
            playerSpriteImage.sprite = characterInfoProfile.SetCharacterProfile();
        }
    }
}
