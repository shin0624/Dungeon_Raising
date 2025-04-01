using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GoToCurrentFloor : MonoBehaviour//���� �÷��̾��� �� �� ������ �޾ƿ�, �ش��ϴ� ���� Scene���� �̵��ϴ� ��ũ��Ʈ. MainScene�� �����Ա� ��ư�� �ٿ��� ���̰�, SinglePlayScene���� "���� ����" �Ǵ� "���� �Ա�"�� ���ư� ������ ���.
{
    private string[] floors = {"DungeonScene", "10_19Floor", "20_29Floor", "30_39Floor", "40_49Floor", "50Floor"};//�� ���� ���� �̸��� �����ϴ� �迭. 1������ 50�������� ���� �̸��� ����.
    [SerializeField] private Button button;
    void Start()
    {
        button.onClick.AddListener(CheckCurrentFloor);//��ư Ŭ�� �� CheckCurrentFloor �޼��� ȣ��.
    }

    private void CheckCurrentFloor()//�÷��̾��� ���� �� ���� üũ�ϰ�, �ش��ϴ� ������ �̵��ϴ� �޼���.
    {
        int currentFloor = PlayerInfo.Instance.GetPlayerFloor();//�÷��̾��� ���� �� ���� ������.
        switch(currentFloor / 10)//�÷��̾��� �� ���� ���� �̵��� ���� ����.
        {
            case 0:
                LoadCurrentFloorScene(0);//1_9Floor ������ �̵�.
                break;

            case 1:
                LoadCurrentFloorScene(1);//10_19Floor ������ �̵�.
                break;

            case 2:
                LoadCurrentFloorScene(2);//20_29Floor ������ �̵�.
                break;

            case 3:
                LoadCurrentFloorScene(3);//30_39Floor ������ �̵�.
                break;

            case 4:
                LoadCurrentFloorScene(4);//40_49Floor ������ �̵�.
                break;

            case 5 :
                LoadCurrentFloorScene(5);//50Floor ������ �̵�.
                break;

            default:
                Debug.Log("Invalid floor number.");//��ȿ���� ���� �� ���� ��� ����� �α� ���.
                break;
        }
    }

    private void LoadCurrentFloorScene(int index)//�� ���� �ش��ϴ� ���� �ε��ϴ� �޼���.
    {
        if(index < floors.Length)//index�� 0�� ���� ���� �����Ƿ�, floors.Length���� ���� ��쿡�� ���� �ε�.
        {
            Debug.Log($"Loading floor: {floors[index]}");//�ε��� �� ���� ����� �α׷� ���.
            SceneManager.LoadScene(floors[index]);//index�� �ش��ϴ� ���� �ε�.
        }
        else
        {
            Debug.Log("Invalid floor index.");//��ȿ���� ���� �� ���� ��� ����� �α� ���.
        }   
    }
}
