using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //���� �÷��̾� �����͸� �ν��Ͻ��� ������ �Ŵ��� ������Ʈ

    private static Managers s_instance;
    public static Managers Instance{get{Init(); return s_instance;}}//�ܺο��� ����� ���� Managers mg = Managers.Instance

    private void Awake() 
    {
        Init();
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
        }
    }


}
