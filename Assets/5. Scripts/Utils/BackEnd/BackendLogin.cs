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

    public void CustomSignUp(string id, string pw)//회원가입 --> backendManager에 함수 호출 추가
    {
        Debug.Log("회원가입을 요청합니다.");
        var bro = Backend.BMember.CustomSignUp(id, pw);
        if(bro.IsSuccess())
        {
            Debug.Log("회원가입에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("회원가입에 실패했습니다. : "+bro);
        }
    }

    public void CustomLogin(string id, string pw)//로그인 --> backendManager에 함수 호출 추가
    {
        Debug.Log("로그인을 요청합니다.");
        var bro = Backend.BMember.CustomLogin(id, pw);
        if(bro.IsSuccess())
        {
            Debug.Log("로그인에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("로그인에 실패했습니다. : " + bro);
        }
    }

    public void UpdateNickname(string nickname)//닉네임 변경
    {
        Debug.Log("닉네임 변경을 요청합니다.");
        var bro = Backend.BMember.UpdateNickname(nickname);
        if(bro.IsSuccess())
        {
            Debug.Log("닉네임 변경에 성공했습니다. : " + bro);
        }
        else
        {
            Debug.LogError("닉네임 변경에 실패했습니다. : " + bro);
        }
    }
}
