using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class HeroPanelLoadController : MonoBehaviour
{
    //GoToHeroSelectWithPanel.cs에서 넘겨받은 영웅 ID를 받아와서 해당하는 패널을 활성화 시키는 역할.

    public static HeroPanelLoadController instance;//싱글톤 패턴을 위한 인스턴스 변수. 영웅 패널 배열을 사용하는 곳에서 이 변수를 통해 접근할 수 있도록 한다.
    public int selectedHeroIndex = 0;//선택된 영웅의 인덱스.(기본값 = 0)
    [SerializeField] public GameObject [] heroPanelPrefabs;//영웅 패널 프리팹 배열(Inspector에서 넣어준다.)
    [SerializeField] public Transform heroPanelParent;//생성된 패널을 넣어줄 부모 오브젝트(HeroSelectScene의 HeroPanelParent 오브젝트를 넣어준다.)
    
    private void Awake() 
    {
        if(instance==null)
        {
            instance = this;//인스턴스가 없을 경우, 자기 자신을 할당.
        }
        else
        {
            Destroy(gameObject);//인스턴스가 이미 존재할 경우, 자기 자신을 삭제.
        }
    }

    private void Start() 
    {
        int heroID = PlayerPrefs.GetInt("HeroID", 1);//GoToHeroSelectWithPanel.cs에서 넘겨받은 영웅 ID를 받아온다. 기본 값은 1로 설정.
        SetHeroIdAndIndex(heroID);//영웅 ID에 맞는 패널을 로드하도록 selectedHeroIndex를 설정한다. 설정된 변수값은 HeroPanelSwitchController에서 사용된다.
    }

    private void SetHeroIdAndIndex(int heroID)//영웅 ID에 맞는 패널을 생성하는 함수.
    {
        if(heroID < 1 || heroID > heroPanelPrefabs.Length)//영웅 패널 배열 길이 이상이거나 1 이하일 경우 오류출력
        {
            Debug.LogError("잘못된 영웅 ID입니다.");
            return;
        }
        selectedHeroIndex = heroID-1;//영웅 선택 인덱스를 저장. 배열은 0부터 시작하므로, 영웅 ID-1을 해준다. 이 값이 HeroSelectScene으로 넘어가야 다음 씬에서 패널 풀 초기화 시 heroID-1에 해당하는 패널은 비활성화 대상에 포함되지 않는다.
        
        DeletePlayerPrefsData();//PlayerPrefs에 저장된 데이터를 삭제한다.
    }

    private void DeletePlayerPrefsData()
    {
        PlayerPrefs.DeleteKey("HeroID");//PlayerPrefs에 저장된 모든 데이터를 삭제. PlayerPrefs는 로컬에 데이터를 저장하는 기능이므로, HeroSelectScene을 나갔다가 다시 들어왔을 때, 이전에 선택한 영웅의 데이터가 남아있는 것을 방지하기 위함.
    }
   
}
