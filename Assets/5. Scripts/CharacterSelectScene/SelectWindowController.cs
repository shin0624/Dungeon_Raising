using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class SelectWindowController : MonoBehaviour
{
    //ĳ���� ����â Ȱ/��Ȱ��ȭ�� �����ϴ� ��ũ��Ʈ.
    // [ĳ���� ���� â] -> ���� ĳ����, ���� ĳ���� ��ư ����. �� �� �ϳ��� Ŭ������ �ʾ��� ��� "����"��ư ��Ȱ��ȭ
    // [���� ���� â] -> ���� ĳ���� ���� �� �˻�, �ü� ���� ����. ������� ��Ȱ��ȭ | ���� ĳ���� ���� �� �ü�, ������ ���� ����. �˻�� ��Ȱ��ȭ | �� �� �ϳ��� Ŭ������ �ʾ��� ��� "����"��ư ��Ȱ��ȭ
    // [���� ���� â]
    [SerializeField] private GameObject characterWindow;
    [SerializeField] private GameObject jobWindow;
    [SerializeField] private GameObject raceWindow;
    [SerializeField] private GameObject nicknameWindow;
    [SerializeField] private Image characterButton;
    [SerializeField] private Image jobButton;
    [SerializeField] private Image raceButton;
    //---------250126 ĳ���� �Ӽ� �� ���� ��ư Ȱ/��Ȱ ���� �߰�
    [SerializeField] private Button knightButton;//�˻� ��ư
    [SerializeField] private Button archerButton;//�ü� ��ư
    [SerializeField] private Button magicianButton;//������ ��ư
    [SerializeField] private Button maleCharacterButton;//���� ĳ���� ��ư
    [SerializeField] private Button femaleCharacterButton;//���� ĳ���� ��ư
    [SerializeField] private GameObject[] failurePanels;//�� �׸� �̼��� �� ��Ÿ���� ��� �г�.

    private string playerGender = "";//�÷��̾� ����
    [SerializeField] private JobSelectManager jobSelectManager;
    //-------------------------------------------------------------------
    private Color activeColor = new Color(0.6862745f, 0.5686275f, 0.1176471f, 1f );//Ȱ��ȭ �� �÷�
    private Color inActiveColor = Color.white;//�⺻ ��
    void Start()
    {
        ActivateWindow("Character");
        Init();
    }

    private void Init()//�ʱ� ��� �г� ��� ��Ȱ��ȭ
    {
        foreach(GameObject panel in failurePanels)
        {
            panel.SetActive(false);
        }
    }

    public void ActivateWindow(string windowName)
    {
        // ��� â�� ��Ȱ��ȭ�ϰ� ��ư ������ �⺻ ������ ����
        characterWindow.SetActive(false);
        jobWindow.SetActive(false);
        raceWindow.SetActive(false);
        characterButton.color = inActiveColor;
        jobButton.color = inActiveColor;
        raceButton.color = inActiveColor;

        // Ư�� â�� Ȱ��ȭ�ϰ� ��ư ������ Ȱ�� ������ ����
        switch (windowName)
        {
            case "Character"://ĳ���� ���� â
                characterWindow.SetActive(true);
                characterButton.color = activeColor;
                break;

            case "Job": // ���� ���� â
                jobWindow.SetActive(true);
                RestrictionJobButtonByGender();
                jobButton.color = activeColor;
                break;

            case "Race"://���� ���� â
                raceWindow.SetActive(true);
                raceButton.color = activeColor;
                break;

            case "Nickname": //�г��� �Է� â
                nicknameWindow.SetActive(true);
                characterButton.gameObject.SetActive(false);
                jobButton.gameObject.SetActive(false);
                raceButton.gameObject.SetActive(false);
                break;

            case "UserInfoSave":
            //���� ����� �Է� ������ ���� ����. �� ���� �г��� â���� ��ȿ�� �˻� ���� ����
                gameObject.SetActive(false);
                break;
            default:
                Debug.LogError("Invalid window name: " + windowName);
                break;
        }
    }
     // ��ư Ŭ�� �̺�Ʈ�� ������ �޼���. �� �����쿡 �ִ� ���� ��ư���� OnClick�̺�Ʈ�� �Ҵ��Ѵ�.
    public void OnCharacterButtonClicked()
    {
        ActivateWindow("Character");
    }

    public void OnJobButtonClicked()
    {
        GenderChoiceCheck();//���� ���� ���� �˻� �� â ���� ���� ����
    }

    public void OnRaceButtonClicked()
    {
        JobChoiceCheck();//���� ���� ���� �˻� �� â ���� ���� ����
    }

    public void OnNicknameButtonClicked()
    {
        RaceChoiceCheck();//���� ���� ���� �˻� �� â ���� ���� ����
    }
    
    public void OnOkayButtonClicked()
    {
        ActivateWindow("UserInfoSave");
    }

    public void OnMaleButtonClicked()//���� ĳ���� ��ư Ŭ�� �� true
    {  
        if(playerGender=="Female" || string.IsNullOrEmpty(playerGender))
        {
            playerGender = "Male";
        }
        PlayerInfo.Instance.SetPlayerGender(playerGender);
        Debug.Log("Male Clicked. PlayerGender : " + playerGender);
        jobSelectManager.SetJobPanel(playerGender);
    }

    public void OnFemaleButtonClicked()//���� ĳ���� ��ư Ŭ�� �� false
    {
        if(playerGender=="Male" || string.IsNullOrEmpty(playerGender))
        {
            playerGender = "Female";
        }
        PlayerInfo.Instance.SetPlayerGender(playerGender);
        Debug.Log("Female Clicked. PlayerGender : " + playerGender);
        jobSelectManager.SetJobPanel(playerGender);
    }

    private void RestrictionJobButtonByGender()//[ĳ���� ���� â]���� ������ ������ ���� [���� ���� â]���� ������ �� �ִ� ������ ��ư�� �޶�����. 
    {
        if(PlayerInfo.Instance.GetPlayerGender()=="Male")
        {
            knightButton.interactable = true;
            archerButton.interactable = true;
            magicianButton.interactable = false;
        }
        if(PlayerInfo.Instance.GetPlayerGender()=="Female")
        {
            knightButton.interactable = false;
            archerButton.interactable = true;
            magicianButton.interactable = true;
        }
        else
        {
            Debug.Log("PlayerGender is not set.");
        }
    }

    private IEnumerator ActiveFailureText(int index)//��� �˾� ��� �ڷ�ƾ
    {
        failurePanels[index].SetActive(true);
        yield return new WaitForSeconds(1.0f);
        failurePanels[index].SetActive(false);
    }

    private void GenderChoiceCheck()//���� ��ư�� ���� ���¿��� "����"��ư�� Ŭ���ߴٸ� ����. ���� ��ư�� ������ �ʾҴٸ� "����" ��ư ��Ȱ��ȭ. ��Ȱ��ȭ ���¿��� ��ư Ŭ�� �� FailureText ���
    {
        if(PlayerInfo.Instance.GetPlayerGender()!=null)
        {
            ActivateWindow("Job");
        }
        else
        {
            Debug.Log("PlayerGender is not set.");
            StartCoroutine(ActiveFailureText(0));
        }
    }

    private void JobChoiceCheck()
    {
        if(PlayerInfo.Instance.GetPlayerJob()!=null)
        {
            ActivateWindow("Race");
        }
        else
        {
            Debug.Log("PlayerJob is not set.");
            StartCoroutine(ActiveFailureText(1));
        }
    }

    private void RaceChoiceCheck()
    {
        if(PlayerInfo.Instance.GetPlayerRace()!=null)
        {
            ActivateWindow("Nickname");
        }
        else
        {
            Debug.Log("PlayerRace is not set.");
            StartCoroutine(ActiveFailureText(2));
        }
    }

}
