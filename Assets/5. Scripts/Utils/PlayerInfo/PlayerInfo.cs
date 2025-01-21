using System.Collections;
using System.Collections.Generic;
using BackEnd.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
   //게임에서 계속 변화될 플레이어의 정보를 저장하는 클래스
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
    private PlayerInfoDefine.PlayerInformations playerInformation = new PlayerInfoDefine.PlayerInformations();//플레이어 기본정보가 저장된 구조체 변수를 선언
    private PlayerStatusDefine.PlayerStatus playerStatus = new PlayerStatusDefine.PlayerStatus();//플레이어 pvp정보, 스테이터스 정보가 저장된 구조체 변수를 선언
    private string playerID = "";
    
    void Start()
    {
        //PlayerIDCheck(playerID);
        Debug.Log($"[DEBUG] CharacterInfoPanelSetting Start(): {PlayerInfo.Instance.GetPlayerNickname()}");
    }

    void Update()
    {
        
    }

    public string PlayerIDCheck(string id)//플레이어 아이디를 string변수에 저장
    {   string tempID = "a1b2c3";
        id = tempID;
        Debug.Log($"PlayerID : {id}");
        return id;
    }

    public void PlayerNicknameCheck(string text)
    {
        Debug.Log($"[DEBUG] Nickname submitted: {text}");
        playerInformation.playerNickname = text;//플레이어 기본정보 구조체의 닉네임을 설정. 플레이어가 인풋필드에 입력한 text값을 전달.
    }

    public string GetPlayerNickname()//외부 클래스에서 플레이어 닉네임을 불러올 때 쓰는 메서드. string nickname =  PlayerInfo.Instance.GetPlayerNickname()으로 호출
    {
        if (string.IsNullOrEmpty(playerInformation.playerNickname))
        {
            Debug.LogWarning("Player nickname is empty or has not been set.");
            return "Unknown Player";
        }
        return playerInformation.playerNickname;
    }

   
}
