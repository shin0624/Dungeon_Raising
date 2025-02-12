using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithPlayerImageSetting : MonoBehaviour
{
    //���尣 UI�� CharacterImagePanel�� �÷��̾� ������ ����ȭ�ϴ� Ŭ����.
    [SerializeField] private Image characterImageParent;
    [SerializeField] private CharacterInfoProfile characterInfoProfile;

    private void OnEnable()
    {
        SetPlayerSpriteImageInDetailPanel();
    }

    private void SetPlayerSpriteImageInDetailPanel()//�÷��̾��� ��������Ʈ �̹����� CharacterInfoProfile�� SetCharacterProfile()�� ���ϰ��� sprite��ü�� ����ȭ.
    {
        if(characterImageParent!=null)
        {
           characterImageParent.sprite = characterInfoProfile.SetPlayerImageInBlackSmith();
        }
    }

    
}
