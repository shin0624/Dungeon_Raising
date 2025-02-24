using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal.Commands;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //플레이어 정보와 동기화되는 유닛의 게임 오브젝트 리스트를 정의한 클래스.
    //유닛은 캐릭터, 영웅, 병사 유닛이 존재함.

    [Header("Database")]
    [SerializeField] private HeroDatabase heroDatabase;//영웅 정보를 저장할 데이터베이스.
    [SerializeField] private SoldierDatabase soldierDatabase;//병사 정보를 저장할 데이터베이스.

    private GameObject playerCharacterPrefab;//현재 플레이어의 성별과 직업에 맞는 캐릭터 프리팹. Managers의 컴포넌트인 PlayerCharacterManager.cs에서 가져온다.
    private List<GameObject> heroPrefabList = new List<GameObject>();//플레이어가 보유 중인 영웅들의 리스트. 최초에 스폰되는 영웅은 리스트[0]. UI의 SelectButton으로 영웅을 골라 바꿀 수 있다.
    private List<GameObject> soldiersPrefabList = new List<GameObject>();//플레이어가 보유 중인 병사 리스트.
    private PlayerCharacterManager playerCharacterManager;//플레이어 캐릭터들의 리스트가 들어있는 클래스 인스턴스.

    public GameObject GetPlayerCharacter()//플레이어의 성별과 직업에 맞는 캐릭터 프리팹을 가져오는 메서드.
    {
        if(playerCharacterManager==null)
        {
            playerCharacterManager = Managers.Instance.GetComponent<PlayerCharacterManager>();
            Debug.Log("PlayerCharacterManager is initialized.");
        }
        playerCharacterPrefab = playerCharacterManager.SetCharacterProfile();//성별과 직업에 맞는 플레이어 캐릭터를 설정한다.
        Debug.Log($"playerCharacter is {playerCharacterPrefab.name}");

        return playerCharacterPrefab;//게임오브젝트 타입으로 리턴하여, 플레이어 정보와 동기화된 플레이어 캐릭터 프리팹을 다른 스크립트에서 참조 가능하게 한다.
    }

    public List<GameObject> GetHeroUnitList()//플레이어가 소유한 영웅 정보를 영웅 DB에서 꺼내온 후 heroPrefabList에 삽입하는 메서드.
    {
        if(heroDatabase!=null)
        {
            //HeroInformationSystem 객체를 선언하고, 이 객체에서 GameObject성분을 뽑아와서 heroPrefabList에 넣어야 한다.
            List<HeroInformationSystem> heroInfo = heroDatabase.heroInformationList.FindAll(i => i.heroRaise.ToString() == PlayerInfo.Instance.GetPlayerRace());//영웅 DB 내의 종족 중 플레이어가 선택한 종족과 같은 이름의 종족들 정보를 뽑아와서 리스트에 저장한다.
            if(heroInfo==null)
            {
                Debug.LogError("No heroes corresponded to the selected race.");
            }
            heroPrefabList.Clear();//초기 히어로 프리팹 리스트 초기화.

            foreach(HeroInformationSystem unit in heroInfo)
            {
                if(unit.heroPrefab!=null)//db에서 꺼내온 영웅 정보 데이터 리스트를 순회하면서 프리팹이 있는 영웅 정보를 찾는다. 이후 미리 준비된 heroPrefabList에 삽입한다.
                {
                    heroPrefabList.Add(unit.heroPrefab);
                    Debug.Log($"{unit.heroName} added to heroPrefabList!");
                }
            }
        }
        else
        {
            Debug.LogWarning("HeroDatabase is Null");
        }
        return heroPrefabList;// 게임오브젝트 리스트 타입으로 리턴타입을 지정하여, 완성된 영웅 프리팹 리스트를 다른 스크립트에서 쉽게 참조하도록 한다.
    } 

    public List<GameObject> GetSoliderList()//soldierInformation의 GameObject타입 프리팹을 soldierPrefabList에 넣고 차례로 스폰하는 메서드.
    {
        if(soldierDatabase!=null)
        {
            List<SoldierInformation> soldierInfo = soldierDatabase.soldierInformationList.FindAll(i => i.soldierPrefab!=null);//병사 프리팹이 존재하는 병사정보를 찾아서 리스트에 저장.
            soldiersPrefabList.Clear();

            foreach(SoldierInformation unit in soldierInfo)
            {
                soldiersPrefabList.Add(unit.soldierPrefab);//찾은 병사 프리팹을 리스트에 저장.
                Debug.Log($"{unit.soldierType} added to soldierPrefabList!");
            }
        }
        else
        {
            Debug.LogWarning("SoldierDatabase is Null.");
        }
        return soldiersPrefabList;
    }
}
