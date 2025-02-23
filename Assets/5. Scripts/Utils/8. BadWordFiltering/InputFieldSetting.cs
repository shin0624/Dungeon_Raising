using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldSetting : MonoBehaviour
{
    //�г��� �Է�â ���� - �ѱ�, ����, ���� ���� �ִ� 10�����̸�, onValueChanged�̺�Ʈ�� Ȱ���Ͽ� �Էµ� ���� ���� ���� ���� ũ��� ĳ���� ����Ʈ�� �������� �����ϵ��� ��.
    [SerializeField] TextMeshProUGUI characterNameText;//�Է��ʵ忡���� �ؽ�Ʈ�� �ݿ��� ����� â�� �г��� �ؽ�Ʈ
    [SerializeField] private TMP_InputField inputNickname;//�г����� �Է¹��� �Է��ʵ�
    private const int MaxCharacterLimit = 10;
    private const float FontSizeLarge = 34.7f;
    private const float FontSizeSmall = 30.0f;
    void Start()
    {
        inputNickname.characterLimit = MaxCharacterLimit;//���ڼ� ���� : 10
        inputNickname.onValueChanged.AddListener(UpdateFontSizeAndText);
    }

    
    public void UpdateFontSizeAndText(string text)//���� ���� ���� ���� ũ��, ������ �������� �����Ѵ�.
    {
        int length = text.Length;
        if(length <=8)//���� ���� 8���� �̳��̸� ǥ�õǴ� ũ�⸦ ũ��
        {
            characterNameText.fontSize = FontSizeLarge;
        }
        else if(length<=MaxCharacterLimit)//���� ���� 10���ڱ��� ���� ǥ�õǴ� ũ�⸦ ũ��
        {
            characterNameText.fontSize = FontSizeSmall;
        }

        characterNameText.text = text;//���� �ݿ��Ͽ� �ѱ��.
    }
}
