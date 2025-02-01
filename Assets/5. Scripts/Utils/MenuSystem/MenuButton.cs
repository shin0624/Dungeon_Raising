using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    //�޴� ��ư�� Ŭ���ϸ� MenuPivot�� Anchor�� MenuPanel�� Ȱ��ȭ�� -> �޴� ��ư�� �ٽ� ������ ��Ȱ��ȭ��.
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject menuPanel;
    void Start()
    {
        menuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    private void OnMenuButtonClicked()
    {
        if(!menuPanel.activeSelf)//�޴� �г��� ���� ���� �� �޴� ��ư Ŭ�� �� 
        {
            menuPanel.SetActive(true);//�޴� �г� Ȱ��ȭ
        }
        else//�޴� �г��� ���� ���� �� �޴� ��ư Ŭ�� ��
        {
            menuPanel.SetActive(false);//�޴� ��ư ��Ȱ��ȭ
        }
    }
}
