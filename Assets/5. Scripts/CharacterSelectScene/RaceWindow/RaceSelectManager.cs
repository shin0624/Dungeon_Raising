using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceSelectManager : MonoBehaviour
{
    //���� ���� â �Ŵ��� : ��ư���� ���� �г��� Ȱ��ȭ. ������ 3������ ������, ���� ��ư�� ���� �� ���� playerInfo.playerRace���� ���ŵǰ�, ���� ��ư�� Ȧ��ٿ�� ���·� "����"��ư�� ������ �� ������ playerRace�� ���� �����. 
    // ���� �г��� RaceWindow - RaceWindowPivot �Ʒ��� �̸� ��Ȱ��ȭ ���·� �ε�Ǿ� ����. ��ư�� ���� �� ���� ���� �г��� ��Ȱ��ȭ�ǰ� ��ư�� �����Ǵ� �г��� Ȱ��ȭ.
    // ���� �гΰ� ���� �� ��� �迭 �� ���� ������ ����. ��ũ��Ʈ ������ �����ϴ� �� ���� �ν����Ϳ��� �������ִ� ����� ���

    [SerializeField] private GameObject[] racePanels;
    [SerializeField] private string[] raceNameHash;
    [SerializeField] private Button[] raceButtons;

    void Start()
    {
        DeactiveAllPanels();

        for(int i=0; i<raceButtons.Length; i++)//��ư �̺�Ʈ ���� �Ҵ�
        {
            int index = i;//Ŭ���� ĸó ����
            raceButtons[i].onClick.AddListener(() => OnRaceButtonClicked(index));
        }
        InitActivePanel();
    }

    private void InitActivePanel()//ó�� [���� ���� â]�� ������ racePanels[0]�� �ش��ϴ� �г��� Ȧ��ٿ� �� ����.
    {
        ActiveSpecificPanel(0);
    }

    private void OnRaceButtonClicked(int index)//��ư�� �������� �� �̺�Ʈ �޼���. ��ư Ŭ�� �� ������ ���� �г��� �ݰ� racePanel[n]�� Ȱ��ȭ�� �� raceNameHash���� playerInfo.Playerrace�� �����Ѵ�.
    {
        if(index <0 || index >= racePanels.Length || index >=raceNameHash.Length)//�ε��� ��ȿ�� �˻� ����
        {
            Debug.LogError("Invalid Race index!");
            return;
        }
        ActiveSpecificPanel(index);//���õ� ��ư�� ���� �г� Ȱ��ȭ
        PlayerInfo.Instance.SetPlayerRace(raceNameHash[index]);//�÷��̾� ������ playerRace���� ����.

        Debug.Log($"{raceNameHash[index]} button clicked. ");
    }

    private void DeactiveAllPanels()//���� �гε� ��� ��Ȱ��ȭ.
    {
        foreach(GameObject panel in racePanels)
        {
            if (panel != null) panel.SetActive(false);
        }
    }

    private void ActiveSpecificPanel(int index)//��ư�� ���� �г��� Ȱ��ȭ. ��ư �̺�Ʈ �Լ����� �Ѱܹ��� index�� �г� �ε����� ���Ͽ� ������ Ȱ��ȭ.
    {
        DeactiveAllPanels();//��� �г� �켱 ��Ȱ��ȭ.
        if(racePanels[index] != null)//index������ �ش��ϴ� �г��� �����ϸ� Ȱ��ȭ
        {
            racePanels[index].SetActive(true);
        }

    }
}
