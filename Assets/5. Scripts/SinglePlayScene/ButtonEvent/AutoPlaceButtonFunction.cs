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
    [SerializeField] private AutoSpawnerSoldier autoSpawnerSoldier;
    [SerializeField] private AutoSpawnCharacter autoSpawnCharacter;
    [SerializeField] private AutoSpawnerHero autoSpawnerHero;
    private Vector3Int characterSpawnPosition = new Vector3Int(0,0,0);//ĳ���� ������ �ڵ���ġ�� ���̾� Ÿ�Ͽ� ������ �׸��� ��ǥ.
    private Vector3Int heroSpawnPosition = new Vector3Int(0,0,0);//���� ������ �ڵ���ġ�� ���̾� Ÿ�Ͽ� ������ �׸��� ��ǥ.
    //----------------Queues------------------------
    private Queue<Vector3Int> availableTilesForSoldiers = new Queue<Vector3Int>();//���� ���ֵ��� �̵� ��ġ�� �� �ִ� Ÿ�ϸ�� ť.
    
    private void Start()//AutoPlaceButtonFunction�� PlaceUILeftPanelController.cs���� OnAutoPlaceButtonClicked()�� ȣ��Ǳ� ������ ������Ʈ ��Ȱ��ȭ �����̸�, OnAutoPlaceButtonClicked()�� ȣ��Ǿ��� �� ����ȴ�.
    {
        StartAutoPlace();
    }
    public void StartAutoPlace()//�ڵ���ġ ��ư�� 2ȸ �̻� Ŭ�� �� ���ֵ��� �ٽ��ѹ� �ڵ���ġ�ȴ�.
    {
        StartCoroutine(AutoPlaceFunction());
    }

    private IEnumerator AutoPlaceFunction()
    {
        FindUnitsPosition(characterPlaceLayer,heroPlaceLayer, soldierPlaceLayer);
        yield return null;
        MoveAllUnits(characterPlaceLayer,heroPlaceLayer, soldierPlaceLayer);
    }

    public void FindUnitsPosition(Tilemap characterTilemap, Tilemap heroTilemap, Tilemap soldierTilemap)
    {
        FindCharacterPosition(characterTilemap);
        FindHeroPosition(heroTilemap);
        FindSoldiersPosition(soldierTilemap);
        Debug.Log("Find Position for AutoPlace Completed.");
    }

    public void MoveAllUnits(Tilemap characterTilemap, Tilemap heroTilemap, Tilemap soldierTilemap)
    {
        MoveCharacter(characterTilemap);
        MoveHero(heroTilemap);
        MoveSoldiers(soldierTilemap);
        Debug.Log("AutoPlace Completed.");
    }



//--------------���� ���� �̵� �Լ�----------------------------
    private void FindSoldiersPosition(Tilemap tilemap)//���� ���ֵ��� ���� ��Ҹ� ã�´�.
    {
        BoundsInt bounds = tilemap.cellBounds;
        foreach(Vector3Int pos in bounds.allPositionsWithin)//�ڵ���ġ�� ����Ÿ�Ͽ��� ��ġ ������ ��ġ�� ã�´�.
        {
            if(tilemap.HasTile(pos))
            {
                availableTilesForSoldiers.Enqueue(pos);//Ÿ�ϸ�� ť�� ����.
                Debug.Log($"availableTilesForSoldiers.Count = {availableTilesForSoldiers.Count}");
            }
        }
        if(availableTilesForSoldiers.Count == 0 )
        {
            Debug.LogWarning("No Available Tiles found in soldierPlaceLayer");
        }
    }

    private void MoveSoldiers(Tilemap tilemap)//���� ���ֵ��� �����Ѵ�.
    {   
        foreach(GameObject unit in autoSpawnerSoldier.spawnedSoldiers)//���� ���̾� ���� ������� ���ο� Ÿ�� ���� �ű��.
        {
            Vector3Int newTilePos = availableTilesForSoldiers.Dequeue();//�ڵ���ġ�� ���� Ÿ���� �����Ͽ� ������.
            Vector3 newWorldPos = tilemap.GetCellCenterWorld(newTilePos);//�ڵ� ��ġ�� Ÿ���� �߾� ��ǥ�� �����´�.
            
            unit.transform.position = newWorldPos;//���� ���ֵ��� �̵���Ų��.
            Debug.Log($"availableTilesForSoldiers.Count = {availableTilesForSoldiers.Count}");
        }
        if(availableTilesForSoldiers.Count == 0)//Ÿ�� ť�� ������� ��츦 ����.
        {
            Debug.LogWarning("Not Enough available tiles for soldiers.");
            return;//break�� ����ϸ� ���� ���� �� �ڵ尡 ��� ����� �״�, Ÿ�� ť�� ������� ���� return���� �Լ��� �����Ų��.
        }
    }

//-----------------ĳ���� ���� �̵� �Լ�-------------------------

    private void FindCharacterPosition(Tilemap tilemap)//ĳ���� ������ ���� ��Ҹ� ã�´�.
    {
        bool founded = false;//Ÿ���� ���� ��츦 �˸��� �÷���. 
        BoundsInt bounds = tilemap.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(tilemap.HasTile(position))
            {
                characterSpawnPosition = position;
                founded = true;
                break;
            }
        }
        if(!founded)//Ÿ���� ���ٸ� ������. ������ characterSpawnPosition = Vector3 (0,0,0)�� ���� �ع����� ������� �ʱ⿡, Ÿ���� ���� ���� ��츦 �����ؾ� ��.
        {
            Debug.LogWarning("There is no tile to spawn Playercharacter.");
        }
    }

    private void MoveCharacter(Tilemap tilemap)//���� ���̾��� ĳ���� ������ �ڵ���ġ�� ���̾� Ÿ�Ϸ� �̵���Ű�� �Լ�.
    {
        Vector3Int newTilePos = characterSpawnPosition;//FindCharacterPosition()���� ã�� �ڵ���ġ�� Ÿ�� ��ǥ�� ���ο� ��ǥ�� ���.
        Vector3 newWorldPos = tilemap.GetCellCenterWorld(newTilePos);//Vector3Int�� �׸�����ǥ�� ���� Ÿ�ϸ� ������ǥ�� ��ȯ.

        if(autoSpawnCharacter.newCharacter!=null)
        {
            autoSpawnCharacter.newCharacter.transform.position = newWorldPos;//ĳ���� ������ �̵�.
        }
        else
        {
            Debug.LogWarning("autoSpawnCharacter.newCharacter is NULL");
        }
    }

    //--------------���� ���� �̵� �Լ�-------------------------

    private void FindHeroPosition(Tilemap tilemap)//���� ������ ���� ��Ҹ� ã�´�.
    {
        bool founded = false;
        BoundsInt bounds = tilemap.cellBounds;//���� ������ ��ġ�� �ڵ���ġ�� ���̾� ���� ��ġ�� ã�´�.
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(tilemap.HasTile(position))
            {
                heroSpawnPosition = position;
                founded = true;
                break;
            }
        }
        if(!founded)//Ÿ���� ���ٸ� ������.
        {
            Debug.LogWarning("There is no tile to spawn Hero.");
        }
    }

    private void MoveHero(Tilemap tilemap)//���� ���̾��� ���� ������ �ڵ���ġ�� ���̾� Ÿ�Ϸ� �̵���Ű�� �Լ�.
    {
        Vector3Int newTilePos = heroSpawnPosition;
        Vector3 newWorldPos = tilemap.GetCellCenterWorld(newTilePos);
     
        if(autoSpawnerHero.newHero!=null)
        {
            autoSpawnerHero.newHero.transform.position = newWorldPos;
        }
        else
        {
            Debug.LogWarning("autoSpawnerHero.newHero is NULL");
        }
    }

    //--------- ��ġ �ʱ�ȭ �� ���� ó�� �����Ǿ��� ��ġ�� ���� �ν��Ͻ��� position�� �ű�� ����, SerializeField ������ �����ߴ� Tilemap ������Ʈ�� AutoSpawn__.cs���� �����ߴ� Tilemap���� �����Ѵ�.
    //�� Ŭ������ ��ӹ��� ResetPlaceButtonFunction.cs����, �� Ŭ������ ĸ��ȭ�� �����ϸ鼭 ���� ��ȭ��ų �� �ִ� Setter�� �߰��Ѵ�.
}
