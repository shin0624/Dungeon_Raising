using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //�÷��̾� ������ ����ȭ�Ǵ� ������ ���� ������Ʈ ����Ʈ�� ������ Ŭ����.
    //������ ĳ����, ����, ���� ������ ������.

    [SerializeField] private HeroDatabase heroDatabase;//���� ������ ������ �����ͺ��̽�.
    [SerializeField] private GameObject soldierPrefab;//���� ���� ������. ���� �÷��̾��� ���� ���� ������ ����ȭ �ʿ�. �ϴ� ����ȭ �Ͽ� �ν����Ϳ��� ���� �Ҵ��Ѵ�.
    private GameObject playerCharacterPrefab;//���� �÷��̾��� ������ ������ �´� ĳ���� ������. Managers�� ������Ʈ�� PlayerCharacterManager.cs���� �����´�.
    private List<GameObject> heroPrefabList = new List<GameObject>();//�÷��̾ ���� ���� �������� ����Ʈ. ���ʿ� �����Ǵ� ������ ����Ʈ[0]. UI�� SelectButton���� ������ ��� �ٲ� �� �ִ�.
    private PlayerCharacterManager playerCharacterManager;//�÷��̾� ĳ���͵��� ����Ʈ�� ����ִ� Ŭ���� �ν��Ͻ�.

    public GameObject GetPlayerCharacter()//�÷��̾��� ������ ������ �´� ĳ���� �������� �������� �޼���.
    {
        if(playerCharacterManager==null)
        {
            playerCharacterManager = Managers.Instance.GetComponent<PlayerCharacterManager>();
            Debug.Log("PlayerCharacterManager is initialized.");
        }
        playerCharacterPrefab = playerCharacterManager.SetCharacterProfile();//������ ������ �´� �÷��̾� ĳ���͸� �����Ѵ�.
        Debug.Log($"playerCharacter is {playerCharacterPrefab.name}");

        return playerCharacterPrefab;//���ӿ�����Ʈ Ÿ������ �����Ͽ�, �÷��̾� ������ ����ȭ�� �÷��̾� ĳ���� �������� �ٸ� ��ũ��Ʈ���� ���� �����ϰ� �Ѵ�.
    }

    public List<GameObject> GetHeroUnitList()//�÷��̾ ������ ���� ������ ���� DB���� ������ �� heroPrefabList�� �����ϴ� �޼���.
    {
        if(heroDatabase!=null)
        {
            //HeroInformationSystem ��ü�� �����ϰ�, �� ��ü���� GameObject������ �̾ƿͼ� heroPrefabList�� �־�� �Ѵ�.
            List<HeroInformationSystem> heroInfo = heroDatabase.heroInformationList.FindAll(i => i.heroRaise.ToString() == PlayerInfo.Instance.GetPlayerRace());//���� DB ���� ���� �� �÷��̾ ������ ������ ���� �̸��� ������ ������ �̾ƿͼ� ����Ʈ�� �����Ѵ�.

            heroPrefabList.Clear();//�ʱ� ����� ������ ����Ʈ �ʱ�ȭ.

            foreach(HeroInformationSystem unit in heroInfo)
            {
                if(unit.heroPrefab!=null)//db���� ������ ���� ���� ������ ����Ʈ�� ��ȸ�ϸ鼭 �������� �ִ� ���� ������ ã�´�. ���� �̸� �غ�� heroPrefabList�� �����Ѵ�.
                {
                    heroPrefabList.Add(unit.heroPrefab);
                    Debug.Log($"{unit.heroName} added to heroPrefabList!");
                }
            }
        }
        return heroPrefabList;// ���ӿ�����Ʈ ����Ʈ Ÿ������ ����Ÿ���� �����Ͽ�, �ϼ��� ���� ������ ����Ʈ�� �ٸ� ��ũ��Ʈ���� ���� �����ϵ��� �Ѵ�.
    } 
}
