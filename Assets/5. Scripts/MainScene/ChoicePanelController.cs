using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins;
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

        goToCurrentFloor.UnlockingFloor(currentFloor, lockImages);//���� �� �� ���� ������ LockImage�� ��Ȱ��ȭ.
    }

    private void OnDisable()
    {
        DettachFuncToButtons();//ĵ������ ��Ȱ��ȭ�Ǹ� ��ư ������ ����� ����.
    }

    private void AttachFuncToButtons()// �� ��ư�� Ÿ�� �̵� �޼��带 �����ʷ� ���.
    {
        int[] floorEntries = {0, 10, 20, 30, 40, 50};
        for(int i = 0; i < floorButtons.Length; i++)
        {
            int floor = floorEntries[i];//��ư�� �ش��ϴ� �� ���� ������. Ŭ���� ĸ�� ������ ���� ���� ���� ���.
            floorButtons[i].onClick.AddListener(() => goToCurrentFloor.CheckCurrentFloor(floor));//��ư Ŭ�� �� CheckCurrentFloor �޼��带 ȣ���Ͽ� �ش� ������ �̵�.
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
