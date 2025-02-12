using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoProfile : MonoBehaviour
{
    //MainScene�� CharacterInfoPanel ���� �� CharacterProfile ��������Ʈ ������ ���� Ŭ����. CharacterInfoPanelSetting Ŭ�������� �� Ŭ�������� ������ �޼���� Ŭ���� �ν��Ͻ��� �����Ͽ� CharacterInfoPanel�� ����� ���� �����Ѵ�.
    //playerInfoDefine�� ����� playerGender, playerJob�� �ش��ϴ� ĳ���� ������ ��������Ʈ�� ã�Ƽ� �гο� �����Ѵ�. => CharacterInfoPanelSetting���� ����
    [SerializeField] private Sprite[] maleCharacterImage;//���� ĳ���� [0] : �˻�, [1] : �ü�
    [SerializeField] private Sprite[] femaleCharacterImage;//���� ĳ���� [0] : �ü�, [1] : ������

    //�Ʒ� �迭�� ���� �̹���
    [SerializeField] private Sprite[] maleCharacterFullAspectImage;// [0] : �˻�, [1] ; �ü� 
    [SerializeField] private Sprite[] femaleCharacterFullAspectImage;// [0] : �ü�, [1] : ������ 

    private string genderCheck()// �÷��̾� ���� üũ
    {
        string gender="";
        if(PlayerInfo.Instance!=null)//�ν��Ͻ��� �����ϸ�
        {
            gender = PlayerInfo.Instance.GetPlayerGender();//�÷��̾� ���� ���� �����ͼ� ����
        }
        return gender;
    }

    private string jobCheck()//�÷��̾� ����üũ
    {
        string job = "";
        if(PlayerInfo.Instance!=null)//�ν��Ͻ��� �����ϸ�
        {
            job = PlayerInfo.Instance.GetPlayerJob();//�÷��̾� ���� ���� �����ͼ� ����
        }
        return job;
    }

    public Sprite SetCharacterProfile()//ĳ���� ������ ��������Ʈ ���� �޼���. ������ ���� ���� �������� �� ���� ���� ���� ��������Ʈ�� �迭���� �������� �� ���� ���� ���� �ش� �迭�� ��������Ʈ�� �����´�.
    {
        if(PlayerInfo.Instance==null)
        {
            Debug.LogError("PlayerInfo is not Initialized.");
            return null;
        }

        string gender = genderCheck();//������ �����ξ��� üũ �޼������ ���ϰ��� ������ ���� ����
        string job = jobCheck();

        if(string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(job))//�� ������ ��ȿ�� üũ ���� ����
        {
            Debug.Log($"Gender or Job is invalid. Gender : {gender}, Job : {job}");
        }

        Sprite [] selectedImageArray = null;//������ ���� ĳ���� ��������Ʈ �迭�� �����Ѵ�.
        if(gender == "Male")
        {
            selectedImageArray = maleCharacterImage;//���� ĳ���� �迭
        }
        else if(gender == "Female")
        {
            selectedImageArray = femaleCharacterImage;//���� ĳ���� �迭
        }
        else//����ó��
        {
            Debug.LogError($"Unknown Gender. Gender : {gender}");
        }

        int jobIndex = -1;
        switch(job)
        {
            case "Knight" :
                jobIndex = 0;//knight�� ���� ĳ���� �迭�� �ε��� 0���̹Ƿ� ��ĥ �� ���� ���ڿ���.
                break;

            case "Archer" :
                if(selectedImageArray==maleCharacterImage) 
                {
                    jobIndex = 1;//���õ� �迭�� ���� �迭�� ��� �ε����� 1��
                }
                else 
                {
                    jobIndex = 0;//���õ� �迭�� ���� �迭�� ��� �ε����� 0��
                }
                break;

            case "Magician" :
                jobIndex = 1;//Magician�� ���� ĳ���� �迭�� �ε��� 1���̹Ƿ�, ��ĥ �� ���� ���ڿ���. 
                break;
                
            default :
                Debug.LogError($"Unknown Job. Job is {job}");
                return null;
        }

        if(selectedImageArray!=null && jobIndex >=0 && jobIndex < selectedImageArray.Length)
        {
            return selectedImageArray[jobIndex];//������ ������ ������ �´� ��������Ʈ�� �����´�.
        }
        else
        {
            Debug.LogError($"Invalid Image index or array. JobIndex is {jobIndex}");
            return null;
        }
    }

    public Sprite SetPlayerImageInBlackSmith()//���尣 ui�� ĳ���� �̹����� �÷��̾� ĳ���ͷ� ���̱� ���� �޼���. 
    {
         if(PlayerInfo.Instance==null)
        {
            Debug.LogError("PlayerInfo is not Initialized.");
            return null;
        }

        string gender = genderCheck();//������ �����ξ��� üũ �޼������ ���ϰ��� ������ ���� ����
        string job = jobCheck();
        

        if(string.IsNullOrEmpty(gender) || string.IsNullOrEmpty(job))//�� ������ ��ȿ�� üũ ���� ����
        {
            Debug.Log($"Gender or Job is invalid. Gender : {gender}, Job : {job}");
        }

        Sprite [] selectedImageArray = null;//������ ���� ĳ���� ������ �迭�� �����Ѵ�.
        if(gender == "Male")
        {
            selectedImageArray = maleCharacterFullAspectImage;//���� ĳ���� �迭
        }
        else if(gender == "Female")
        {
            selectedImageArray = femaleCharacterFullAspectImage;//���� ĳ���� �迭
        }
        else//����ó��
        {
            Debug.LogError($"Unknown Gender. Gender : {gender}");
        }

        int jobIndex = -1;
        switch(job)
        {
            case "Knight" :
                jobIndex = 0;//knight�� ���� ĳ���� �迭�� �ε��� 0���̹Ƿ� ��ĥ �� ���� ���ڿ���.
                break;

            case "Archer" :
                if(selectedImageArray==maleCharacterFullAspectImage) 
                {
                    jobIndex = 1;//���õ� �迭�� ���� �迭�� ��� �ε����� 1��
                }
                else 
                {
                    jobIndex = 0;//���õ� �迭�� ���� �迭�� ��� �ε����� 0��
                }
                break;

            case "Magician" :
                jobIndex = 1;//Magician�� ���� ĳ���� �迭�� �ε��� 1���̹Ƿ�, ��ĥ �� ���� ���ڿ���. 
                break;
                
            default :
                Debug.LogError($"Unknown Job. Job is {job}");
                return null;
        }

        if(selectedImageArray!=null && jobIndex >=0 && jobIndex < selectedImageArray.Length)
        {
            return selectedImageArray[jobIndex];//������ ������ ������ �´� �����ո� �����´�.
        }
        else
        {
            Debug.LogError($"Invalid Image index or array. JobIndex is {jobIndex}");
            return null;
        }
    }



}
