using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerProgressManager : MonoBehaviour// ���� ���� ���¸� �����ϴ� �Ŵ���. ���� Ŭ���� ���´� ������ ����Ǵ� ���� ���� �����͸� �����ؾ� �ϹǷ�, �̱������� ����.
{
    public static TowerProgressManager Instance {get; private set;}//Ÿ�� ���� �Ŵ����� �ν��Ͻ�. �ٸ� Ŭ�������� ������ �� �ֵ��� public���� ����.

    private TowerProgressData towerProgress = new TowerProgressData();//Ÿ�� ���� ���¸� �����ϴ� ������. TowerProgressData Ŭ������ �ν��Ͻ�.
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


}
