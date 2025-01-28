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
    private static string playerID = "";
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

    public string SetPlayerID(string id)//플레이어 아이디를 string변수에 저장
    {   string tempID = "a1b2c3";
        id = tempID;
        Debug.Log($"PlayerID : {id}");
        return id;
    }

    public void SetPlayerNickname(string text)
    {
        if(!string.IsNullOrEmpty(text))
        {
            playerInformation.playerNickname = text;//플레이어 기본정보 구조체의 닉네임을 설정. 플레이어가 인풋필드에 입력한 text값을 전달. 
        }
        else
        {
            Debug.LogWarning("Invalid nickname input.");
        }
    }

    public void SetPlayerGender(string text)//플레이어 성별을 전달하는 메서드
    {
        if(!string.IsNullOrEmpty(text))
        {
            playerInformation.playerGender = text;//플레이어 기본정보 구조체의 성별을 설정. 플레이어가 SelectWindowController에서 선택한 성별(string변수)을 전달.
        }
        else
        {
            Debug.LogWarning("Invalid gender input.");
        }
    }

    public string GetPlayerID()
    {
        return string.IsNullOrEmpty(playerID) ? "Unknown ID" : playerID;
    }

    public string GetPlayerNickname()//외부 클래스에서 플레이어 닉네임을 불러올 때 쓰는 메서드. string nickname =  PlayerInfo.Instance.GetPlayerNickname()으로 호출
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

    public string GetPlayerGender()//외부 클래스에서 플레이어 성별을 불러올 때 쓰는 메서드. string gender = PlayerInfo.Instance.GetPlayerGender()으로 호출
    {
        if (string.IsNullOrEmpty(playerInformation.playerGender))
        {
            //Debug.Log("Player gender has not been set.");
            return null;//플레이어 성별이 설정되지 않았다면 null값을 반환
        }
        else
        {
            Debug.Log($"[DEBUG] PlayerGender: {playerInformation.playerGender}");
            return playerInformation.playerGender;
        }
    } 

    public void SetPlayerJob(string text)//플레이어 직업 설정. jobselectmanager에서 사용 
    {
        if(!string.IsNullOrEmpty(playerInformation.playerJob))//먼저 저장된 값이 있다면 일단 지운다.
        {
            playerInformation.playerJob = "";
        }
        playerInformation.playerJob = text;//지정한 string 값으로 저장.
        Debug.Log($"playerJob is {playerInformation.playerJob}");
    }

    public string GetPlayerJob()//외부 클래스에서 플레이어 직업을 불러올 때 쓰는 메서드
    {
        if (string.IsNullOrEmpty(playerInformation.playerJob))
        {
            return null;//플레이어 직업이 설정되지 않았다면 null값을 반환
        }
        else
        {
            Debug.Log($"[DEBUG] Player Job: {playerInformation.playerJob}");
            return playerInformation.playerJob;
        }
    }

}
