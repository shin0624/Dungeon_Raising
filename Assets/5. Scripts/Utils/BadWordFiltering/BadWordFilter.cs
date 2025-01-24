using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheBackend;
using System.Runtime.InteropServices.WindowsRuntime;

public class BadWordFilter : MonoBehaviour
{
   //�ڳ� developer�� ��Ӿ� ���͸� sdk�� ����� ��Ӿ� ���͸� Ŭ����
    private TheBackend.ToolKit.InvalidFilter.FilterManager filterManager = new TheBackend.ToolKit.InvalidFilter.FilterManager();
    private bool isInitialized = false;//sdk �ʱ�ȭ ���� Ȯ�� �÷���

    private void Awake() 
    {
        Init();
    }
    void Start()
    {
        isInitialized = Init();
    }

    private bool Init()
    {
      // ��Ӿ� ���͸� SDK �ʱ�ȭ
        if (filterManager.LoadInvalidString())
        {
            Debug.Log("�����߽��ϴ�.");
            return true;
        }
        else
        {
            Debug.LogError("�����߽��ϴ�.");
            return false;
        }
    }

    public bool FilterFunc(string text)
    {   
        if(filterManager.IsFilteredString(text))//��Ӿ ���Ե� ��� true�� ����
        {
            Debug.Log("��Ӿ ���Ե� �г����Դϴ�.");
            return true;
        }
        else//��Ӿ� ���͸��� �ɸ��� ������ false�� ����
        {
            Debug.Log("�г��� ����� �����մϴ�.");
            return false;
        }
    }

    public bool IsInitialized()//�г��� ��ǲ ��Ʈ�ѷ����� sdk�ʱ�ȭ ���� üũ�� ���
    {
        return isInitialized;
    }
}
