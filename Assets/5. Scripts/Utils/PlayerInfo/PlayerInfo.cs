using System.Collections;
using System.Collections.Generic;
using BackEnd.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
   //���ӿ��� ��� ��ȭ�� �÷��̾��� ������ �����ϴ� Ŭ����
    [SerializeField] private CharacterCheckManager characterCheckManager;
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
    private PlayerInfoDefine.PlayerInformations playerInformation = new PlayerInfoDefine.PlayerInformations();//�÷��̾� �⺻������ ����� ����ü ������ ����
    private PlayerStatusDefine.PlayerStatus playerStatus = new PlayerStatusDefine.PlayerStatus();//�÷��̾� pvp����, �������ͽ� ������ ����� ����ü ������ ����
    private string playerID = "";
    
    void Start()
    {
        //PlayerIDCheck(playerID);
        Debug.Log($"[DEBUG] CharacterInfoPanelSetting Start(): {PlayerInfo.Instance.GetPlayerNickname()}");
    }

    void Update()
    {
        
    }

    public string PlayerIDCheck(string id)//�÷��̾� ���̵� string������ ����
    {   string tempID = "a1b2c3";
        id = tempID;
        Debug.Log($"PlayerID : {id}");
        return id;
    }

    public void PlayerNicknameCheck(string text)
    {
        Debug.Log($"[DEBUG] Nickname submitted: {text}");
        playerInformation.playerNickname = text;//�÷��̾� �⺻���� ����ü�� �г����� ����. �÷��̾ ��ǲ�ʵ忡 �Է��� text���� ����.
    }

    public string GetPlayerNickname()//�ܺ� Ŭ�������� �÷��̾� �г����� �ҷ��� �� ���� �޼���. string nickname =  PlayerInfo.Instance.GetPlayerNickname()���� ȣ��
    {
        if (string.IsNullOrEmpty(playerInformation.playerNickname))
        {
            Debug.LogWarning("Player nickname is empty or has not been set.");
            return "Unknown Player";
        }
        return playerInformation.playerNickname;
    }

   
}
