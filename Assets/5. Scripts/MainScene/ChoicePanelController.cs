using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelController : MonoBehaviour//�̵��ϰ��� �ϴ� �� ���� �����ϴ� FloorChoicePanel�� ��Ʈ�ѷ�.
{
    // ���� towerFloor���� �����ͼ� UI�� ǥ��("���� ����") 
    // �� ��ư���� LockImage�� ������, ChoicePanel�� ���µ� �� ���� ���� �÷��̾��� Ŭ���� ���θ� üũ�ϰ� LockImage�� ����.
    // LockImage�� ��Ȱ��ȭ�� ��ư�� Ŭ���ϸ� �ش� ������ �̵��Ѵ�. LockImage�� ��ư���� ���� �������Ǳ� ������, Ŭ�������� ���� ���� ��ư�� Ŭ���ص� �ش� ������ �̵����� ����.
    // ����, ��ư�� Ŭ���ϸ� �ش� �� �� ������ Ŭ���� ���θ� üũ�ϰ�, üũ�� ����ؾ� �ش� ������ ���� ����.

    [SerializeField] private TextMeshProUGUI currentFloorText;//���� ������ ǥ���ϴ� TextMeshProUGUI.
    [SerializeField] private Button[] floorButtons;//�� ��ư��. �� ��ư�� LockImage�� �����ϰ� ����.
    [SerializeField] private Image[] lockImages;//�� ��ư�� LockImage��.
    [SerializeField] private GoToCurrentFloor goToCurrentFloor;//���� ������ �̵��ϴ� �޼��尡 �ִ� ��ũ��Ʈ.
    private int currentFloor;//���� ����.

    private void OnEnable()
    {
        currentFloor = PlayerInfo.Instance.GetPlayerFloor();//���� ������ ������.
        currentFloorText.text = $"���� �� �� : {currentFloor}��";//���� ������ �����ͼ� TextMeshProUGUI�� ǥ��.
        AttachFuncToButtons();// �� ��ư�� ������ ���.
                
        if(currentFloor < 10)//1��~9�������� ��Ȱ��ȭ��Ű�� ����.
        {
            return;
        }
        else
        {
            goToCurrentFloor.CheckLockImage(currentFloor, lockImages);//���� ���� Ŭ���� ���θ� Ȯ�� �� LockImage�� ��Ȱ��ȭ.
        }
    }

    private void OnDisable()
    {
        DettachFuncToButtons();//ĵ������ ��Ȱ��ȭ�Ǹ� ��ư ������ ����� ����.
    }

    private void AttachFuncToButtons()// �� ��ư�� Ÿ�� �̵� �޼��带 �����ʷ� ���.
    {
        foreach(Button btn in floorButtons)
        {
            btn.onClick.AddListener(() => goToCurrentFloor.CheckCurrentFloor(currentFloor));//�� ��ư�� Ŭ�� �̺�Ʈ�� �߰�. �Ű������� ���� �� ���� �����Ͽ�, �ش��ϴ� ������ �̵��� �� �ְ� �Ѵ�.
        }
    }

    private void DettachFuncToButtons()
    {
        foreach(Button btn in floorButtons)
        {
            btn.onClick.RemoveAllListeners();//�� ��ư�� �߰��� Ŭ�� �̺�Ʈ�� ����.
        }
    }
    

    

}
