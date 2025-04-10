using System.Collections;
using System.Collections.Generic;
using BackEnd.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
   //���ӿ��� ��� ��ȭ�� �÷��̾��� ������ �����ϴ� Ŭ����
    [SerializeField] private CharacterCheckManager characterCheckManager;
    private PlayerInfoDefine.PlayerInformations playerInformation = new PlayerInfoDefine.PlayerInformations();//�÷��̾� �⺻������ ����� ����ü ������ ����
    private PlayerStatusDefine.PlayerStatus playerStatus = new PlayerStatusDefine.PlayerStatus();//�÷��̾� pvp����, �������ͽ� ������ ����� ����ü ������ ����
    private static string playerID = "";
    private static PlayerInfo _instance = null;
    public static PlayerInfo Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<PlayerInfo>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("PlayerInfo");
                    _instance = go.AddComponent<PlayerInfo>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    void Awake() 
    {
        if(_instance==null)
        {
            _instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    void Start()
    {
        //PlayerIDCheck(playerID);
        Debug.Log($"[DEBUG] CharacterInfoPanelSetting Start(): {PlayerInfo.Instance.GetPlayerNickname()}");
    }
//------------------Setter---------------------------------------------------------------------------------------
    public string SetPlayerID(string id)//�÷��̾� ���̵� string������ ����
    {   string tempID = "a1b2c3";
        id = tempID;
        Debug.Log($"PlayerID : {id}");
        return id;
    }

    public void SetPlayerNickname(string text)
    {
        if(!string.IsNullOrEmpty(text))
        {
            playerInformation.playerNickname = text;//�÷��̾� �⺻���� ����ü�� �г����� ����. �÷��̾ ��ǲ�ʵ忡 �Է��� text���� ����. 
        }
        else
        {
            Debug.LogWarning("Invalid nickname input.");
        }
    }

    public void SetPlayerGender(string text)//�÷��̾� ������ �����ϴ� �޼���
    {
        if(!string.IsNullOrEmpty(text))
        {
            playerInformation.playerGender = text;//�÷��̾� �⺻���� ����ü�� ������ ����. �÷��̾ SelectWindowController���� ������ ����(string����)�� ����.
        }
        else
        {
            Debug.LogWarning("Invalid gender input.");
        }
    }

    public void SetPlayerJob(string text)//�÷��̾� ���� ����. jobselectmanager���� ���. �Ű����� text�� Knight, Archer, Magician �� �ϳ� 
    {
        if(!string.IsNullOrEmpty(playerInformation.playerJob))//���� ����� ���� �ִٸ� �ϴ� �����.
        {
            playerInformation.playerJob = "";
        }
        playerInformation.playerJob = text;//�Ѱܹ��� ���� �� �Ű����������� playerJob�� ����.
        Debug.Log($"playerJob is {playerInformation.playerJob}");
    }

    public void SetPlayerRace(string text)//�÷��̾� ���� ����. RaceSelectManager���� ȣ���Ͽ� PlayerInfoDefine�� playerRace���� �����Ѵ�.
    {
        if(!string.IsNullOrEmpty(playerInformation.playerRace))// ���� ������ ���� ���� ��� �ʱ�ȭ
        {
            playerInformation.playerRace = "";
        }
        playerInformation.playerRace = text;//�Ѱܹ��� ���� �� �Ű����� ������ playerRace�� ����
        Debug.Log($"playerRace is {playerInformation.playerRace}");
    }

    public void SetPlayerGold(int gold)//�÷��̾� ��ȭ�� ����.
    {
        playerInformation.playerGold = gold;
    }

    private int SetCurrentFloor()//�÷��̾ �ش� ���� ��� ������ Ŭ�������� ��, ���� ���� ���� ������ �����ϴ� �޼���.
    {
        int nextFloor = playerInformation.towerFloor+=1;//�÷��̾ Ŭ������ �� ���� 1 ������Ų��.
        Debug.Log("next Floor : "+ nextFloor);//����� �α׷� �� ���� ����Ѵ�.
        return nextFloor;// playerInformation�� PlayerInfo�� ��������̰�, �� ���� towerFloor�� ����ü ���� �ʵ��̱⿡, �� ���� ����.
    }

    public void SetPlayerFloor()//��������� ����� ���� ���� ����ü �������� �ִ´�.
    {
        playerInformation.towerFloor = SetCurrentFloor();
    }

//--------------Getter------------------------------------------------------------------------------------
    public string GetPlayerID()
    {
        return string.IsNullOrEmpty(playerID) ? "Unknown ID" : playerID;
    }

    public string GetPlayerNickname()//�ܺ� Ŭ�������� �÷��̾� �г����� �ҷ��� �� ���� �޼���. string nickname =  PlayerInfo.Instance.GetPlayerNickname()���� ȣ��
    {
        if (string.IsNullOrEmpty(playerInformation.playerNickname))
        {
            Debug.LogWarning("Player nickname is empty or has not been set.");
            return "Unknown Player";
        }
        else
        {
            return playerInformation.playerNickname;
        }
    }

    public string GetPlayerGender()//�ܺ� Ŭ�������� �÷��̾� ������ �ҷ��� �� ���� �޼���. string gender = PlayerInfo.Instance.GetPlayerGender()���� ȣ��
    {
        if (string.IsNullOrEmpty(playerInformation.playerGender))
        {
            //Debug.Log("Player gender has not been set.");
            return null;//�÷��̾� ������ �������� �ʾҴٸ� null���� ��ȯ
        }
        else
        {
            Debug.Log($"[DEBUG] PlayerGender: {playerInformation.playerGender}");
            return playerInformation.playerGender;//������ ������ �����̸� ��ȯ
        }
    } 

    public string GetPlayerJob()//�ܺ� Ŭ�������� �÷��̾� ������ �ҷ��� �� ���� �޼���
    {
        if (string.IsNullOrEmpty(playerInformation.playerJob))
        {
            return null;//�÷��̾� ������ �������� �ʾҴٸ� null���� ��ȯ
        }
        else
        {
            Debug.Log($"[DEBUG] Player Job: {playerInformation.playerJob}");
            return playerInformation.playerJob;//������ ������ �����̸� ��ȯ
        }
    }

    public string GetPlayerRace()//�ܺ� Ŭ�������� �÷��̾� ������ �ҷ��� �� ���� �޼���
    {
        if(string.IsNullOrEmpty(playerInformation.playerRace))
        {
            return null;//�÷��̾� ������ �������� �ʾҴٸ� null�� ��ȯ
        }
        else
        {
            Debug.Log($"[DEBUG]  Player Race : {playerInformation.playerRace}");
            return playerInformation.playerRace;//������ ������ �����̸� ��ȯ
        }
    }

    public int GetplayerGold()//�ܺ� Ŭ�������� �÷��̾��� ��ȭ���� �ҷ��� �� ���� �޼���.
    {
            return playerInformation.playerGold;//��ȭ�� �����ϸ� ��ȯ
    }

    public int GetPlayerFloor()
    {
        if(playerInformation.towerFloor == 0)//�÷��̾� �� ������ �������� �ʾƼ� 0�� ���, 1�� ���� �� 1�� ��ȯ.
        {
            SetPlayerFloor();
        }
        return playerInformation.towerFloor;//�÷��̾� �� ������ �����Ǿ� �ִٸ� �ش� �� ������ ��ȯ
    }

}
