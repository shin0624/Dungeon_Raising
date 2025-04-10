using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GoToCurrentFloor : MonoBehaviour//���� �÷��̾��� �� �� ������ �޾ƿ�, �ش��ϴ� ���� Scene���� �̵��ϴ� ��ũ��Ʈ. 
{   // ChoicePanelController.cs���� ȣ��ȴ�.
    private string[] floors = {"DungeonScene", "10_19Floor", "20_29Floor", "30_39Floor", "40_49Floor", "50Floor"};//�� ���� ���� �̸��� �����ϴ� �迭. 1������ 50�������� ���� �̸��� ����.

    public void CheckCurrentFloor(int targetFloor)//�÷��̾��� ���� �� ���� üũ�ϰ�, �ش��ϴ� ������ �̵��ϴ� �޼���. targetFloor�� �����Ϸ��� ���� ��ǥ ��(10,20,30.40,50)
    {
        int playerFloor = PlayerInfo.Instance.GetPlayerFloor();//���� �÷��̾��� �� ���� ������.
        if(targetFloor > playerFloor)
        {
            return;//�÷��̾��� �� ������ ���� ������ �̵��� �� ������ ����.
        }
        int index = targetFloor / 10;
        LoadCurrentFloorScene(index);//�� ���� �ش��ϴ� ���� �ε��ϴ� �޼��� ȣ��.
    }

    private void LoadCurrentFloorScene(int index)//�� ���� �ش��ϴ� ���� �ε��ϴ� �޼���.
    {
        if(index < floors.Length)//index�� 0�� ���� ���� �����Ƿ�, floors.Length���� ���� ��쿡�� ���� �ε�.
        {
            Debug.Log($"Loading floor: {floors[index]}");//�ε��� �� ���� ����� �α׷� ���.
            StartCoroutine(LateLoadScene(index));//LateLoadScene �޼��带 ȣ���Ͽ� ���� �ε�.
        }
        else
        {
            Debug.Log("Invalid floor index.");//��ȿ���� ���� �� ���� ��� ����� �α� ���.
        }   
    }

    private IEnumerator LateLoadScene(int index)//���� �ε��ϴ� �ڷ�ƾ �޼���. �� ��ȯ �� UI �ִϸ��̼��� �����ϱ� ���� �ڷ�ƾ���� �ۼ�.
    {
        DOTWeenUIAnimation.PopupDownAnimationInUI(gameObject, Vector3.zero, 0.2f);//�� ��ȯ �� UI �ִϸ��̼��� ����.
        yield return new WaitForSecondsRealtime(0.15f);//�ִϸ��̼� ���̺��� ��¦ ª�� ��� �� ���� ���� �ε��Ѵ�. �˾��� ���� �� �ٷ� ��Ȱ��ȭ�Ǿ������ ������, LoadScene()�� �������� ���� �� �ֱ� ����.
        
        SceneManager.LoadScene(floors[index]);//index�� �ش��ϴ� ���� �ε�.
    }

    public void UnlockingFloor(int currentFloor, Image[] lockImages)//���� �÷��̾��� �� ���� ����, �ش� ���� ��� Ŭ����Ǿ��ٸ� LockImage�� ��Ȱ��ȭ�ϴ� �޼���.
    {
        int maxIndex = currentFloor / 10;
        for(int i = 0; i <= maxIndex && i < lockImages.Length; i++)
        {
            lockImages[i].gameObject.SetActive(false);//LockImage�� ��Ȱ��ȭ.
            Debug.Log($"Unlocking floor: {i}");//�ش� ���� �ر��ߴٴ� ����� �α� ���. 
        }
    }

    public void ClearProcess(int currentFloor)//WinPanelController.cs���� ȣ��Ǹ�, ���� ������ Ŭ����ó�� �� �� ���� �� Ŭ���� ���θ� üũ�ϴ� �޼���. Ŭ���� ���ο� ���� "���� ����"Ŭ�� �� �̵��ϴ� ���� ����ȴ�.
    {
        //dungeonInfo.floorNumber�� 1���� 6����. currentFloor�� 1���� 50�����̱� ������, �� ������ ������ �ٸ���. ��? => ���� �÷��̾ �÷����ؾ� �ϴ� ���� 50���ε�, ���� ��Ű��ó�� �� �� �� 10���� ������ ǥ���ϱ�� �߱� ����.
        int realCurrentFloor = 0;//���� �� ���� ������ ����. 1������ 50�������� �� ���� ����. currentFloor�� floorNumber�� ������ �ٸ��� ������, floorNumber�� �°� ��ȯ���־�� �Ѵ�.

        switch(currentFloor/10)
        {
            case 0 : 
                realCurrentFloor = 1;//1�� ~ 9��.
                break;
            case 1 : 
                realCurrentFloor = 2;//10�� ~ 19��.
                break;
            case 2 : 
                realCurrentFloor = 3;//20�� ~ 29��.
                break;
            case 3 : 
                realCurrentFloor = 4;//30�� ~ 39��.
                break;
            case 4 : 
                realCurrentFloor = 5;//40�� ~ 49��.
                break;
            case 5 : 
                realCurrentFloor = 6;//50��.
                break;
        }

        List<string> dungeonIDs = TowerProgressManager.Instance.allDungeons.FindAll(dungeonInfo =>dungeonInfo.floorNumber == realCurrentFloor)
                                                                            .ConvertAll(dungeonInfo => dungeonInfo.dungeonID);
        //allDungeon����Ʈ���� ���� ���� �ش��ϴ� DungeonInformation��ü���� �߷�����.
        //�� ��ü�鿡�� dungeonID�� �����Ͽ� ����Ʈ�� ��ȯ�Ѵ�.
        //ConvertAll�� ���ٽ����� �� ����Ʈ�� ���� �� ����. Select()�ε� ������ �� ������, ����Ƽ���� LINQ����� �����ϴ� ���� ���⿡ C#�� ����޼��带 ���.

        SingleDungeonInfoRepository singleDungeonInfoRepository = GameObject.Find("Managers").GetComponent<SingleDungeonInfoRepository>();

        TowerProgressManager.Instance.SetDungeonClear(currentFloor, singleDungeonInfoRepository.dungeonInformation.dungeonID);//���� ����� ����id�� �ش��ϴ� ������ Ŭ���� ���θ� true�� ����.
        
        if(TowerProgressManager.Instance.IsFloorCleared(currentFloor, dungeonIDs))//currentFloor�� �����ϴ� ��������Ʈ�� �ҷ��ͼ� ��� Ŭ����Ǿ����� üũ.
        {
            PlayerInfo.Instance.SetPlayerFloor();//true�̸� ���� �� ���� +1
        }
        else//���� currentFloor�� ��� ������ Ŭ������� �ʾҴٸ�
        {
            Debug.Log($"Number of Remaining dungeon is {dungeonIDs.Count}");
            return;
        }
    }

    public string GoToNextDungeon(int currentFloor)//WinPanelController.cs���� ȣ��� ���� ���� �̵� �޼���.
    {
        int nextFloorSceneNum = PlayerInfo.Instance.GetPlayerFloor() / 10;// ���� �� ���� �����´�.ResultUIController.cs���� �̹� towerFloor�� ���� �۾��� ���Ʊ⿡, Ŭ���� ���ο� ���� ������Ʈ�Ǿ����� ��.
        
        return floors[nextFloorSceneNum];//���� �� �� /10�� �� (ex : 32���̸� 3 ��ȯ -> floors[3] = 30_39Floor��)�� ������
    }
}
