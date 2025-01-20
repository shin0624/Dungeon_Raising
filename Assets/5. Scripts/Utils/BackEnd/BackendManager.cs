using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;// �ڳ� SDK namespace �߰�

//�ڳ��� ��� ����� BackendŬ���� ���ο� �̱��� �ν��Ͻ��� ����Ǿ��ֱ� ������ �ʱ�ȭ ���Ŀ��� Backend.__()�� ���� �����Ͽ� ��� ����

public class BackendManager : MonoBehaviour {
    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        BackendSetup();
    }
    private void BackendSetup()
    {
        var bro = Backend.Initialize(); // �ڳ� �ʱ�ȭ

        // �ڳ� �ʱ�ȭ�� ���� ���䰪
        if(bro.IsSuccess()) 
        {
            Debug.Log("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 204 Success
        } else 
        {
            Debug.LogError("�ʱ�ȭ ���� : " + bro); // ������ ��� statusCode 400�� ���� �߻�
        }
        //SignUp();
    }
    private void SignUp()
    {
        //BackendLogin.Instance.CustomSignUp("user1", "1234");//ȸ������ �޼��� ȣ��
        //BackendLogin.Instance.CustomLogin("user1", "1234");
        //BackendLogin.Instance.UpdateNickname("Administrator");
        Debug.Log("ȸ������ �׽�Ʈ ����, �α��� �׽�Ʈ ����");
    }
}