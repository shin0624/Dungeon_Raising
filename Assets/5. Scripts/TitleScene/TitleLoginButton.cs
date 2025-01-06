using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class TitleLoginButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button loginButton;//로그인버튼의 버튼 객체
    [SerializeField] private Image loginButtonImage;//로그인 버튼의 이미지 객체
    [SerializeField] private Sprite loginButtonClickedSprite;//로그인버튼 클릭 시 변경될 스프라이트 객체
    [SerializeField] private Sprite loginButtonHoveredSprite;//로그인 버튼 호버링 시 변경 될 스프라이트 객체
    [SerializeField] private Sprite loginButtonDefaultSprite;//로그인 버튼의 기본 스프라이트
    void Start()
    {
        loginButtonImage = GetComponent<Image>();//로그인버튼의 이미지를 가져온다.
        loginButtonImage.sprite = loginButtonDefaultSprite;//처음엔 기본 스프라이트로 설정.
        loginButton.onClick.AddListener(LoginButtonClicked);//버튼 객체에 클릭 시 이벤트 리스너를 등록한다.       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        loginButtonImage.sprite = loginButtonHoveredSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        loginButtonImage.sprite = loginButtonDefaultSprite;
    }

    void LoginButtonClicked()
    {
        loginButtonImage.sprite = loginButtonClickedSprite;//로그인 버튼 클릭 시 지정된 스프라이트로 교체
        LoadCharacterSelectScene();//로그인 버튼 클릭 시 캐릭터 선택 씬으로 이동
    }

    void LoadCharacterSelectScene()
    {
        SceneManager.LoadScene("CharacterSelectScene");
    }
}
