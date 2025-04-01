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


}
