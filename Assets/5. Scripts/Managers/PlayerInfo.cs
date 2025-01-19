using System.Collections;
using System.Collections.Generic;
using BackEnd.Quobject.SocketIoClientDotNet.Client;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
   //���ӿ��� ��� ��ȭ�� �÷��̾��� ������ �����ϴ� Ŭ����
   
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

    private string PlayerIDCheck(string id)//�÷��̾� ���̵� string������ ����
    {
        id = characterCheckManager.playerID;
        Debug.Log($"PlayerID : {playerID}");
        return id;
    }
}
