using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoSpawnerSoldier : MonoBehaviour
{
    // SinglePlayScene�� ó�� ���� �� �ڵ����� ���� ������ �����ǵ��� �ϴ� ��ũ��Ʈ.
    // ���̾�� [���� ���̾�, ĳ���� ���̾�, ���� ���̾�] �� 3���� �����ϸ�, SinglePlayScene�� ó�� ���� �� ������ ���̾�� ���ֵ��� �ڵ� �����ȴ�.
    // ���� ��ġ�� ����Ͽ� ������ ��ġ�� �ٲٴ� ����� AutoSpawner__.cs �̿��� �ٸ� ��ũ��Ʈ���� �����Ѵ�.
    // 3���� ���̾�� �� 25ĭ(5*5)���� �����Ǿ� �ְ�, ������ ĳ���Ͱ� ���� 1ĭ��, ����� 23ĭ�� ��ġ�ȴ�.
    
    [SerializeField] private Tilemap soldierTilemapLayer;
    [SerializeField] public int maxAmount = 10;//�� ���� SinglePlay�� ���Ǵ� �ִ� ���� ���� 10��. 10���� ���縦 23ĭ �� �������� ��ġ.
    [SerializeField] private Transform prefabParent;//������ �������� �ڽ����� �� �θ� ������Ʈ
    [SerializeField] private UnitManager unitManager;
    
    //Vector3Int : Ÿ�ϸ��� Grid��ǥ �ý����� ������� �����ϹǷ�, �������� ��ġ�� �� Ÿ���� ���� ��ǥ((0,0) ��)�� ��Ȯ�ϰ� �����ϱ� ���� Vector3Int�� ���.
    //Vector3�� �Ǽ��� ��ǥ�� ���� �ڿ� f�� �ٿ��� �ϸ� Ÿ�ϸʿ����� �������� ����.
    //Ÿ�ϸʿ��� ��ġ�� ã�� ���� Vector3Int�� ����ϰ�, ���� ������ ���� ��ǥ�� ��ġ�� ���� Vector3�� ���. �ַ� TilemapŬ������ GetCellCenterWorld()�޼��� � Vector3Int ������(Ÿ�ϸ��� ��ǥ)�� �־� ��ȯ�Ѵ�.
    private Queue<Vector3Int> soldierSpawnPositions = new Queue<Vector3Int>();//���簡 ������ Ÿ���� ��ǥ�� ������ ť. ���縦 ������ �� �ش� ������ Ÿ����ǥ�� �����ؾ� ���� ��ǥ �ߺ� ��ȯ�� ������ �� �ֱ⿡, ù��° ��Ҹ� �����ϴ� ���꿡 O(1)�� �ɸ��� Dequeue�� ���� ���� ť ���·� ����. ����Ʈ�� Remove�� O(n)�� �ɸ��Ƿ� ť�� ���°� �� ȿ������ ���̴�.
    private List<GameObject> mySoldiers;
    public List<GameObject> spawnedSoldiers = new List<GameObject>();
    private GameObject newSoldier;
    private int spawnedCount = 0;
    private Quaternion rotation = Quaternion.Euler(0,-180,0);//�÷��̾����� �������� �⺻������ ������ ���� �����Ƿ�, 180�� ȸ�����Ѽ� ��� ���� �ٶ󺸰� �Ѵ�.


    private void OnEnable()
    {
        mySoldiers = unitManager.GetSoliderList();
        FindSpawnPosition();//���� Ȱ��ȭ�� �� ���� ������ ��ġ ã��
    }

    private void Start()
    {
        if(mySoldiers == null || mySoldiers.Count==0)
        {
            Debug.LogError("No Soldiers prefab found in UnitManager");
            return;
        }
        if(soldierSpawnPositions.Count < maxAmount)
        {
            Debug.LogWarning("Not enough spawn Positions available");
        }
        while(spawnedCount < maxAmount && soldierSpawnPositions.Count > 0)
        {
            SpawnSoldier();//���� �������� Ÿ�ϸ� ���� ���� ���ڸ�ŭ �����Ѵ�.
        }
    }

    private void FindSpawnPosition()//���� �������� Ÿ�ϸ� �� ��ġ�� ���� ���ڸ�ŭ �����Ѵ�.
    {
        //BoundsInt�� �ַ� Ÿ�ϸ� ���� ��ȿ�� cell ������ ��Ÿ���� �� ���. �� cell�� �ϳ��� tile�� ��Ÿ����.
        BoundsInt bounds = soldierTilemapLayer.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {   
            if(soldierTilemapLayer.HasTile(position))//Ÿ�ϸ� ���� �����ϴ� ��� ��ġ�� ������ �ݺ��ϸ� Ÿ���� �����ϴ� ��ġ�� ����
            {   
                soldierSpawnPositions.Enqueue(position);//���� ��ȯ�� ���̾�� Ÿ���� �����ϴ� ��ġ�� ��ǥ���� ť�� ����.
                //Debug.Log($"soldierSpawnPositions.Count = {soldierSpawnPositions.Count}");
            }
        }
        if(soldierSpawnPositions.Count == 0)
        {
            Debug.LogWarning("No Available Tiles found in soldierPlaceLayer");
        }
    }

    private void SpawnSoldier()//������ ��ȯ ���� ���θ� üũ�ϰ� ��ȯ ������ Ÿ�Ͽ� ���� �������� Instantiate()�ϴ� �Լ�
    {
        if(spawnedCount >=maxAmount)
        {
            Debug.LogWarning("maxAmount of soldier is spawned.");
            return;
        }
        /*
            ĳ���Ͱ� ������ Ÿ���� FindSpawnPosition()���� ť�� �־��� Ÿ���̸�, ������� ������ ��ġ�ȴ�.
            Dequeue�� ù ��ǥ���� ������ ���� ĳ���Ͱ� ��ġ�� Ÿ���� �����Ͽ� ��� �Ұ��ϰ� ��. (���� Ÿ�Ͽ� ������ �ߺ� ��ȯ�Ǵ� ���� ����)
        */
        if(soldierSpawnPositions.Count > 0)
        {
            Vector3Int spawnTile = soldierSpawnPositions.Dequeue();
            Vector3 worldPosition = soldierTilemapLayer.GetCellCenterWorld(spawnTile);//�ش� Ÿ�� �߽� ��ġ�� �����ͼ� �װ��� ĳ���͸� ��ġ.

            GameObject newSoldier = Instantiate(mySoldiers[0], worldPosition, rotation, prefabParent);//���� ���� ����Ʈ�� ù��° ��Ҹ� ����. 
            spawnedSoldiers.Add(newSoldier);
            Debug.Log($"soldierSpawnPositions.Count = {soldierSpawnPositions.Count}");
            spawnedCount++;
        }
        else
        {
            Debug.LogWarning("No Available Tiles found in soldierPlaceLayer");
        }
    }

    public void ClearSpawnedSoldiers()//��ġ ����� �ٲ� ��, ������ �����Ǿ��� ���� ���� �ν��Ͻ�����  ��� �����Ѵ�.
    {
        foreach(var solider in spawnedSoldiers)
        {
            Destroy(solider);
        }
        spawnedSoldiers.Clear();
        spawnedCount = 0; // ������ ���� �� �ʱ�ȭ
    }

    public int GetSpawnedCount()
    {
        return spawnedCount;
    }

    public int GetMaxAmount()
    {
        return maxAmount;
    }
}
