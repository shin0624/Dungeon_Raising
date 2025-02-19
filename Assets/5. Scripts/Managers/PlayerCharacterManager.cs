using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterManager : MonoBehaviour
{

    //playerInfoDefine�� ����� playerGender, playerJob�� �ش��ϴ� ĳ���� �������� ��� �������� ������ �� �ֵ��� �÷��̾� ĳ������ ������ �����ϴ� Ŭ����. Managers�� �߰��Ѵ�.
    [SerializeField] private GameObject[] maleCharacterPrefabs;//���� ĳ���� [0] : �˻�, [1] : �ü�
    [SerializeField] private GameObject[] femaleCharacterPrefabs;//���� ĳ���� [0] : �ü�, [1] : ������

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

    public GameObject SetCharacterProfile()//ĳ���� �������� ���� �޼���. ������ ���� ���� ��������, ���� ���� ���� ������ �迭�� ����, ���� ���� ���� ���� �迭�� �ε����� �����Ѵ�.
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

        GameObject [] selectedPrefabArray = null;//������ ���� ĳ���� �������� �迭�� �����Ѵ�.
        if(gender == "Male")
        {
            selectedPrefabArray = maleCharacterPrefabs;//���� ĳ���� �迭
        }
        else if(gender == "Female")
        {
            selectedPrefabArray = femaleCharacterPrefabs;//���� ĳ���� �迭
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
                if(selectedPrefabArray==maleCharacterPrefabs) 
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

        if(selectedPrefabArray!=null && jobIndex >=0 && jobIndex < selectedPrefabArray.Length)
        {
            return selectedPrefabArray[jobIndex];//������ ������ ������ �´� ���������� �����´�.
        }
        else
        {
            Debug.LogError($"Invalid Image index or array. JobIndex is {jobIndex}");
            return null;
        }
    }

}
