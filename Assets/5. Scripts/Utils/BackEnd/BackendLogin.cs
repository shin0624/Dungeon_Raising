using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackendLogin
{
    private static BackendLogin _instance = null;
    public static BackendLogin Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new BackendLogin();
            }
            return _instance;
        }
    }

    public void CustomSignUp(string id, string pw)//ȸ������ --> backendManager�� �Լ� ȣ�� �߰�
    {
        Debug.Log("ȸ�������� ��û�մϴ�.");
        var bro = Backend.BMember.CustomSignUp(id, pw);
        if(bro.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("ȸ�����Կ� �����߽��ϴ�. : "+bro);
        }
    }

    public void CustomLogin(string id, string pw)//�α��� --> backendManager�� �Լ� ȣ�� �߰�
    {
        Debug.Log("�α����� ��û�մϴ�.");
        var bro = Backend.BMember.CustomLogin(id, pw);
        if(bro.IsSuccess())
        {
            Debug.Log("�α��ο� �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�α��ο� �����߽��ϴ�. : " + bro);
        }
    }

    public void UpdateNickname(string nickname)//�г��� ����
    {
        Debug.Log("�г��� ������ ��û�մϴ�.");
        var bro = Backend.BMember.UpdateNickname(nickname);
        if(bro.IsSuccess())
        {
            Debug.Log("�г��� ���濡 �����߽��ϴ�. : " + bro);
        }
        else
        {
            Debug.LogError("�г��� ���濡 �����߽��ϴ�. : " + bro);
        }
    }
}