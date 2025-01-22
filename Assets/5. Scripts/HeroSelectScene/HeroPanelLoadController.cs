using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class HeroPanelLoadController : MonoBehaviour
{
    //GoToHeroSelectWithPanel.cs���� �Ѱܹ��� ���� ID�� �޾ƿͼ� �ش��ϴ� �г��� Ȱ��ȭ ��Ű�� ����.

    public static HeroPanelLoadController instance;//�̱��� ������ ���� �ν��Ͻ� ����. ���� �г� �迭�� ����ϴ� ������ �� ������ ���� ������ �� �ֵ��� �Ѵ�.
    public int selectedHeroIndex = 0;//���õ� ������ �ε���.(�⺻�� = 0)
    [SerializeField] public GameObject [] heroPanelPrefabs;//���� �г� ������ �迭(Inspector���� �־��ش�.)
    [SerializeField] public Transform heroPanelParent;//������ �г��� �־��� �θ� ������Ʈ(HeroSelectScene�� HeroPanelParent ������Ʈ�� �־��ش�.)
    
    private void Awake() 
    {
        if(instance==null)
        {
            instance = this;//�ν��Ͻ��� ���� ���, �ڱ� �ڽ��� �Ҵ�.
        }
        else
        {
            Destroy(gameObject);//�ν��Ͻ��� �̹� ������ ���, �ڱ� �ڽ��� ����.
        }
    }

    private void Start() 
    {
        int heroID = PlayerPrefs.GetInt("HeroID", 1);//GoToHeroSelectWithPanel.cs���� �Ѱܹ��� ���� ID�� �޾ƿ´�. �⺻ ���� 1�� ����.
        SetHeroIdAndIndex(heroID);//���� ID�� �´� �г��� �ε��ϵ��� selectedHeroIndex�� �����Ѵ�. ������ �������� HeroPanelSwitchController���� ���ȴ�.
    }

    private void SetHeroIdAndIndex(int heroID)//���� ID�� �´� �г��� �����ϴ� �Լ�.
    {
        if(heroID < 1 || heroID > heroPanelPrefabs.Length)//���� �г� �迭 ���� �̻��̰ų� 1 ������ ��� �������
        {
            Debug.LogError("�߸��� ���� ID�Դϴ�.");
            return;
        }
        selectedHeroIndex = heroID-1;//���� ���� �ε����� ����. �迭�� 0���� �����ϹǷ�, ���� ID-1�� ���ش�. �� ���� HeroSelectScene���� �Ѿ�� ���� ������ �г� Ǯ �ʱ�ȭ �� heroID-1�� �ش��ϴ� �г��� ��Ȱ��ȭ ��� ���Ե��� �ʴ´�.
        
        DeletePlayerPrefsData();//PlayerPrefs�� ����� �����͸� �����Ѵ�.
    }

    private void DeletePlayerPrefsData()
    {
        PlayerPrefs.DeleteKey("HeroID");//PlayerPrefs�� ����� ��� �����͸� ����. PlayerPrefs�� ���ÿ� �����͸� �����ϴ� ����̹Ƿ�, HeroSelectScene�� �����ٰ� �ٽ� ������ ��, ������ ������ ������ �����Ͱ� �����ִ� ���� �����ϱ� ����.
    }
   
}
