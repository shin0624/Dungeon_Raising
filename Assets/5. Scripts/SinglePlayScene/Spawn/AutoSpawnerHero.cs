using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoSpawnerHero : MonoBehaviour
{
    // SinglePlayScene�� ó�� ���� �� �ڵ����� ���� ������ �����ǵ��� �ϴ� ��ũ��Ʈ.
    // ���̾�� [���� ���̾�, ĳ���� ���̾�, ���� ���̾�] �� 3���� �����ϸ�, SinglePlayScene�� ó�� ���� �� ������ ���̾�� ���ֵ��� �ڵ� �����ȴ�.
    // ���� ��ġ�� ����Ͽ� ������ ��ġ�� �ٲٴ� ����� AutoSpawner__.cs �̿��� �ٸ� ��ũ��Ʈ���� �����Ѵ�.
    // 3���� ���̾�� �� 25ĭ(5*5)���� �����Ǿ� �ְ�, ������ ĳ���Ͱ� ���� 1ĭ��, ����� 23ĭ�� ��ġ�ȴ�.
    
    [SerializeField] private Tilemap heroTilemapLayer;
    [SerializeField] public int maxAmount = 1;//�� ���� singlePlay�� ���Ǵ� ���� ���� 1��. ������ ĭ�� �����ǰ�, UI�� SelectButton���� ��ü�� �� �ִ�.
    [SerializeField] private Transform prefabParent;//������ �������� �ڽ����� �� �θ� ������Ʈ
    [SerializeField] private UnitManager unitManager;
    private List<GameObject> heroList;//�÷��̾ ���� ���� �������� ����Ʈ. ���ʿ� �����Ǵ� ������ ����Ʈ[0]. UI�� SelectButton���� ������ ��� �ٲ� �� �ִ�.
    public GameObject newHero;
    private Vector3Int heroSpawnPosition = new Vector3Int(0,0,0);//���� ������ ���ʿ� ������ Ÿ���� �׸��� ��ǥ.
    private int nowSpawnedHeroCount = 0;//���� ������ ������ ��. �ִ� 1��.
    private Quaternion rotation = Quaternion.Euler(0,-180,0);//�÷��̾����� �������� �⺻������ ������ ���� �����Ƿ�, 180�� ȸ�����Ѽ� ��� ���� �ٶ󺸰� �Ѵ�.
    
    private void OnEnable() 
    {
        heroList = unitManager.GetHeroUnitList();
        FindSpawnPosition();//���� Ȱ��ȭ�� �� ���� ������ ��ġ ã��
    }

    private void Start()//Ȱ��ȭ �ʱ�ȭ�� ������ ������ ��ȯ�Ѵ�.
    {
        SpawnHero();//ĳ���� �������� Ÿ�ϸ� �� ���� ��ġ�� �����Ѵ�.
    }

    private void FindSpawnPosition()//���� �������� Ÿ�ϸ� �� ���� ��ġ�� �����ϴ� �޼���.
    {
        bool founded = false;
        BoundsInt bounds = heroTilemapLayer.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(heroTilemapLayer.HasTile(position))
            {
                heroSpawnPosition = position;
                founded = true;
                break;
            }
        }
        if(!founded)//Ÿ���� ���ٸ� ������.
        {
            Debug.LogWarning("There is no tile to spawn hero.");
        }
    }

    private void SpawnHero()//������ ��ȯ ���� ���θ� üũ�ϰ� ���� ���� Ÿ�Ͽ� ��ȯ�Ѵ�. ���� ������ �޼���.
    {
        if(nowSpawnedHeroCount >= maxAmount)
        {
            Debug.Log("maxAmount of hero is spawned.");
            return;
        }
        if(heroList.Count == 0)
        {
            Debug.LogWarning("There is no hero in the list.");
            return;
        }
        Vector3Int spawnTile = heroSpawnPosition;//FindSpawnPosition()���� ã�� Ÿ�� ��ġ�� �ҷ��´�.
        Vector3 worldPositon = heroTilemapLayer.GetCellCenterWorld(spawnTile);//Ÿ���� �׸��� ��ǥ�� ���� ��ǥ�� ��ȯ�Ѵ�.

        newHero = Instantiate(heroList[0], worldPositon, rotation, prefabParent);//���� ����Ʈ�� ù��° ������ �����Ѵ�.
        nowSpawnedHeroCount++;
    }

    public void ClearSpawnedHero()//��ġ ����� �ٲ� ��, ������ �����Ǿ��� ���� ���� �ν��Ͻ�����  ��� �����Ѵ�.
    {
        Destroy(newHero);
    }

    public int GetSpawnedCount()
    {
        return nowSpawnedHeroCount;
    }

    public int GetMaxAmount()
    {
        return maxAmount;
    }
}
