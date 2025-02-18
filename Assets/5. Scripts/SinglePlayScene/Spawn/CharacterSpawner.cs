using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private int maxCharacters = 30;

    private List<Vector3Int> spawnPositions = new List<Vector3Int>();
    private List<GameObject> spawnedCharacters = new List<GameObject>();
     void Start()
    {
        FindSpawnPosition();//������ �� ���� ������ ��ġ ã��
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCharacter();
        }
    }

    private void FindSpawnPosition()
    {
        BoundsInt bounds = tileMap.cellBounds;//Ÿ�ϸʿ����� BoundsInt�� �ַ� Ÿ�ϸ� ���� ��ȿ�� cell ������ ��Ÿ���� �� ���. �� cell�� �ϳ��� tile�� ��Ÿ����.
        foreach(Vector3Int pos in bounds.allPositionsWithin)//Ÿ�ϸ� ���� �����ϴ� ��� ��ġ�� ������ �ݺ��ϸ鼭, Ÿ���� �����ϴ� ��ġ�� ����Ʈ�� ����,.
        {
            if(tileMap.HasTile(pos))//Ÿ���� �����ϴ� ��ġ�� ����
            {
                spawnPositions.Add(pos);
            }
        }
    }

    private void SpawnCharacter()
    {
        if(spawnedCharacters.Count >= maxCharacters)
        {
            Debug.Log("�ִ� ��ȯ ���� ����");
            return;
        }

        if(spawnPositions.Count == 0)
        {
            Debug.Log("��ȯ ���� ��ġ ����");
            return;
        }

        Vector3Int spawnTile = spawnPositions[Random.Range(0, spawnPositions.Count)];//ĳ���Ͱ� ������ Ÿ���� FIndSpawnPosition()���� ����Ʈ�� �־��� Ÿ�� �� �����ϰ� ����
        Vector3 worldPosition = tileMap.GetCellCenterWorld(spawnTile);//�ش� Ÿ�� �߽� ��ġ�� �����ͼ� �װ��� ĳ���͸� ��ġ.

        GameObject newCharacter = Instantiate(characterPrefab, worldPosition, Quaternion.identity);
        spawnedCharacters.Add(newCharacter);
        spawnPositions.Remove(spawnTile);//ĳ���Ͱ� ��ġ�� Ÿ���� ��� �Ұ� �ϰ� �����. -> ĳ���ʹ� �� ĭ�� �ϳ���.
    }
}
