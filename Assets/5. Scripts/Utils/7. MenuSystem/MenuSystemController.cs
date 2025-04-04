using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSystemController : MonoBehaviour
{
    //���� ��� �޴� ��ư Ŭ������ �����ϴ� �޴� �ý����� �����ϴ� Ŭ����. 
    //MenuPanel�� [��������, ������, ����] 3���� ��ư���� ������ -> �� ��ư�� ������ �ش��ϴ� �г��� ���� MenuPanelPivot���� Ȱ��ȭ��.
    [SerializeField] private Button[] buttonsInMenuPanel;//�޴� �гο� ��ġ�� 3���� ��ư
    [SerializeField] private GameObject[] panelsInPivot;
    void Start()
    {
        DeactiveAllPanels();//�����ִ°� �ִٸ� �ݰ�
        AssignToButtons();//��ư�� �̺�Ʈ �Ҵ�
    }

    private void AssignToButtons()//�� ��ư�� Ŭ�� �̺�Ʈ�� �Ҵ�.
    {
        for(int i=0; i<buttonsInMenuPanel.Length; i++)//��ư �̺�Ʈ ���� �Ҵ�
        {
            int index = i;//Ŭ���� ĸó ����
            buttonsInMenuPanel[i].onClick.AddListener(() => OnMenuPanelButtonClicked(index));
        }
    }

    private void OnMenuPanelButtonClicked(int index)//��ư�� �Ҵ��� Ŭ�� �̺�Ʈ
    {
        if(index < 0 || index >= panelsInPivot.Length)//�ε��� ��ȿ�� �˻� ����
        {
            Debug.LogError("Invalid MenuPanel index!");
            return;
        }
        ActiveSpecificPanel(index);//���õ� ��ư�� ���� �г� Ȱ��ȭ
    }

    private void DeactiveAllPanels()//���� �гε� ��� ��Ȱ��ȭ.
    {
        foreach(GameObject panel in panelsInPivot)
        {
            if (panel != null) panel.SetActive(false);
        }
    }

    private void ActiveSpecificPanel(int index)//��ư�� ���� �г��� Ȱ��ȭ. ��ư �̺�Ʈ �Լ����� �Ѱܹ��� index�� �г� �ε����� ���Ͽ� ������ Ȱ��ȭ.
    {
        DeactiveAllPanels();//��� �г� �켱 ��Ȱ��ȭ.
        if(panelsInPivot[index] != null)//index������ �ش��ϴ� �г��� �����ϸ� Ȱ��ȭ
        {
            panelsInPivot[index].SetActive(true);
            DOTWeenUIAnimation.PopupAnimationInUI(panelsInPivot[index], panelsInPivot[index].transform.localScale * 1.2f, 0.2f, panelsInPivot[index].transform.localScale, 0.2f);
        }
    }
}
