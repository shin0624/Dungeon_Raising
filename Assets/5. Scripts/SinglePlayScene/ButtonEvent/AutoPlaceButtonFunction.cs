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
    [SerializeField] private AutoSpawnCharacter autoSpawnCharacter;
    [SerializeField] private AutoSpawnerHero autoSpawnerHero;
    private Vector3Int characterSpawnPosition = new Vector3Int(0,0,0);//ĳ���� ������ �ڵ���ġ�� ���̾� Ÿ�Ͽ� ������ �׸��� ��ǥ.
    private Vector3Int heroSpawnPosition = new Vector3Int(0,0,0);//���� ������ �ڵ���ġ�� ���̾� Ÿ�Ͽ� ������ �׸��� ��ǥ.
    //----------------Queues
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
        FindUnitsPosition();
        yield return null;
        MoveAllUnits();
    }

    private void FindUnitsPosition()
    {
        FindCharacterPosition();
        FindHeroPosition();
        FindSoldiersPosition();
        Debug.Log("Find Position for AutoPlace Completed.");
    }

    private void MoveAllUnits()
    {
        MoveCharacter();
        MoveHero();
        MoveSoldiers();
        Debug.Log("AutoPlace Completed.");
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
            Debug.LogWarning("No Available Tiles found in soldierPlaceLayer");
        }
    }

    private void MoveSoldiers()//���� ���ֵ��� �����Ѵ�.
    {   
        foreach(GameObject unit in autoSpawnerSoldier.spawnedSoldiers)//���� ���̾� ���� ������� ���ο� Ÿ�� ���� �ű��.
        {
            if(availableTilesForSoldiers.Count == 0)//Ÿ�� ť�� ������� ��츦 ����.
            {
                Debug.LogWarning("Not Enough available tiles for soldiers.");
                return;//break�� ����ϸ� ���� ���� �� �ڵ尡 ��� ����� �״�, Ÿ�� ť�� ������� ���� return���� �Լ��� �����Ų��.
            }

            Vector3Int newTilePos = availableTilesForSoldiers.Dequeue();//�ڵ���ġ�� ���� Ÿ���� �����Ͽ� ������.
            Vector3 newWorldPos = soldierPlaceLayer.GetCellCenterWorld(newTilePos);//�ڵ� ��ġ�� Ÿ���� �߾� ��ǥ�� �����´�.

            unit.transform.position = newWorldPos;//���� ���ֵ��� �̵���Ų��.
        }
    }

//-----------------ĳ���� ���� �̵� �Լ�-------------------------

    private void FindCharacterPosition()//ĳ���� ������ ���� ��Ҹ� ã�´�.
    {
        bool founded = false;//Ÿ���� ���� ��츦 �˸��� �÷���. 
        BoundsInt bounds = characterPlaceLayer.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(characterPlaceLayer.HasTile(position))
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

    private void MoveCharacter()//���� ���̾��� ĳ���� ������ �ڵ���ġ�� ���̾� Ÿ�Ϸ� �̵���Ű�� �Լ�.
    {
        Vector3Int newTilePos = characterSpawnPosition;//FindCharacterPosition()���� ã�� �ڵ���ġ�� Ÿ�� ��ǥ�� ���ο� ��ǥ�� ���.
        Vector3 newWorldPos = characterPlaceLayer.GetCellCenterWorld(newTilePos);//Vector3Int�� �׸�����ǥ�� ���� Ÿ�ϸ� ������ǥ�� ��ȯ.

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

    private void FindHeroPosition()//���� ������ ���� ��Ҹ� ã�´�.
    {
        bool founded = false;
        BoundsInt bounds = heroPlaceLayer.cellBounds;//���� ������ ��ġ�� �ڵ���ġ�� ���̾� ���� ��ġ�� ã�´�.
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(heroPlaceLayer.HasTile(position))
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

    private void MoveHero()//���� ���̾��� ���� ������ �ڵ���ġ�� ���̾� Ÿ�Ϸ� �̵���Ű�� �Լ�.
    {
        Vector3Int newTilePos = heroSpawnPosition;
        Vector3 newWorldPos = heroPlaceLayer.GetCellCenterWorld(newTilePos);
     
        if(autoSpawnerHero.newHero!=null)
        {
            autoSpawnerHero.newHero.transform.position = newWorldPos;
        }
        else
        {
            Debug.LogWarning("autoSpawnerHero.newHero is NULL");
        }
    }

}
