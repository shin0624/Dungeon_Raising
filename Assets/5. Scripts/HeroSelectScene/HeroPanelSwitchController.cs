using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanelSwitchController : MonoBehaviour
{
    // �¿� ��ư�� ���� ����� �г��� ��ȯ�ϴ� ��ũ��Ʈ.
    // HeroPanelLoadController.cs���� ������ �̱��� �ν��Ͻ��� ���� ���� �г� �迭�� �г� �θ� Ʈ�������� �����Ѵ�.
    //�г��� Ǯ���� ����Ͽ� �̸� ��Ȱ��ȭ ���ѳ���, ��ư Ŭ�� �� �ε��� ��갪�� ���� Ȱ��ȭ/��Ȱ��ȭ �ϸ� �÷��̾�� �����ش�.
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    private GameObject [] panelPool;//�г� ������Ʈ���� ������Ʈ Ǯ������ �����Ѵ�.
    private int currentIndex;//���� �ε���

    private void Start() 
    {
        InitPanelPool();//�г� ������Ʈ Ǯ �ʱ�ȭ
        currentIndex = HeroPanelLoadController.instance.selectedHeroIndex;//currentIndex�� HeroPanelLoadController���� �޾ƿ� ���õ� ���� �ε��� ��.
        SwitchPanel(currentIndex);//�г� Ȱ��ȭ

        leftButton.onClick.AddListener(OnLeftButtonClick);//�� ��ư Ŭ�� �� �̺�Ʈ �߰�
        rightButton.onClick.AddListener(OnRightButtonClick);//�� ��ư Ŭ�� �� �̺�Ʈ �߰�
    }

    private void InitPanelPool()//�г� ������Ʈ Ǯ �ʱ�ȭ
    {
        int panelCount = HeroPanelLoadController.instance.heroPanelPrefabs.Length;//�г� ������ HeroPanelLoadController�� heroPanelPrefabs �迭 ���̿� ����.
        panelPool = new GameObject[panelCount];//�г� ������ŭ�� �迭 ����.

        for(int i=0; i<panelCount; i++)
        {
            panelPool[i] = Instantiate(HeroPanelLoadController.instance.heroPanelPrefabs[i], HeroPanelLoadController.instance.heroPanelParent);//�г� �������� �����Ͽ� Ǯ�� �ִ´�. ��ġ�� HeroPanelParent.
            panelPool[i].SetActive(false);//������ �г��� ��Ȱ��ȭ ���·� �д�.
        }
    }

    private void SwitchPanel(int index)//�г� Ȱ��ȭ �� ���� �г� ��Ȱ��ȭ. �� �޼��带 ����, HeroSelectScene �ε� �� ���� ��(ThroneScene)���� ���õ� ������ �ش��ϴ� �гθ� Ȱ��ȭ�ȴ�. 
    {
        for(int i=0; i<panelPool.Length; i++)
        {
            panelPool[i].SetActive(i==index);//���� �ε����� ���� �гθ� Ȱ��ȭ. �ε����� 1�̸� 1��° �гθ� Ȱ��ȭ�ϴ� ��.
        }
    }

    private void OnLeftButtonClick()//�� ��ư Ŭ�� ��
    {
        currentIndex--; //�ε��� ����
        if(currentIndex < 0)
        {
            currentIndex = panelPool.Length-1;//�ε����� 0���� �۾����� �迭�� ������ �ε����� ����. ��, ��ȯ �����ϵ��� ����
        }
        SwitchPanel(currentIndex);//�г� ��ȯ
    }

    private void OnRightButtonClick()//�� ��ư Ŭ�� ��
    {
        currentIndex++;//�ε��� ����
        if(currentIndex >= panelPool.Length)
        {
            currentIndex = 0;//�ε����� �迭 ���̺��� Ŀ���� 0���� ����.
        }
        SwitchPanel(currentIndex);//�г� ��ȯ
    }
    

}
