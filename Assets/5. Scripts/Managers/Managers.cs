using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //개별 플레이어 데이터를 인스턴스로 선언할 매니저 컴포넌트

    private static Managers s_instance;
    public static Managers Instance{get{Init(); return s_instance;}}//외부에서 사용할 때는 Managers mg = Managers.Instance

    private void Awake() 
    {
        Init();
        //InitPlayerInfo();
        Debug.Log("Managers Object Initialized.");
    }

    private static void Init()
    {
        if(s_instance==null)
        {
            GameObject manager = GameObject.Find("Managers");
            if(manager==null)
            {
                manager = new GameObject{name = "Managers"};
                manager.AddComponent<Managers>();
            }
            DontDestroyOnLoad(manager);
            s_instance  = manager.GetComponent<Managers>();

            if(manager.GetComponent<PlayerInfo>()==null)
            {
                manager.AddComponent<PlayerInfo>();
            }
            if(manager.GetComponent<PlayerCharacterManager>()==null)
            {
                manager.AddComponent<PlayerCharacterManager>();
            }
        }
        DOTWeenUIAnimation.Init();// DOTWeen의 초기화는 싱글톤 기반이기에, 게임 시작 시 한 번만 초기화 해주면 게임 종료 시 까지 초기화가 유지됨.
        
    }

    private void InitPlayerInfo()
    {
        PlayerInfo.Instance.SetPlayerGold(1000);
        Debug.Log($"현재 플레이어 소지 재화 : {PlayerInfo.Instance.GetplayerGold()}");
    }


}
