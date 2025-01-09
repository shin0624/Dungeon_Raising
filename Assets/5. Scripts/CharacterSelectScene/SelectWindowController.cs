using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectWindowController : MonoBehaviour
{
    //ĳ���� ����â Ȱ/��Ȱ��ȭ�� �����ϴ� ��ũ��Ʈ.
    [SerializeField] private GameObject characterWindow;
    [SerializeField] private GameObject jobWindow;
    [SerializeField] private GameObject raceWindow;
    [SerializeField] private GameObject nicknameWindow;
    [SerializeField] private Image characterButton;
    [SerializeField] private Image jobButton;
    [SerializeField] private Image raceButton;
    private Color activeColor = new Color(0.6862745f, 0.5686275f, 0.1176471f, 1f );//Ȱ��ȭ �� ��
    private Color inActiveColor = Color.white;//�⺻ ��
    void Start()
    {
        ActivateWindow("Character");
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
        ActivateWindow("Job");
    }

    public void OnRaceButtonClicked()
    {
        ActivateWindow("Race");
    }

    public void OnNicknameButtonClicked()
    {
        ActivateWindow("Nickname");
    }
    
    public void OnOkayButtonClicked()
    {
        ActivateWindow("UserInfoSave");
    }
}
