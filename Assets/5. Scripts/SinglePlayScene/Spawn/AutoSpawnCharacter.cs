using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoSpawnCharacter : MonoBehaviour
{
  // SinglePlayScene�� ó�� ���� �� �ڵ����� ĳ���� ������ �����ǵ��� �ϴ� ��ũ��Ʈ.
    // ���̾�� [���� ���̾�, ĳ���� ���̾�, ���� ���̾�] �� 3���� �����ϸ�, SinglePlayScene�� ó�� ���� �� ������ ���̾�� ���ֵ��� �ڵ� �����ȴ�.
    // ���� ��ġ�� ����Ͽ� ������ ��ġ�� �ٲٴ� ����� AutoSpawner__.cs �̿��� �ٸ� ��ũ��Ʈ���� �����Ѵ�.
    // 3���� ���̾�� �� 25ĭ(5*5)���� �����Ǿ� �ְ�, ������ ĳ���Ͱ� ���� 1ĭ��, ����� 23ĭ�� ��ġ�ȴ�.
    
    [SerializeField] private Tilemap characterTilemapLayer;
    [SerializeField] private int maxAmount = 1;//�� ���� singlePlay�� ���Ǵ� ĳ������ ���� 1��. ������ ĭ�� �����ȴ�. ĳ������ ������ �� ������ �����ȴ�.
    [SerializeField] private Transform prefabParent;//������ �������� �ڽ����� �� �θ� ������Ʈ
    [SerializeField] private UnitManager unitManager;
    private GameObject playerCharacter;//���� �÷��̾��� ������ ������ �´� ĳ���� ������. Managers�� ������Ʈ�� PlayerCharacterManager.cs���� �����´�.
    private Vector3Int characterSpawnPosition = new Vector3Int(0,0,0);//ĳ������ ������ ���ʿ� ������ Ÿ���� �׸��� ��ǥ.
    private int nowSpawnedCharacterCount = 0;//���� ������ ĳ������ ��. �ִ� 1��.
    private Quaternion rotation = Quaternion.Euler(0,-180,0);//�÷��̾����� �������� �⺻������ ������ ���� �����Ƿ�, 180�� ȸ�����Ѽ� ��� ���� �ٶ󺸰� �Ѵ�.

    private void OnEnable() 
    {
        playerCharacter = unitManager.GetPlayerCharacter();//�÷��̾��� ������ ������ �´� ĳ���� �������� �����´�.
        FindSpawnPosition();//���� Ȱ��ȭ�� �� ���� ������ ��ġ ã��
    }

    private void Start()//Ȱ��ȭ �ʱ�ȭ�� ������ ĳ���͸� ��ȯ�Ѵ�.
    {
        SpawnPlayerCharacter();//ĳ���� �������� Ÿ�ϸ� �� ĳ���� ��ġ�� �����Ѵ�.
    }


    private void FindSpawnPosition()//ĳ���� �������� ������ Ÿ�ϸ� ��ġ�� Ž���ϴ� �޼���
    {
        BoundsInt bounds = characterTilemapLayer.cellBounds;
        foreach(Vector3Int position in bounds.allPositionsWithin)
        {
            if(characterTilemapLayer.HasTile(position))
            {
                characterSpawnPosition = position;
                break;
            }
        }
        if(characterSpawnPosition == new Vector3Int(0,0,0))//Ÿ���� ���ٸ� ������.
        {
            Debug.LogWarning("There is no tile to spawn Playercharacter.");
        }
    }

    private void SpawnPlayerCharacter()//ĳ�������� ��ȯ ���� ���θ� üũ�ϰ� ���� Ÿ�Ͽ� ��ȯ�Ѵ�. ���� ������ �޼���.
    {
        if(nowSpawnedCharacterCount >= maxAmount)
        {
            Debug.Log("maxAmount of Playercharacter is spawned.");
            return;
        }
        if(playerCharacter == null)
        {
            Debug.LogWarning("PlayerCharacter is not initialized.");
            return;
        }
        Vector3Int spawnTile = characterSpawnPosition;//FindSpawnPosition()���� ã�� Ÿ�� ��ġ�� �ҷ��´�.
        Vector3 worldPositon = characterTilemapLayer.GetCellCenterWorld(spawnTile);//Ÿ���� �׸��� ��ǥ�� ���� ��ǥ�� ��ȯ�Ѵ�.
        
        GameObject newCharacter = Instantiate(playerCharacter, worldPositon, rotation, prefabParent);//�÷��̾� ������ ��ġ�ϴ� �÷��̾� ĳ���� �������� ��ȯ�Ѵ�.
        nowSpawnedCharacterCount++;//������ ĳ���ʹ� 1�� ���̹Ƿ� ī��Ʈ�� �������� �߰� ��ȯ�� �����Ѵ�.
    }
}
