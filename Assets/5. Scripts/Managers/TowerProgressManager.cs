using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TowerProgressManager : MonoBehaviour// ���� ���� ���¸� �����ϴ� �Ŵ���. ���� Ŭ���� ���´� ������ ����Ǵ� ���� ���� �����͸� �����ؾ� �ϹǷ�, �̱������� ����.
{
    public static TowerProgressManager Instance {get; private set;}//Ÿ�� ���� �Ŵ����� �ν��Ͻ�. �ٸ� Ŭ�������� ������ �� �ֵ��� public���� ����.

    private TowerProgressData towerProgress = new TowerProgressData();//Ÿ�� ���� ���¸� �����ϴ� ������. TowerProgressData Ŭ������ �ν��Ͻ�.
    
    private Dictionary<int, int> floorDungeonCount = new Dictionary<int, int>{ {1,3}, {10,4}, {20,5}, {30,5}, {40,6}, {50,1}};// �� �� ���� ���� ��Ģ ��ųʸ�. �� ���� ������ ������ �����̴�.

    public List<DungeonInformation> allDungeons;// �� ���� �� ���� SO ����Ʈ.
    [SerializeField] private DungeonDatabase dungeonDatabase;//���� �����ͺ��̽�. ���� ������ ��� �ִ� SO.
    
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;//Ÿ�� ���� �Ŵ����� �ν��Ͻ��� ���� �ν��Ͻ��� ����.
            DontDestroyOnLoad(gameObject);//���� �ٲ� �ı����� �ʵ��� ����. 
        }
        else
        {
            Destroy(gameObject);//Ÿ�� ���� �Ŵ����� �ν��Ͻ��� �̹� �����ϸ� ���� �ν��Ͻ��� �ı�.
        }
    }

    private void Start()
    {
        InputToAllDungeons();//���� DB�� �� �� ����Ʈ ������ ���� �������� ��� allDungeons�� �߰��Ѵ�.
        InitProgress(50, allDungeons);// �� 50���� Ÿ��, �� �ȿ� �ִ� �������� Ŭ���� ���θ� False�� �ʱ�ȭ.
        SceneManager.sceneLoaded+=OnSceneLoaded;//���� �ε�� ������ OnSceneLoaded �޼��带 ȣ��.
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//���� ����� �� ���� ���� Ŭ���� ���� �� ���� �� ��, Ŭ���� ���� ���� ������Ʈ�Ѵ�.
    {
        //PlayerInfo.Instance.SetPlayerFloor();// Ÿ�� �� �� �׽�Ʈ.
    }

    public void SetDungeonClear(int floor, string dungeonID)//Ư�� ���� ���� Ŭ���� ���θ� true�� �����ϴ� �޼���.
    {
        if(!towerProgress.floorProgress.ContainsKey(floor))//floor�� �ش��ϴ� ���� floorProgress ��ųʸ��� ���ٸ� ���ο� DungeonFloorData�� �����Ͽ� �߰�.
        {
            towerProgress.floorProgress[floor] = new DungeonFloorData();
        }
        towerProgress.floorProgress[floor].clearedDungeons[dungeonID] = true;//floor�� Ű�� �ϴ� DungeonFloorDataŬ������ clearedDungeons��ųʸ�����, dungeonID Ű�� �´� ���� true�� ����.
        //��, 1���� 001������ Ŭ����Ǿ��ٰ� �ϸ� SetDungeonClear(1, "001") ȣ�� -> floorProgress�� ��� �� 1���� Ű�� ���� DungeonFloorData�� �̵� -> 001�� Ű�� ���� clearedDungeons�� ���� true�� ����.
    }

    public bool IsDungeonCleared(int floor, string dungeonID)// Ư�� ���� ���� Ŭ���� ���θ� Ȯ���ϴ� �Լ�.
    {
        //��ųʸ��� �ش� floor�� dungeonID�� �����ϴ��� Ȯ�� ��, Ŭ���� ���θ� ��ȯ�Ѵ�.
        return towerProgress.floorProgress.ContainsKey(floor) && //��ü 50�� �����Ȳ ��ųʸ����� �ش� ���� �����ϴ��� ����.
        towerProgress.floorProgress[floor].clearedDungeons.ContainsKey(dungeonID) && //�ش� ���� ���� Ŭ���� ���� ��ųʸ����� dungeonID�� �ش��ϴ� ������ �����ϴ��� ����.
        towerProgress.floorProgress[floor].clearedDungeons[dungeonID];//�ش� ���� dungeonID�� �ش��ϴ� ������ Ŭ���� �Ǿ����� ����.
    }

    public bool IsFloorCleared(int floor, List<string> dungeonIDs)//�ش� ���� ��� ������ Ŭ���� �Ǿ����� Ȯ���ϴ� �޼���.
    {
        if(!towerProgress.floorProgress.ContainsKey(floor))//���� floor���� �رݵ��� �ʾҴٸ� false ����.
        {            
            Debug.Log($"{floor}���� �رݵ��� �ʾҽ��ϴ�.");
            return false;
        }
        
        foreach(var dungeonID in dungeonIDs)//���� id ����Ʈ�� ��ȸ�ϸ�, �� ������ Ŭ���� ���θ� Ȯ��.
        {
            if(!IsDungeonCleared(floor, dungeonID))//floor���� dungeonID�� �ش��ϴ� ������ Ŭ������� �ʾҴٸ� false ����.
            {
                Debug.Log($"{floor}���� {dungeonID} ������ Ŭ������� �ʾҽ��ϴ�.");
                return false;
            }
        }
        return true;//��� ������ Ŭ����Ǿ��ٸ� true ����.
    }

    private void InitProgress(int totalFloors, List<DungeonInformation> allDungeons)// �� ���� ��� ���� Ŭ���� ���¸� �ʱ�ȭ�ϴ� �޼���. �� ���� �����ϴ� ������ ID�� clearedDungeons�� �����ϰ�, �⺻������ false�� ����.
    {
        towerProgress.floorProgress.Clear();//���� ���� ������ �ʱ�ȭ.

        Dictionary<int, List<string>> dungeonData = new Dictionary<int, List<string>>();// �� �� ����ID�� ������ ��ųʸ�. �� ��ȣ�� Ű��, �� ���� ���� �������� id����Ʈ�� ������ ����.

        foreach(DungeonInformation dungeon in allDungeons)//  ���� ������ dungeonData ��ųʸ��� �з��Ѵ�.
        {
            if(!dungeonData.ContainsKey(dungeon.floorNumber))// dungeonData��ųʸ��� floorNumber ���� ���ٸ� (��, �ش� ���� ó�� �����ϸ�)
            {
                dungeonData[dungeon.floorNumber] = new List<string>();// �� ����Ʈ�� ����.
            }
            dungeonData[dungeon.floorNumber].Add(dungeon.dungeonID);//�ش� ���� ���Ե� ������ ID�� ����Ʈ�� �߰��Ѵ�.          
        }

        for(int floor = 1; floor <= totalFloors; floor++)//1������ �ݺ�
        {
            towerProgress.floorProgress[floor] = new DungeonFloorData();// �� ���� ���� Ŭ���� ������ �����ϴ� ��ü�� ����.

            int maxDungeonCount = GetDungeonCountForFloor(floor);// �� ���� ��ġ�� �� �ִ� �ִ� ���� ���� ����.

            if(dungeonData.ContainsKey(floor))//�ش� ���� ������ �����ϴ� ���.
            {
                int dungeonCount = Mathf.Min(dungeonData[floor].Count, maxDungeonCount);//�� ���� �ִ� ���� ���� floorDungeonCount�� ��Ģ�� ���Ͽ� �ִ� ���� ������ ������ ������ ����.
                for(int i = 0; i< dungeonCount; i++)//�ִ� ���� ������ ���� ������ŭ �ݺ��ϸ�
                {
                    towerProgress.floorProgress[floor].clearedDungeons[dungeonData[floor][i]] = false;//���� ID�� clearedDungeons�� �����Ѵ�. �� ������ Ŭ���� ���δ� false.
                }
            }
            else//���� �ش� ���� �ش��ϴ� ����SO�� ���� ���. ��, ���� �� ���� � ������ ���� �� ���� ���� SO�� �������� �ʾҴٸ�
            {
                towerProgress.floorProgress[floor].clearedDungeons = new Dictionary<string, bool>();// �� ������ ���� ���´�. ���� �߰� �����ϵ���.
            }
        }  
    }

    private int GetDungeonCountForFloor(int floor)// ���� ���� �ִ� ������ ������ ��ȯ�ϴ� �޼���. 1��~9���� 3���� ������ �����ϰ�, 10��~29�������� 4��..
    {
        foreach(var entry in floorDungeonCount.OrderByDescending(e => e.Key))//���� �� ���� �� ��Ģ ��ųʸ��� ���� �� Ű(����)�� ������������ ������ �� �̴´�.
        {
            if(floor >= entry.Key) return entry.Value;// floor�� Ÿ�� �� �� => floorDungeonCount�� key�� {50,40,30,20,10,1}������ �񱳵Ǹ�, floor�� �ش��ϴ� value�� "�� ���� ������"�� ��ȯ��.
        } // --> ��, floor = 1 -> floorDungeonCount{ {1, 3} } => 1���� �ִ� ���� �� 3 ��ȯ / floor = 28 -> floorDungeonCount{ {20, 5} } => 28���� �ִ� ���� �� 5�� ��ȯ.

        return 3;//�⺻���� 3(1��)
    }

    private void InputToAllDungeons()//���� DB�� �� �� ����Ʈ ������ ���� �������� ��� allDungeons�� �߰��Ѵ�. ����Ʈ ���� ���ҵ��� Ǯ� �� ����Ʈ�� �ֱ� ���� AddRange�� ����Ͽ���, AddRange�� �Ű������� ���� ����Ʈ���� ���ҵ��� Ÿ�� ����Ʈ�� �ִ� ���̹Ƿ�, allDungeons�� ���Ҵ� ����Ʈ�� �ƴ϶� DungeonInformation��ü���̴�.
    {
        allDungeons = new List<DungeonInformation>();//���� ������ ���� ����Ʈ �ʱ�ȭ.
        allDungeons.AddRange(dungeonDatabase.undergroundDungeonList);//���� ���� ����Ʈ �߰�.
        allDungeons.AddRange(dungeonDatabase.hellDungeonList);//���� ���� ����Ʈ �߰�.
        allDungeons.AddRange(dungeonDatabase.silvanDungeonList);//�ǹ� ���� ����Ʈ �߰�.
        allDungeons.AddRange(dungeonDatabase.KrisosDungeonList);//ũ���ҽ� ���� ����Ʈ �߰�.
        allDungeons.AddRange(dungeonDatabase.skyDungeonList);//õ�� ���� ����Ʈ �߰�.
        allDungeons.AddRange(dungeonDatabase.lastFloorDungeonList);//���� �÷ξ� ���� ����Ʈ �߰�.
    }
    


}

