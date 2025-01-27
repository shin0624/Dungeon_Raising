using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenderButtonClickedEvent : MonoBehaviour
{
    [SerializeField] private Button maleCharacterButton;
    [SerializeField] private Button femaleCharacterButton;
    [SerializeField] private Sprite maleCharacterButtonClicked;
    [SerializeField] private Sprite femaleCharacterButtonClicked;
    [SerializeField] private Sprite maleCharacterButtonNonClicked;
    [SerializeField] private Sprite femaleCharacterButtonNonClicked;
    

    void Start()
    {
        InitButtonSetting(femaleCharacterButton, femaleCharacterButtonNonClicked);
        InitButtonSetting(maleCharacterButton, maleCharacterButtonNonClicked);

        maleCharacterButton.onClick.AddListener(OnMaleButtonClicked);
        femaleCharacterButton.onClick.AddListener(OnFemaleButtonClicked);
    }

    private void InitButtonSetting(Button button, Sprite defaultSprite)//버튼의 스프라이트를 디폴트 스프라이트로 변경
    {
        button.image.sprite = defaultSprite;
    }

    private void OnMaleButtonClicked()
    {
        InitButtonSetting(femaleCharacterButton, femaleCharacterButtonNonClicked);//기존에 눌려있던 여성 캐릭터 버튼의 스프라이트를 디폴트로 바꾼다.
        maleCharacterButton.image.sprite = maleCharacterButtonClicked;

    }

    private void OnFemaleButtonClicked()
    {
        InitButtonSetting(maleCharacterButton, maleCharacterButtonNonClicked);//기존에 눌려있던 남성 캐릭터 버튼의 스프라이트를 디폴트로 바꾼다.
        femaleCharacterButton.image.sprite = femaleCharacterButtonClicked;
    }


}
