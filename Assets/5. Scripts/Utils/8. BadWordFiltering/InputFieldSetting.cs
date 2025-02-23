using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldSetting : MonoBehaviour
{
    //닉네임 입력창 셋팅 - 한글, 영문, 숫자 포함 최대 10글자이며, onValueChanged이벤트를 활용하여 입력된 문자 수에 따라 글자 크기와 캐릭터 리미트를 동적으로 조절하도록 함.
    [SerializeField] TextMeshProUGUI characterNameText;//입력필드에서의 텍스트가 반영될 사용자 창의 닉네임 텍스트
    [SerializeField] private TMP_InputField inputNickname;//닉네임을 입력받을 입력필드
    private const int MaxCharacterLimit = 10;
    private const float FontSizeLarge = 34.7f;
    private const float FontSizeSmall = 30.0f;
    void Start()
    {
        inputNickname.characterLimit = MaxCharacterLimit;//문자수 제한 : 10
        inputNickname.onValueChanged.AddListener(UpdateFontSizeAndText);
    }

    
    public void UpdateFontSizeAndText(string text)//문자 수에 따라 글자 크기, 제한을 동적으로 조절한다.
    {
        int length = text.Length;
        if(length <=8)//문자 수가 8글자 이내이면 표시되는 크기를 크게
        {
            characterNameText.fontSize = FontSizeLarge;
        }
        else if(length<=MaxCharacterLimit)//글자 수가 10글자까지 가면 표시되는 크기를 크게
        {
            characterNameText.fontSize = FontSizeSmall;
        }

        characterNameText.text = text;//최종 반영하여 넘긴다.
    }
}
