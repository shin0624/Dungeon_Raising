using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithUIController : MonoBehaviour
{
    //���尣 UI ��Ʈ�ѷ�. ���尣�� 2���� ���׷��̵� �г� ������ ĳ���� ��ȭ, ���� ��ȭ �� ����� �����Ѵ�.
    // ĳ���� ���׷��̵� �г� : ��ư 4�� ( ��� �̹��� �׸���� ���������� ���� �ҷ����⸦ ������ ���̹Ƿ� �� Ŭ�������� ��ư�� �������� ����.)
    // ���� ���׷��̵� �г� : ��ư 4�� ( ���� �̹��� �׸���� ���������� ���� �ҷ����⸦ ������ ���̹Ƿ� �� Ŭ�������� ��ư�� �������� ����.)
    // �� �гο��� �����г�(������ / �±�) 2���� ����.
    // ���� �г��� ���� ����, ���� �г��� ���� �г� �� 1���� �迭�� �����Ͽ� ����. -> ĳ���� ���� �г�[ĳ���� ������, ĳ���� �±�], ���� ���� �г�[���� ������, ���� �±�]
    //�� Ŭ������ BlackSmithPanel�� �Ҵ��Ѵ�.
    [SerializeField] private Button[] characterPanelButtons = new Button[4];//ĳ���� ���׷��̵� �гο� ������ ��ư (0 : ������, 1 : �±�, 2 : ������ ����, 3 : �±� ����)
    [SerializeField] private Button[] heroPanelButtons = new Button[4];//���� ���׷��̵� �гο� ������ ��ư (0 : ������, 1 : �±�, 2 : ������ ����, 3 : �±� ����)
    [SerializeField] private GameObject[] characterPanels = new GameObject[2];//ĳ����â �����г�. [0] : ������, [1] : �±�
    [SerializeField] private GameObject[] heroPanels = new GameObject[2];//���� â �����г�. [0] : ������, [1] : �±ޱ�
    [SerializeField] private GameObject characterMainPanel;
    [SerializeField] private GameObject heroMainPanel;
    [SerializeField] private Button characterButton;
    [SerializeField] private Button heroButton;

    //���� ��ȭ ���� �ؽ�Ʈ ���

    private void OnEnable() 
    {
        RemoveAllListeners();//�̺�Ʈ �ߺ� ����� �����ϱ� ����, ó�� Ȱ��ȭ �� ��� ������ �ʱ�ȭ.
        characterMainPanel.SetActive(true);//ó�� ���尣 â�� ���µǸ� ĳ���� �����г��� ��µ�.
        AttachAllListeners();//�� �г��� ��ư�� �����ʸ� ���.
    }
    private void OnDisable() 
    {
        RemoveAllListeners();//�� �г��� ��ư �����ʵ��� ��� ����.
        DeActiveAllPanels();//��� �г��� ��Ȱ��ȭ.
    }

    private void AttachAllListeners()//������ ���
    {
        characterButton.onClick.AddListener(OnCharacterButtonClicked);
        heroButton.onClick.AddListener(OnHeroButtonClicked);  

        characterPanelButtons[0].onClick.AddListener(CharacterLevelUpPanelButtonClicked);
        characterPanelButtons[1].onClick.AddListener(CharacterUpgradePanelButtonClicked);
        characterPanelButtons[2].onClick.AddListener(PerformCharacterLevelUp);
        characterPanelButtons[3].onClick.AddListener(PerformCharacterUpgrade);

        heroPanelButtons[0].onClick.AddListener(HeroLevelUpPanelButtonClicked);
        heroPanelButtons[1].onClick.AddListener(HerorUpgradePanelButtonClicked);
        heroPanelButtons[2].onClick.AddListener(PerformHeroLevelUp);
        heroPanelButtons[3].onClick.AddListener(PerformHeroUpgrade);

    }

    private void RemoveAllListeners()//OnDisable()���� ȣ���� ��� ������ ���� �޼���
    {
        foreach (Button button in characterPanelButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        foreach (Button button in heroPanelButtons)
        {
            button.onClick.RemoveAllListeners();
        }
        characterButton.onClick.RemoveListener(OnCharacterButtonClicked);
        heroButton.onClick.RemoveListener(OnHeroButtonClicked);
        
    }

    private void DeActiveAllPanels()//OnDisable()���� ȣ���� ��� �г� ��Ȱ��ȭ �޼���
    {
        characterMainPanel.SetActive(false);
        heroMainPanel.SetActive(false);
        foreach(GameObject subPanel in characterPanels)
        {
            subPanel.SetActive(false);
        }
        foreach(GameObject subPanel in heroPanels)
        {
            subPanel.SetActive(false);
        }
    }

//----------------���� �г� ��ȯ ��ư �̺�Ʈ-------------------------------------

    private void OnCharacterButtonClicked()
    {
        if(!characterMainPanel.activeSelf)
        {
            characterMainPanel.SetActive(true);
            heroMainPanel.SetActive(false);
        }
    }

    private void OnHeroButtonClicked()
    {
        if(!heroMainPanel.activeSelf)
        {
            heroMainPanel.SetActive(true);
            characterMainPanel.SetActive(false);
        }
    }
//----------------ĳ���� ���׷��̵� �г� ��ư �̺�Ʈ-------------------------------------

    private void CharacterLevelUpPanelButtonClicked()//ĳ���͹�ư[0] : ĳ���� ������ �г�(interactionPanels[0,1]) ���
    {
        if(characterMainPanel.activeSelf && !characterPanels[0].activeSelf)
        {
            characterPanels[0].SetActive(true);
            characterPanels[1].SetActive(false);
        }
    }

    private void CharacterUpgradePanelButtonClicked()//ĳ���͹�ư[1] : ĳ���� �±� �г�(interactionPanels[0,2]) ���
    {
        if(characterMainPanel.activeSelf && !characterPanels[1].activeSelf)
        {
            characterPanels[1].SetActive(true);
            characterPanels[0].SetActive(false);
        }
    }

    private void PerformCharacterLevelUp()//ĳ���� �г� ���� ��
    {
        Debug.Log("ĳ���� ������");
    }

    private void PerformCharacterUpgrade()//ĳ���� �г� �±�
    {
        Debug.Log("ĳ���� �±�");

    }

    //----------------���� ���׷��̵� �г� ��ư �̺�Ʈ-------------------------------------

    private void HeroLevelUpPanelButtonClicked()//������ư[0] : ���� ������ �г�(interactionPanels[1,1]) ���
    {
        if(heroMainPanel.activeSelf && !heroPanels[0].activeSelf)
        {
            heroPanels[0].SetActive(true);
            heroPanels[1].SetActive(false);
        }
    }

    private void HerorUpgradePanelButtonClicked()//������ư[1] : ���� �±� �г�(interactionPanels[1,2]) ���
    {
        if(heroMainPanel.activeSelf && !heroPanels[1].activeSelf)
        {
            heroPanels[1].SetActive(true);
            heroPanels[0].SetActive(false);
        }
    }

    private void PerformHeroLevelUp()//���� �г� ���� ��
    {
        Debug.Log("���� ������");
    }

    private void PerformHeroUpgrade()//���� �г� �±�
    {
        Debug.Log("���� �±�");
    }
}