using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NicknameInputController : MonoBehaviour
{
    //�г����� �Է� ������ ��Ӿ� üũ�� ���ļ� characterName�� ������Ʈ ��Ű�� Ŭ����
    [SerializeField] TMP_InputField inputNickname;//��ǲ�ʵ� 
    [SerializeField] TextMeshProUGUI characterName;//background ĵ������ ��ġ�� ĳ���� �̸� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI alertText;// ��Ӿ� ���ο� ���� ��� or ����� ��Ÿ���� �ؽ�Ʈ
    [SerializeField] GameObject selectWindowPanel;//�г��� �Է� ���� �� ���� �гε� ��Ȱ��ȭ �� �ΰ��� ȭ������.
    
    [SerializeField] private BadWordFilter badWordFilter;
    [SerializeField] private InputFieldSetting inputFieldSetting;
     
    void Start()
    {
        Init();
        inputNickname.onSubmit.AddListener(OnNicknameSubmit);//��ǲ �ʵ忡�� Enter Ű �Է� �� ��Ӿ� üũ �̺�Ʈ�� ����
    }

    private void Init()
    {
        if(badWordFilter==null)//badwordfilter������Ʈ ���� üũ
        {
            badWordFilter = gameObject.GetComponent<BadWordFilter>();
        }
        if(!badWordFilter.IsInitialized())//SDK �ʱ�ȭ ���� üũ
        {
            Debug.LogError("��Ӿ� ���͸� SDK�� �ʱ�ȭ���� �ʾҽ��ϴ�. Ȯ���� �ʿ��մϴ�.");
            return;
        }
    }

    private void OnNicknameSubmit(string text)//�г����� ��Ӿ� ���ο� ���� submit�� ������ ����. ��ǲ �ʵ忡�� �Է¹��� inputNickname.text�� ���ڷ� �޴´�.
    {
        if(IsBadWord(text))//��Ӿ��� ���(isbadword=true)
        {
            ShowMessage("��Ӿ�� ����� �� �����ϴ�.");//������
            ReActiveInputField();//��ǲ �ʵ� �ʱ�ȭ. �ٽ� �ۼ�
        }
        else
        {
            ShowMessage("��� ������ �г��� �Դϴ�.");
            UpdateCharacterName(text);
            GoToInGame();
        }
    }

    private bool IsBadWord(string text)//�г����� ��Ӿ� ���θ� Ȯ��
    {
        return badWordFilter.FilterFunc(text);//�ڳ� sdk�� ��Ӿ� ���� ����� ����Ͽ� ��Ӿ� ���θ� Ȯ���Ѵ�. FilterFunc���� ��Ӿ �ɸ��� true, ��Ӿ ������ false�� ���޵ȴ�.
    }

    private void UpdateCharacterName(string text)
    {
        inputFieldSetting.UpdateFontSizeAndText(text);//ĳ���� �̸��� ��ǲ�ʵ忡 �ۼ��� �̸����� ������Ʈ
    }

    private void ShowMessage(string alertMessage)//��Ӿ �߰߿��� �޼��� ���
    {
        alertText.text = alertMessage;
    }
    private void GoToInGame()
    {
        selectWindowPanel.SetActive(false);
    }

    private void ReActiveInputField()
    {
        inputNickname.text = "";// ��ǲ �ʵ� �ؽ�Ʈ �ʱ�ȭ
        inputNickname.ActivateInputField();//��ǲ �ʵ� �ٽ� Ȱ��ȭ
    }
}