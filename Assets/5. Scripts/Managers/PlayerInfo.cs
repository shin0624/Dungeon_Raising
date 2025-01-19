using System.Collections;
using System.Collections.Generic;
using BackEnd.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
   //게임에서 계속 변화될 플레이어의 정보를 저장하는 클래스
   
    [SerializeField] private CharacterCheckManager characterCheckManager;
    private PlayerInfoDefine playerInfoDefine;
    private PlayerStatusDefine playerStatusDefine;
    private string playerID = "";
    
    void Start()
    {
        PlayerIDCheck(playerID);
    }

    void Update()
    {
        
    }

    private string PlayerIDCheck(string id)//플레이어 아이디를 string변수에 저장
    {
        id = characterCheckManager.playerID;
        Debug.Log($"PlayerID : {playerID}");
        return id;
    }
}
