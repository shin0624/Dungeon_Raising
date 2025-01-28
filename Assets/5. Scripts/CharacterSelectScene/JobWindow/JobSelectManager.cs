using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSelectManager : MonoBehaviour
{
    //���� ���� â �Ŵ��� : ���õ� ĳ���� ������ ���� [���� ����â]���� Ŭ���� �� �ִ� ������ �޶���. 
    //Ŭ���� ������ ���� ��ư ������ SelectWindwoController�� RestrictJobButtonByGender()���� ����
    //�� Ŭ���������� playerGender��(Male, Female)�� ���� ���� ��ư�� ���� �ٸ� ���� �����ϰ�, ���޹��� ���� ���� ���� ��ư Ŭ�� �� �׿� �´� �г��� �������� ����.
    // playerGender : Male -> �˻�, �ü� ��ư�� Male ���� -> Male[�˻�, �ü�]�迭�� �����. 
    // playerGender : Female -> �ü�, ������ ��ư�� Female ���� -> Female[�ü�, ������]�迭�� �����
    // Male, Female �迭���� �� ���� �� ������ �´� �г��� ����Ǿ��ְ�, �� �г��� MotionWindow�� ĳ���� ��Ʈ�� ǥ��. ��ų ��ư Ŭ�� �� ĳ���� ��Ʈ�� �Ķ���� ��ȯ.
    // �˻� ��ư�� Ȧ��ٿ�� ���¿��� �ü� ��ư�� Ŭ�� �� �˻� �г��� ��Ȱ��ȭ, �ü� �г��� Ȱ��ȭ��.

    private string genderValue ="";
    [SerializeField] private GameObject[] maleJobPanels;//���� ���� �г�. [0] : �˻�, [1] : �ü�
    [SerializeField] private GameObject[] femaleJobPanels;//���� ���� �г� [0] : �ü�, [1] : ������
    [SerializeField] private Button knightButton;
    [SerializeField] private Button archerButton;
    [SerializeField] private Button magicianButton;
    [SerializeField] private GameObject jobWindow;

    void Start()
    {
        DeactiveAllPanels();//���̾��Ű�� �����ϴ� ��/�� ������ �г��� ��� ��Ȱ��ȭ
        StartCoroutine(WaitForGenderSelection());//���� ���� �Ϸ�� ���� ���
    }

    private void OnKnightButtonClicked()
    {
        ActiveSpecificPanel(maleJobPanels, 0);
        PlayerInfo.Instance.SetPlayerJob("Knight");
    }

    private void OnMaleArcherButtonClicked()
    {
        ActiveSpecificPanel(maleJobPanels, 1);
        PlayerInfo.Instance.SetPlayerJob("Archer");
    }

    private void OnFemaleButtonClicked()
    {
        ActiveSpecificPanel(femaleJobPanels, 0);
    }

    private void OnMagicianButtonClicked()
    {
        ActiveSpecificPanel(femaleJobPanels, 1);
        PlayerInfo.Instance.SetPlayerJob("Magician");
    }

    private IEnumerator WaitForGenderSelection()// ���� ������ �Ϸ�� �� ���� ����Ѵ�. ������Ʈ������ ���� ���� �Ϸ� ���� ������ �ۼ��ϸ� ���� ������ �Ϸ�� �Ŀ��� ���ǹ� Ȯ���� ��� �߻��ϹǷ� �ڷ�ƾ���� ó��.
    {
        while(string.IsNullOrEmpty(genderValue) && !jobWindow.activeSelf)//���� ���� NULL�� �ƴϰ� �� �� ���� + [���� ���� â]���� [���� ���� â]���� �Ѿ�� �� ���� ���
        {
            genderValue = PlayerInfo.Instance.GetPlayerGender();//������ ���� ���� ����.
            yield return null;//�� ������ ���
        }
        SetJobPanel(genderValue);
    }

    private void SetMaleJobPanel()//������ ������ ���
    {
        ActiveJobPanel(maleJobPanels);
          // ���� ������ ���� �� �� ������ �߰�(�̺�Ʈ �ߺ� ��� ����)
        knightButton.onClick.RemoveAllListeners();
        archerButton.onClick.RemoveAllListeners();

        knightButton.onClick.AddListener(OnKnightButtonClicked);
        archerButton.onClick.AddListener(OnMaleArcherButtonClicked);//�ü��� ��/�� ���� �����̰�, ������ �гθ� �ٸ��� ������ �Ű����� �гο� ���� �ü� ��ư�� �ٸ� �̺�Ʈ�� ���.
    }   

    private void SetFemaleJobPanel()//������ ������ ���
    {
        ActiveJobPanel(femaleJobPanels);

        // ���� ������ ���� �� �� ������ �߰�(�̺�Ʈ �ߺ� ��� ����)
        archerButton.onClick.RemoveAllListeners();
        magicianButton.onClick.RemoveAllListeners();

        archerButton.onClick.AddListener(OnFemaleButtonClicked);
        magicianButton.onClick.AddListener(OnMagicianButtonClicked);
    }

    private void ActiveJobPanel(GameObject[] panels)//������ �г��� Ȱ��ȭ�ϴ� �Լ�. ������ �����ִ� �гε��� ��� ��Ȱ��ȭ �� �� ��ư���� ���õ� ������ ���� �гθ� Ȱ��ȭ.
    {
        DeactiveAllPanels();//���� Ȱ��ȭ�� �г��� �����Ѵٸ� �ݴ´�. 
        if(panels.Length >0)
        {
            panels[0].SetActive(true);//[���� ���� â]�� ������ �׻� ù ��° ������ ��Ƽ�� �� ����
        }
    }

    private void ActiveSpecificPanel(GameObject[] panels, int index)// Ŭ���� ��ư�� ����Ű�� �гθ� Ȱ��ȭ�ϰ� �ٸ� �г��� ��Ȱ��ȭ��Ű�� �޼���. �� ��ư �̺�Ʈ���� ȣ��.
    {
        DeactiveAllPanels();
        if(index >=0 && index < panels.Length)
        {
            panels[index].SetActive(true);//�ش��ϴ� �г��� Ȱ��ȭ.
        }
    }

    private void DeactiveAllPanels()//�����ִ� ���� �г��� ��Ȱ��ȭ. ���� ���� �� ���� �����ϵ��� �����Ͽ��� ������, ���� ���� �� ���� ��� ���� �гε��� ���� ��Ȱ��ȭ��ų �� �ֵ��� DeactiveAllPanels�� �Ű������� ���ְ� ��� �гο� ���� �ݺ��� �����Ѵ�.
    {
         // ��� �г� ��Ȱ��ȭ (����, ���� ���)
        foreach (GameObject panel in maleJobPanels)
        {
            if (panel != null) panel.SetActive(false);
        }
        foreach (GameObject panel in femaleJobPanels)
        {
            if (panel != null) panel.SetActive(false);
        }
    }

    public void SetJobPanel(string text)//������ ���� ���� �г��� �����ϴ� �޼���. ���ڷδ� genderValue���� ���޹���.
    {
        switch(text)
        {
            case "Male":
                SetMaleJobPanel();
                break;

            case "Female":
                SetFemaleJobPanel();
                break;

            default : 
                Debug.LogError("Unknown gender value. Please Check.");
                break;
        }
    }
}
