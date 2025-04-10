using System.Collections;
using System.Collections.Generic;
using BackEnd.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
   //게임에서 계속 변화될 플레이어의 정보를 저장하는 클래스
    [SerializeField] private CharacterCheckManager characterCheckManager;
    private PlayerInfoDefine.PlayerInformations playerInformation = new PlayerInfoDefine.PlayerInformations();//플레이어 기본정보가 저장된 구조체 변수를 선언
    private PlayerStatusDefine.PlayerStatus playerStatus = new PlayerStatusDefine.PlayerStatus();//플레이어 pvp정보, 스테이터스 정보가 저장된 구조체 변수를 선언
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

    public void SetPlayerJob(string text)//플레이어 직업 설정. jobselectmanager에서 사용. 매개변수 text는 Knight, Archer, Magician 중 하나 
    {
        if(!string.IsNullOrEmpty(playerInformation.playerJob))//먼저 저장된 값이 있다면 일단 지운다.
        {
            playerInformation.playerJob = "";
        }
        playerInformation.playerJob = text;//넘겨받은 직업 명 매개변수값으로 playerJob을 설정.
        Debug.Log($"playerJob is {playerInformation.playerJob}");
    }

    public void SetPlayerRace(string text)//플레이어 종족 설정. RaceSelectManager에서 호출하여 PlayerInfoDefine의 playerRace값을 설정한다.
    {
        if(!string.IsNullOrEmpty(playerInformation.playerRace))// 먼저 설정된 값이 있을 경우 초기화
        {
            playerInformation.playerRace = "";
        }
        playerInformation.playerRace = text;//넘겨받은 종족 명 매개변수 값으로 playerRace를 설정
        Debug.Log($"playerRace is {playerInformation.playerRace}");
    }

    public void SetPlayerGold(int gold)//플레이어 재화량 설정.
    {
        playerInformation.playerGold = gold;
    }

    private int SetCurrentFloor()//플레이어가 해당 층의 모든 던전을 클리어했을 때, 현재 층을 다음 층으로 변경하는 메서드.
    {
        int nextFloor = playerInformation.towerFloor+=1;//플레이어가 클리어한 층 수를 1 증가시킨다.
        Debug.Log("next Floor : "+ nextFloor);//디버그 로그로 층 수를 출력한다.
        return nextFloor;// playerInformation이 PlayerInfo의 멤버변수이고, 그 안의 towerFloor는 구조체 내의 필드이기에, 값 변경 가능.
    }

    public void SetPlayerFloor()//명시적으로 변경된 값을 실제 구조체 변수값에 넣는다.
    {
        playerInformation.towerFloor = SetCurrentFloor();
    }

//--------------Getter------------------------------------------------------------------------------------
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
            return playerInformation.playerGender;//성별이 설정된 상태이면 반환
        }
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
            return playerInformation.playerJob;//직업이 설정된 상태이면 반환
        }
    }

    public string GetPlayerRace()//외부 클래스에서 플레이어 종족을 불러올 때 쓰는 메서드
    {
        if(string.IsNullOrEmpty(playerInformation.playerRace))
        {
            return null;//플레이어 종족이 설정되지 않았다면 null을 반환
        }
        else
        {
            Debug.Log($"[DEBUG]  Player Race : {playerInformation.playerRace}");
            return playerInformation.playerRace;//종족이 설정된 상태이면 반환
        }
    }

    public int GetplayerGold()//외부 클래스에서 플레이어의 재화량을 불러올 때 쓰는 메서드.
    {
            return playerInformation.playerGold;//재화가 존재하면 반환
    }

    public int GetPlayerFloor()
    {
        if(playerInformation.towerFloor == 0)//플레이어 층 정보가 설정되지 않아서 0인 경우, 1로 설정 후 1을 반환.
        {
            SetPlayerFloor();
        }
        return playerInformation.towerFloor;//플레이어 층 정보가 설정되어 있다면 해당 층 정보를 반환
    }

}
