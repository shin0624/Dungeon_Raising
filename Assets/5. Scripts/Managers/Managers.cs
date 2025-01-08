using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //���� �÷��̾� �����͸� �ν��Ͻ��� ������ �Ŵ��� ������Ʈ

    private static Managers s_instance;
    private static Managers Instance{get{Init(); return s_instance;}}//�ܺο��� ����� ���� Managers mg = Managers.Instance
    void Start()
    {
        Init();
    }

    private static void Init()
    {
        if(s_instance==null)
        {
            GameObject manager = GameObject.Find("Managers");
            if(manager==null)
            {
                manager = new GameObject{name = "Manager"};
                manager.AddComponent<Managers>();
            }
            DontDestroyOnLoad(manager);
            s_instance  = manager.GetComponent<Managers>();
        }
    }


}
