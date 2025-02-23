using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NicknameInputController : MonoBehaviour
{
    //�г����� �Է� ������ ��Ӿ� üũ�� ���ļ� characterName�� ������Ʈ ��Ű�� Ŭ����
    [SerializeField] TMP_InputField inputNickname;//��ǲ�ʵ� 
    [SerializeField] TextMeshProUGUI characterName;//background ĵ������ ��ġ�� ĳ���� �̸� �ؽ�Ʈ
    [SerializeField] TextMeshProUGUI alertText;// ��Ӿ� ���ο� ���� ��� or ����� ��Ÿ���� �ؽ�Ʈ
    [SerializeField] GameObject selectWindowPanel;//�г��� �Է� ���� �� ���� �гε� ��Ȱ��ȭ �� �ΰ��� ȭ������.
    [SerializeField] private Button acceptButton;//�г��� �Է� �Ϸ� ��ư
    [SerializeField] private GameObject yesOrNoPanel;//�г��� ��� ���� Ȯ�� �г�
    [SerializeField] private Button yesButton;//�г��� ��� Ȯ�� ��ư
    [SerializeField] private Button noButton;//�г��� ��� ��� ��ư
    
    [SerializeField] private BadWordFilter badWordFilter;
    [SerializeField] private InputFieldSetting inputFieldSetting;
    //[SerializeField] private AsyncSceneLoader asyncSceneLoader;
     
    void Start()
    {
        StartCoroutine(DelayInitBadWordFilter());
        inputNickname.onSubmit.AddListener(OnNicknameSubmit);//��ǲ �ʵ忡�� Enter Ű �Է� �� ��Ӿ� üũ �̺�Ʈ�� ����
        acceptButton.onClick.AddListener(()=>OnNicknameSubmit(inputNickname.text));//�г��� �Է� �Ϸ� ��ư Ŭ�� �� ��Ӿ� üũ �̺�Ʈ�� ����
        yesOrNoPanel.SetActive(false);//�г��� ��� ���� Ȯ�� �г� ��Ȱ��ȭ
    }

    private IEnumerator DelayInitBadWordFilter()//��Ӿ� ���͸� sdk�� �ʱ�ȭ���� üũ�� 0.5�� ������Ų��. BadWordFilter������ �ʱ�ȭ�� ���� ����� �� �ʱ�ȭ ���θ� üũ�ؾ� �ϱ� ����. �� �޼��� ������ ������ ������ ������ �ʱ�ȭ ���ΰ� ���� False�� �� �� �ʱ�ȭ�� ����� �� �־� ������ ���ߵ� �� �����Ƿ�.
    {
        yield return new WaitForSeconds(0.5f);
        Init();
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
            StartCoroutine(AcceptButtonColorChange());//�Ϸ� ��ư ���� ���� �� ��Ȱ��ȭ
            ShowMessage("��Ӿ�� ����� �� �����ϴ�.");//������
            ReActiveInputField();//��ǲ �ʵ� �ʱ�ȭ. �ٽ� �ۼ�
        }
        else if(string.IsNullOrEmpty(text))//�г����� ������� ���
        {
            StartCoroutine(AcceptButtonColorChange());//�Ϸ� ��ư ���� ���� �� ��Ȱ��ȭ
            ShowMessage("�г����� �Է����ּ���.");//������
            ReActiveInputField();//��ǲ �ʵ� �ʱ�ȭ. �ٽ� �ۼ�
        }
        else
        {
            ShowMessage("��� ������ �г��� �Դϴ�. ����Ͻðڽ��ϱ�?");//��� ���θ� Ȯ���ϴ� �г��� Ȱ��ȭ�ȴ�. ���� yes��ư�� ������ GoToInGame()�޼��尡 ����ȴ�. no��ư�� ������ ��ǲ�ʵ尡 �ʱ�ȭ�ȴ�.
            ActivateYesOrNoPanel(text);
        }
    }

    private void ActivateYesOrNoPanel(string text)
    {
        acceptButton.gameObject.SetActive(false);
        yesOrNoPanel.SetActive(true);
        yesButton.onClick.AddListener(() => OnYesButtonClicked(text));
        noButton.onClick.AddListener(OnNoButtonClicked);
    }

    private void OnYesButtonClicked(string text)
    {
        yesOrNoPanel.SetActive(false);
        UpdateCharacterName(text);
        GoToInGame();
    }
    private void OnNoButtonClicked()
    {
        yesOrNoPanel.SetActive(false);
        ShowMessage("�г����� �Է����ּ���.");//
        acceptButton.gameObject.SetActive(true);
        ReActiveInputField();
    }

    private bool IsBadWord(string text)//�г����� ��Ӿ� ���θ� Ȯ��
    {
        return badWordFilter.FilterFunc(text);//�ڳ� sdk�� ��Ӿ� ���� ����� ����Ͽ� ��Ӿ� ���θ� Ȯ���Ѵ�. FilterFunc���� ��Ӿ �ɸ��� true, ��Ӿ ������ false�� ���޵ȴ�.
    }

    private void UpdateCharacterName(string text)
    {
        inputFieldSetting.UpdateFontSizeAndText(text);//ĳ���� �̸��� ��ǲ�ʵ忡 �ۼ��� �̸����� ������Ʈ
        PlayerInfo.Instance.SetPlayerNickname(text);//�ۼ��� �г����� PlayerInfo�� ����
    }

    private void ShowMessage(string alertMessage)//��Ӿ� �߰߿��� �޼��� ���
    {
        alertText.text = alertMessage;
    }
    private void GoToInGame()
    {
        selectWindowPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
        //asyncSceneLoader.LoadMainSceneAsync();//�񵿱� �ε� �õ�
    }

    private void ReActiveInputField()
    {
        inputNickname.text = "";// ��ǲ �ʵ� �ؽ�Ʈ �ʱ�ȭ
        inputNickname.ActivateInputField();//��ǲ �ʵ� �ٽ� Ȱ��ȭ
    }

    private IEnumerator AcceptButtonColorChange()//�Ϸ� ��ư ���� ���� �� ��Ȱ��ȭ
    {
        acceptButton.interactable = false;//�г��� �Է� �Ϸ� ��ư ��Ȱ��ȭ
        acceptButton.image.color = Color.Lerp(Color.red, Color.white, 0.5f);
        yield return new WaitForSeconds(0.5f);
        acceptButton.image.color = new Color(0.09f, 0.5f, 1.0f, 1.0f);
        acceptButton.interactable = true;//�г��� �Է� �Ϸ� ��ư Ȱ��ȭ
    }
}
