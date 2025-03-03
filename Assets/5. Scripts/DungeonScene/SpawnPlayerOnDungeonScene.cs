using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerOnDungeonScene : MonoBehaviour
{
    // Manager객체에 할당된 PlayerCharacterManager.cs 에서 플레이어 캐릭터 정보를 가져온 후 DungeonScene 에서 조종 가능한 플레이어 캐릭터를 스폰하는 클래스.
    // @@@DungeonScene과 SinglePlayScene 햇갈리지 않도록 주의@@@ 본 클래스는 DungeonScene에서 던전 입구 트리거를 찾기 위해 터치로 돌아다닐 수 있는 플레이어를 스폰하는 클래스이다.
    [SerializeField] Transform playerSpawnPoint;
    private GameObject playerPrefab;
    private PlayerCharacterManager playerCharacterManager;//Manager 객체에 할당된 PlayerCharacterManager.cs 스크립트
    private GameObject newCharacter;

    private void OnEnable()//씬이 로드되면 플레이어 정보에 맞는 캐릭터를 체크한다.
    {
        GetPlayerCharacter();
    }

    private void Start()//씬의 요소가 모두 그려지면 플레이어 캐릭터를 스폰한다.
    {
        SpawnPlayerCharacter();
    }

    private void OnDisable()//씬 비활성화 시 플레이어 캐릭터를 리셋한다.
    {
        ResetPlayerCharacter();
    }

    private void GetPlayerCharacter()//플레이어의 성별과 직업에 맞는 캐릭터 프리팹을 가져오는 메서드.
    {  
        if(playerCharacterManager==null)
        {
            playerCharacterManager = Managers.Instance.GetComponent<PlayerCharacterManager>();
            Debug.Log("PlayerCharacterManager is initialized.");
        }
        playerPrefab = playerCharacterManager.SetCharacterProfile();//성별과 직업에 맞는 플레이어 캐릭터를 설정한다.
        Debug.Log($"playerCharacter is {playerPrefab.name}");

    }

    private void SpawnPlayerCharacter()//플레이어 캐릭터를 스폰 포인트에 스폰하는 메서드.
    {
         newCharacter = Instantiate(playerPrefab, playerSpawnPoint);//플레이어 정보와 일치하는 플레이어 캐릭터 프리팹을 소환한다.
         newCharacter.transform.position = playerSpawnPoint.position;//플레이어 캐릭터의 위치를 스폰 포인트로 이동시킨다.
         newCharacter.AddComponent<PlayerMovementTemp>();//플레이어 캐릭터에 플레이어 이동 스크립트를 추가한다.
         newCharacter.GetComponent<BoxCollider2D>().isTrigger = false;
         newCharacter.GetComponent<UnitMoveController>().enabled = false;
        
         newCharacter.transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);//플레이어 캐릭터의 크기를 조정한다.
    }

    private void ResetPlayerCharacter()//플레이어 캐릭터를 리셋하는 메서드.
    {
        Destroy(newCharacter);
    }
}
