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

    private void InitButtonSetting(Button button, Sprite defaultSprite)//��ư�� ��������Ʈ�� ����Ʈ ��������Ʈ�� ����
    {
        button.image.sprite = defaultSprite;
    }

    private void OnMaleButtonClicked()
    {
        InitButtonSetting(femaleCharacterButton, femaleCharacterButtonNonClicked);//������ �����ִ� ���� ĳ���� ��ư�� ��������Ʈ�� ����Ʈ�� �ٲ۴�.
        maleCharacterButton.image.sprite = maleCharacterButtonClicked;

    }

    private void OnFemaleButtonClicked()
    {
        InitButtonSetting(maleCharacterButton, maleCharacterButtonNonClicked);//������ �����ִ� ���� ĳ���� ��ư�� ��������Ʈ�� ����Ʈ�� �ٲ۴�.
        femaleCharacterButton.image.sprite = femaleCharacterButtonClicked;
    }


}
