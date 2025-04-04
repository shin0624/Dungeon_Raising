using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DutyButtonController : MonoBehaviour
{
    //����Ʈ ��ư�� Ŭ���ϸ� DutyPanel Anchor�� QuestPanel�� Ȱ��ȭ�� -> ����Ʈ ��ư�� �ٽ� ������ ��Ȱ��ȭ��.
    //�κ��丮 ��ư�� �����ϴ� ��ũ��Ʈ
    [SerializeField] private Button dutyButton;
    [SerializeField] private GameObject dutyPanel;

    private void OnEnable() 
    {
        dutyPanel.SetActive(false);
    }
    
    private void Start()
    { 
        dutyButton.onClick.AddListener(OnDutyButtonClicked);
    }

    private void OnDutyButtonClicked()
    {
        if(!dutyPanel.activeSelf)//�г��� ���� ���� �� ��ư Ŭ�� �� 
        {
            dutyPanel.SetActive(true);//�г� Ȱ��ȭ
            DOTWeenUIAnimation.PopupAnimationInUI(dutyPanel, dutyPanel.transform.localScale * 0.2f, 0.001f, dutyPanel.transform.localScale, 0.2f);
        }
        else//�г��� ���� ���� �� ��ư Ŭ�� ��
        {
            DOTWeenUIAnimation.PopupDownAnimationInUI(dutyPanel, dutyPanel.transform.localScale * 0.2f, 0.2f);
        }
    }
}
