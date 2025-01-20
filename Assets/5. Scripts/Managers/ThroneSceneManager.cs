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
    void Start()
    {
        Init();
    }

    private void HeroButtonClicked()//���� ��ư Ŭ�� �� ���� �г� Ȱ��ȭ. ���� ���� �г� ��Ȱ��ȭ
    {
        dungeonFightPanel.SetActive(false);
        heroPanel.SetActive(true);
    }
    
    private void DungeonFightButtonClicked()//���� ���� ��ư Ŭ�� �� ���� ���� �г� Ȱ��ȭ. ���� �г� ��Ȱ��ȭ
    {
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
    }
}
