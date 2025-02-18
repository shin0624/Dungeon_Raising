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
        FindSpawnPosition();//시작할 때 스폰 가능한 위치 찾기
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
        BoundsInt bounds = tileMap.cellBounds;//타일맵에서의 BoundsInt는 주로 타일맵 내의 유효한 cell 영역을 나타내는 데 사용. 각 cell은 하나의 tile을 나타낸다.
        foreach(Vector3Int pos in bounds.allPositionsWithin)//타일맵 셀에 존재하는 모든 위치를 꺼내어 반복하면서, 타일이 존재하는 위치를 리스트에 저장,.
        {
            if(tileMap.HasTile(pos))//타일이 존재하는 위치만 저장
            {
                spawnPositions.Add(pos);
            }
        }
    }

    private void SpawnCharacter()
    {
        if(spawnedCharacters.Count >= maxCharacters)
        {
            Debug.Log("최대 소환 수에 도달");
            return;
        }

        if(spawnPositions.Count == 0)
        {
            Debug.Log("소환 가능 위치 없음");
            return;
        }

        Vector3Int spawnTile = spawnPositions[Random.Range(0, spawnPositions.Count)];//캐릭터가 스폰될 타일은 FIndSpawnPosition()에서 리스트에 넣어진 타일 중 랜덤하게 지정
        Vector3 worldPosition = tileMap.GetCellCenterWorld(spawnTile);//해당 타일 중심 위치를 가져와서 그곳에 캐릭터를 배치.

        GameObject newCharacter = Instantiate(characterPrefab, worldPosition, Quaternion.identity);
        spawnedCharacters.Add(newCharacter);
        spawnPositions.Remove(spawnTile);//캐릭터가 위치한 타일은 사용 불가 하게 만든다. -> 캐릭터는 한 칸에 하나만.
    }
}
