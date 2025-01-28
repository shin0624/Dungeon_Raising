using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class CharacterInfoProfile : MonoBehaviour
{
    //MainScene�� CharacterInfoPanel ���� �� CharacterProfile �̹��� ������ ���� Ŭ����. CharacterInfoPanelSetting Ŭ�������� �� Ŭ�������� ������ �޼���� Ŭ���� �ν��Ͻ��� �����Ͽ� CharacterInfoPanel�� ����� ���� �����Ѵ�.
    //playerInfoDefine�� ����� playerGender, playerJob�� �ش��ϴ� ĳ���� ������ �̹����� ã�Ƽ� �гο� �����Ѵ�. => CharacterInfoPanelSetting���� ����

    [SerializeField] private Image characterProfileImage;
    [SerializeField] private Image[] maleCharacterImage;//���� ĳ���� [0] : �˻�, [1] : �ü�
    [SerializeField] private Image[] femaleCharacterImage;//���� ĳ���� [0] : �ü�, [1] : ������
    private string playertype = "";

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private string genderCheck()// �÷��̾� ���� üũ
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

    private string jobCheck()//�÷��̾� ����üũ
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
