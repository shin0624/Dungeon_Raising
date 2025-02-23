using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheBackend;
using System.Runtime.InteropServices.WindowsRuntime;

public class BadWordFilter : MonoBehaviour
{
   //뒤끝 developer의 비속어 필터링 sdk를 사용한 비속어 필터링 클래스
    private TheBackend.ToolKit.InvalidFilter.FilterManager filterManager = new TheBackend.ToolKit.InvalidFilter.FilterManager();
    private bool isInitialized = false;//sdk 초기화 상태 확인 플래그

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
      // 비속어 필터링 SDK 초기화
        if (filterManager.LoadInvalidString())
        {
            Debug.Log("성공했습니다.");
            return true;
        }
        else
        {
            Debug.LogError("실패했습니다.");
            return false;
        }
    }

    public bool FilterFunc(string text)
    {   
        if(filterManager.IsFilteredString(text))//비속어가 포함된 경우 true를 전달
        {
            Debug.Log("비속어가 포함된 닉네임입니다.");
            return true;
        }
        else//비속어 필터링에 걸리지 않으면 false를 전달
        {
            Debug.Log("닉네임 사용이 가능합니다.");
            return false;
        }
    }

    public bool IsInitialized()//닉네임 인풋 컨트롤러에서 sdk초기화 여부 체크에 사용
    {
        return isInitialized;
    }
}
