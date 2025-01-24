using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCheckManager : MonoBehaviour
{
    // 플레이어 별 캐릭터 생성 여부를 관리하는 스크립트. 캐릭터가 생성되지 않았다면 캐릭터 선택 창을 먼저 띄우고, 이미 생성된 상태라면 선택 창은 비활성화한다.
    private static Dictionary<string, bool> playerCharacterStatus = new Dictionary<string, bool>();//플레이어별 캐릭터 생성 여부를 관리하는 딕셔너리
    [SerializeField] private GameObject characterSelectPanel;//캐릭터 선택창
    public string playerID;//현재 플레이어 아이디. 유니크한 값이며, 멀티플레이 환경에서의 사용을 위해 추후 photon의 유니크 플레이어 아이디를 사용한다.

    void Start()
    {
        if(characterSelectPanel==null)
        {
            Debug.LogError("CharacterSelectCanvas is NULL");
        }
        UpdateCanvasState();
    }

    public static void SetCharacterCreated(string playerID, bool isCreated)//플레이어의 캐릭터 생성 여부 확인. playerID : 플레이어 ID, isCreated : 캐릭터 생성 여부
    {
        if(playerCharacterStatus.ContainsKey(playerID))//캐릭터 생성유무 딕셔너리가 플레이어 아이디를 키로 갖고 있다면
        {
            playerCharacterStatus[playerID] = isCreated;//이 플레이어 키에 해당하는 값을 true로 업데이트
        }
        else//키가 존재하지 않으면(플레이어 아이디가 없다면) 새로운 키값 쌍을 추가
        {
            playerCharacterStatus.Add(playerID, isCreated);
        }
    }

    public static bool IsCharacterCreated(string playerID)//플레이어 캐릭터의 생성 여부를 가져오는 메서드
    {
        return playerCharacterStatus.ContainsKey(playerID) && playerCharacterStatus[playerID];//키와 값을 모두 갖고 있다면 1(생성 O), 0이면 생성x
    }

    private void UpdateCanvasState()//캐릭터 생성 창을 표시할 것인지를 결정하는 메서드
    {
        if(string.IsNullOrEmpty(playerID))
        {
            Debug.Log("Player ID is NULL. Setting dummy ID.");
            playerID = PlayerInfo.Instance.PlayerIDCheck(playerID);//플레이어 id가 없다면 playerinfo에 설정된 더미 id를 가져와서 설정
            
        }
        bool isCreated = IsCharacterCreated(playerID);//플레이어 아이디가 없다면 0 → 캐릭터 생성 창을 띄워야 함. 

        characterSelectPanel.gameObject.SetActive(!isCreated);//캐릭터 생성 유무에 따라서 활성화 여부 결정. 캐릭터가 생성되지 않았다면 캐릭터 선택 창을 활성화해서 캐릭터가 생성되도록 해야 함. 이후 캐릭터가 생성되고 플레이어 id가 등록되었다면 다음 접속부터는 MainScene으로 바로 이동하도록 설정.
        Debug.Log($"playerID bool : {isCreated}");

    }
}
