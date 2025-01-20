using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoadingManager : MonoBehaviour
{
    //로딩바를 재생하고, 로딩바 재생이 완료되었을 때 원하는 메소드를 출력
    [SerializeField] private ProgressControl progress;
    [SerializeField] private Button loginButton;
    private void Awake() 
    {
        SystemSetUp();
    }
    
    private void Start() 
    {
        loginButton.gameObject.SetActive(false);// 초기 셋팅이 모두 완료된 후 로그인 버튼이 나올 수 있도록 한다.
    }

    private void SystemSetUp()//현재 앱에서 필요한 초기 설정. 비활성화 상태에서도 게임이 계속 진행되도록
    {
        Application.runInBackground = true;//비활성화 상태에서도 게임이 계속 진행

        Screen.sleepTimeout = SleepTimeout.NeverSleep;//화면이 꺼지지 않도록 설정
        progress.Play(OnAfterProgress);//progressControl 클래스에 작성했던 Play를 호출하여 로딩바를 재생하고, 로딩이 완료되었을 때 OnAfterProgress 호출
    }

    private void OnAfterProgress()
    {
        loginButton.gameObject.SetActive(true);
    }

}
