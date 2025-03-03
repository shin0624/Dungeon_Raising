using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerOnDungeonScene : MonoBehaviour
{
    // Manager��ü�� �Ҵ�� PlayerCharacterManager.cs ���� �÷��̾� ĳ���� ������ ������ �� DungeonScene ���� ���� ������ �÷��̾� ĳ���͸� �����ϴ� Ŭ����.
    // @@@DungeonScene�� SinglePlayScene �ް����� �ʵ��� ����@@@ �� Ŭ������ DungeonScene���� ���� �Ա� Ʈ���Ÿ� ã�� ���� ��ġ�� ���ƴٴ� �� �ִ� �÷��̾ �����ϴ� Ŭ�����̴�.
    [SerializeField] Transform playerSpawnPoint;
    private GameObject playerPrefab;
    private PlayerCharacterManager playerCharacterManager;//Manager ��ü�� �Ҵ�� PlayerCharacterManager.cs ��ũ��Ʈ
    private GameObject newCharacter;

    private void OnEnable()//���� �ε�Ǹ� �÷��̾� ������ �´� ĳ���͸� üũ�Ѵ�.
    {
        GetPlayerCharacter();
    }

    private void Start()//���� ��Ұ� ��� �׷����� �÷��̾� ĳ���͸� �����Ѵ�.
    {
        SpawnPlayerCharacter();
    }

    private void OnDisable()//�� ��Ȱ��ȭ �� �÷��̾� ĳ���͸� �����Ѵ�.
    {
        ResetPlayerCharacter();
    }

    private void GetPlayerCharacter()//�÷��̾��� ������ ������ �´� ĳ���� �������� �������� �޼���.
    {  
        if(playerCharacterManager==null)
        {
            playerCharacterManager = Managers.Instance.GetComponent<PlayerCharacterManager>();
            Debug.Log("PlayerCharacterManager is initialized.");
        }
        playerPrefab = playerCharacterManager.SetCharacterProfile();//������ ������ �´� �÷��̾� ĳ���͸� �����Ѵ�.
        Debug.Log($"playerCharacter is {playerPrefab.name}");

    }

    private void SpawnPlayerCharacter()//�÷��̾� ĳ���͸� ���� ����Ʈ�� �����ϴ� �޼���.
    {
         newCharacter = Instantiate(playerPrefab, playerSpawnPoint);//�÷��̾� ������ ��ġ�ϴ� �÷��̾� ĳ���� �������� ��ȯ�Ѵ�.
         newCharacter.transform.position = playerSpawnPoint.position;//�÷��̾� ĳ������ ��ġ�� ���� ����Ʈ�� �̵���Ų��.
         newCharacter.AddComponent<PlayerMovementTemp>();//�÷��̾� ĳ���Ϳ� �÷��̾� �̵� ��ũ��Ʈ�� �߰��Ѵ�.
         newCharacter.GetComponent<BoxCollider2D>().isTrigger = false;
         newCharacter.GetComponent<UnitMoveController>().enabled = false;
        
         newCharacter.transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);//�÷��̾� ĳ������ ũ�⸦ �����Ѵ�.
    }

    private void ResetPlayerCharacter()//�÷��̾� ĳ���͸� �����ϴ� �޼���.
    {
        Destroy(newCharacter);
    }
}
