using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCheckManager : MonoBehaviour
{
    // �÷��̾� �� ĳ���� ���� ���θ� �����ϴ� ��ũ��Ʈ. ĳ���Ͱ� �������� �ʾҴٸ� ĳ���� ���� â�� ���� ����, �̹� ������ ���¶�� ���� â�� ��Ȱ��ȭ�Ѵ�.
    private static Dictionary<string, bool> playerCharacterStatus = new Dictionary<string, bool>();//�÷��̾ ĳ���� ���� ���θ� �����ϴ� ��ųʸ�
    [SerializeField] private GameObject characterSelectPanel;//ĳ���� ����â
    public string playerID;//���� �÷��̾� ���̵�. ����ũ�� ���̸�, ��Ƽ�÷��� ȯ�濡���� ����� ���� ���� photon�� ����ũ �÷��̾� ���̵� ����Ѵ�.

    void Start()
    {
        if(characterSelectPanel==null)
        {
            Debug.LogError("CharacterSelectCanvas is NULL");
        }
        UpdateCanvasState();
    }

    public static void SetCharacterCreated(string playerID, bool isCreated)//�÷��̾��� ĳ���� ���� ���� Ȯ��. playerID : �÷��̾� ID, isCreated : ĳ���� ���� ����
    {
        if(playerCharacterStatus.ContainsKey(playerID))//ĳ���� �������� ��ųʸ��� �÷��̾� ���̵� Ű�� ���� �ִٸ�
        {
            playerCharacterStatus[playerID] = isCreated;//�� �÷��̾� Ű�� �ش��ϴ� ���� true�� ������Ʈ
        }
        else//Ű�� �������� ������(�÷��̾� ���̵� ���ٸ�) ���ο� Ű�� ���� �߰�
        {
            playerCharacterStatus.Add(playerID, isCreated);
        }
    }

    public static bool IsCharacterCreated(string playerID)//�÷��̾� ĳ������ ���� ���θ� �������� �޼���
    {
        return playerCharacterStatus.ContainsKey(playerID) && playerCharacterStatus[playerID];//Ű�� ���� ��� ���� �ִٸ� 1(���� O), 0�̸� ����x
    }

    private void UpdateCanvasState()//ĳ���� ���� â�� ǥ���� �������� �����ϴ� �޼���
    {
        if(string.IsNullOrEmpty(playerID))
        {
            Debug.Log("Player ID is NULL. Setting dummy ID.");
            playerID = PlayerInfo.Instance.PlayerIDCheck(playerID);//�÷��̾� id�� ���ٸ� playerinfo�� ������ ���� id�� �����ͼ� ����
            
        }
        bool isCreated = IsCharacterCreated(playerID);//�÷��̾� ���̵� ���ٸ� 0 �� ĳ���� ���� â�� ����� ��. 

        characterSelectPanel.gameObject.SetActive(!isCreated);//ĳ���� ���� ������ ���� Ȱ��ȭ ���� ����. ĳ���Ͱ� �������� �ʾҴٸ� ĳ���� ���� â�� Ȱ��ȭ�ؼ� ĳ���Ͱ� �����ǵ��� �ؾ� ��. ���� ĳ���Ͱ� �����ǰ� �÷��̾� id�� ��ϵǾ��ٸ� ���� ���Ӻ��ʹ� MainScene���� �ٷ� �̵��ϵ��� ����.
        Debug.Log($"playerID bool : {isCreated}");

    }
}
