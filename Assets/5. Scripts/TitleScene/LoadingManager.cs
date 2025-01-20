using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoadingManager : MonoBehaviour
{
    //�ε��ٸ� ����ϰ�, �ε��� ����� �Ϸ�Ǿ��� �� ���ϴ� �޼ҵ带 ���
    [SerializeField] private ProgressControl progress;
    [SerializeField] private Button loginButton;
    private void Awake() 
    {
        SystemSetUp();
    }
    
    private void Start() 
    {
        loginButton.gameObject.SetActive(false);// �ʱ� ������ ��� �Ϸ�� �� �α��� ��ư�� ���� �� �ֵ��� �Ѵ�.
    }

    private void SystemSetUp()//���� �ۿ��� �ʿ��� �ʱ� ����. ��Ȱ��ȭ ���¿����� ������ ��� ����ǵ���
    {
        Application.runInBackground = true;//��Ȱ��ȭ ���¿����� ������ ��� ����

        Screen.sleepTimeout = SleepTimeout.NeverSleep;//ȭ���� ������ �ʵ��� ����
        progress.Play(OnAfterProgress);//progressControl Ŭ������ �ۼ��ߴ� Play�� ȣ���Ͽ� �ε��ٸ� ����ϰ�, �ε��� �Ϸ�Ǿ��� �� OnAfterProgress ȣ��
    }

    private void OnAfterProgress()
    {
        loginButton.gameObject.SetActive(true);
    }

}
