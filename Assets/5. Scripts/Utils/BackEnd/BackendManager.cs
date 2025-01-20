using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;// 뒤끝 SDK namespace 추가

//뒤끝의 모든 기능은 Backend클래스 내부에 싱글톤 인스턴스로 선언되어있기 때문에 초기화 이후에는 Backend.__()와 같이 접근하여 사용 가능

public class BackendManager : MonoBehaviour {
    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        BackendSetup();
    }
    private void BackendSetup()
    {
        var bro = Backend.Initialize(); // 뒤끝 초기화

        // 뒤끝 초기화에 대한 응답값
        if(bro.IsSuccess()) 
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        } else 
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        }
        //SignUp();
    }
    private void SignUp()
    {
        //BackendLogin.Instance.CustomSignUp("user1", "1234");//회원가입 메서드 호출
        //BackendLogin.Instance.CustomLogin("user1", "1234");
        //BackendLogin.Instance.UpdateNickname("Administrator");
        Debug.Log("회원가입 테스트 종료, 로그인 테스트 종료");
    }
}