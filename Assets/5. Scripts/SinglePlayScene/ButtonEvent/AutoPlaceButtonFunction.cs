using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoPlaceButtonFunction : MonoBehaviour
{
     // SinglePlayScene���� �ڵ� ��ġ ��ư Ŭ�� �� ����Ǵ� Ȱ��ȭ�Ǵ� Ŭ����.
     // �ڵ� ��ġ�� ������ �÷��̾� ĳ������ ���� ��������, �� ����(�Ǵ� �� �÷��̾�)�� �󼺿� ���� �ڵ� ��ġ�ȴ�. ����� �÷��̾�� ���� ���� �ڿ� ��ġ�ȴ�.
     // ������ 5*5 �׸��� ���� ��ġ�ȴ�. ������ �÷��̾� ĳ���ʹ� ���� 1ĭ�� ��ġ�Ǹ�, ����� 10ĭ�� ��ġ�ȴ�.
     // ���� ���� : ������, ���Ÿ���, ������
     // �˻� : ������ || �ü� : ���Ÿ��� || ������ : ������
     // ���� Ÿ�� : ������ / ���Ÿ��� / ������
     // ������ > ���������� ���ϰ� ���Ÿ������� ����. || ���Ÿ��� > ���������� ���ϰ� ���������� ���� || ������ > ���Ÿ������� ���ϰ� ���������� ����.
     // �ڵ���ġ������ ���� ��ġ�� ���� ������ ����, �� �� Ÿ�Ϸ� ĳ���� ������ ��ġ�ǰ�, ������ ���·� ������ ���� ���ֵ��� ��ġ�ȴ�.
     // ���� �� ������ ��ġ�� Ÿ�Ͽ��� ���ο� �ִ� ������ ������ ���� ������ �÷��̾� Ÿ�Ͽ� ���� ��� �� ������ ���ο� ��ġ. �� ������ ������ �÷��̾�� ���ٸ�, ������ ���� ���� ������ ���ο� ��ġ. 
     // �ڵ� ��ġ ���� ���� : �ڵ���ġ ���� ��, ���� Ÿ�� ���̾ �����ϴ� ���ֵ��� ���� �ڵ���ġ�� ���̾�� �ű��. ���� �ν��Ͻ��� ������ �� ���� �����ϴ� �� ���� ȿ�� �鿡�� ���� ����.
    [SerializeField] private Tilemap soldierPlaceLayer;
    [SerializeField] private Tilemap heroPlaceLayer;
    [SerializeField] private Tilemap characterPlaceLayer;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private AutoSpawnerSoldier autoSpawnerSoldier;

    //----------------Queues
    private Queue<Vector3Int> availableTilesForSoldiers = new Queue<Vector3Int>();//���� ���ֵ��� �̵� ��ġ�� �� �ִ� Ÿ�ϸ�� ť.
    


    private void Start()
    {
       
    }
//--------------���� ���� �̵� �Լ�----------------------------
    private void FindSoldiersPosition()//���� ���ֵ��� ���� ��Ҹ� ã�´�.
    {
        BoundsInt bounds = soldierPlaceLayer.cellBounds;
        foreach(Vector3Int pos in bounds.allPositionsWithin)//�ڵ���ġ�� ����Ÿ�Ͽ��� ��ġ ������ ��ġ�� ã�´�.
        {
            if(soldierPlaceLayer.HasTile(pos))
            {
                availableTilesForSoldiers.Enqueue(pos);//Ÿ�ϸ�� ť�� ����.
            }
        }
        if(availableTilesForSoldiers.Count == 0 )
        {
            Debug.LogWarning("No Available Tilse found in soldierPlaceLayer");
        }
    }

    private void MoveSoldiers()//���� ���ֵ��� �����Ѵ�.
    {   
        foreach(GameObject unit in autoSpawnerSoldier.spawnedSoldiers)//���� ���̾� ���� ������� ���ο� Ÿ�� ���� �ű��.
        {
            if(availableTilesForSoldiers.Count == 0)
            {
                Debug.LogWarning("Not Enough available tiles for soldiers.");
                break;
            }

            Vector3Int newTilePos = availableTilesForSoldiers.Dequeue();//�ڵ���ġ�� ���� Ÿ���� �����Ͽ� ������.
            Vector3 newWorldPos = soldierPlaceLayer.GetCellCenterWorld(newTilePos);//�ڵ� ��ġ�� Ÿ���� �߾� ��ǥ�� �����´�.

            unit.transform.position = newWorldPos;//���� ���ֵ��� �̵���Ų��.
        }
    }

//-----------------ĳ���� ���� �̵� �Լ�-------------------------









}
