using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroneSceneManager : MonoBehaviour
{
    //���� ���� �г� ��ȯ ���� ����ϴ� �Ŵ���
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private GameObject dungeonFightPanel;
    [SerializeField] private Button heroButton;
    [SerializeField] private Button dungeonFightButton;
    [SerializeField] private Image characteristicImage;
    void Start()
    {
        Init();
    }

    private void HeroButtonClicked()//���� ��ư Ŭ�� �� ���� �г� Ȱ��ȭ. ���� ���� �г� ��Ȱ��ȭ
    {
         characteristicImage.gameObject.SetActive(true);
        dungeonFightPanel.SetActive(false);
        heroPanel.SetActive(true);
    }
    
    private void DungeonFightButtonClicked()//���� ���� ��ư Ŭ�� �� ���� ���� �г� Ȱ��ȭ. ���� �г� ��Ȱ��ȭ
    {
        characteristicImage.gameObject.SetActive(false);
        heroPanel.SetActive(false);
        dungeonFightPanel.SetActive(true);
    }

    private void Init()
    {
        NullCheck();
        heroButton.onClick.AddListener(HeroButtonClicked);//���� ��ư �̺�Ʈ ������ ���
        dungeonFightButton.onClick.AddListener(DungeonFightButtonClicked);//���� ���� ��ư �̺�Ʈ ������ ���
        heroPanel.SetActive(true);//ó�� ���� ������ ���� �г��� ���� Ȱ��ȭ�Ǿ� �ִ� ����.
        dungeonFightPanel.SetActive(false);
        characteristicImage.gameObject.SetActive(true);
    }

    private void NullCheck()
    {
        if(heroPanel==null)
        {
            Debug.Log("HeroPanel is NULL");
            heroPanel = GameObject.Find("HeroPanel");
        }
        else
        {
            Debug.Log("HeroPanel Set Active");
        }

        if(dungeonFightPanel==null)
        {
            Debug.Log("DungeonFightPanel is NULL");
            dungeonFightPanel = GameObject.Find("DungeonFightPanel");
        }
        else
        {
            Debug.Log("DungeonFightPanel Set Active");
        }

        if(characteristicImage==null)
        {
            Debug.Log("CharacteristicImage is NULL");
            dungeonFightPanel = GameObject.Find("CharacteristicImage");
        }
    }
}
