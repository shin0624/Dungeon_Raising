using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeroPanelSwitchController : MonoBehaviour
{
    // 좌우 버튼을 눌러 히어로 패널을 전환하는 스크립트.
    // HeroPanelLoadController.cs에서 생성된 싱글톤 인스턴스를 통해 영웅 패널 배열과 패널 부모 트랜스폼에 접근한다.
    //패널은 풀링을 사용하여 미리 비활성화 시켜놓고, 버튼 클릭 시 인덱스 계산값에 따라 활성화/비활성화 하며 플레이어에게 보여준다.
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    private GameObject [] panelPool;//패널 오브젝트들은 오브젝트 풀링으로 관리한다.
    private int currentIndex;//현재 인덱스

    private void Start() 
    {
        InitPanelPool();//패널 오브젝트 풀 초기화
        currentIndex = HeroPanelLoadController.instance.selectedHeroIndex;//currentIndex는 HeroPanelLoadController에서 받아온 선택된 영웅 인덱스 값.
        SwitchPanel(currentIndex);//패널 활성화

        leftButton.onClick.AddListener(OnLeftButtonClick);//좌 버튼 클릭 시 이벤트 추가
        rightButton.onClick.AddListener(OnRightButtonClick);//우 버튼 클릭 시 이벤트 추가
    }

    private void InitPanelPool()//패널 오브젝트 풀 초기화
    {
        int panelCount = HeroPanelLoadController.instance.heroPanelPrefabs.Length;//패널 개수는 HeroPanelLoadController의 heroPanelPrefabs 배열 길이와 같다.
        panelPool = new GameObject[panelCount];//패널 개수만큼의 배열 생성.

        for(int i=0; i<panelCount; i++)
        {
            panelPool[i] = Instantiate(HeroPanelLoadController.instance.heroPanelPrefabs[i], HeroPanelLoadController.instance.heroPanelParent);//패널 프리팹을 생성하여 풀에 넣는다. 위치는 HeroPanelParent.
            panelPool[i].SetActive(false);//생성된 패널은 비활성화 상태로 둔다.
        }
    }

    private void SwitchPanel(int index)//패널 활성화 및 이전 패널 비활성화. 이 메서드를 통해, HeroSelectScene 로드 시 이전 씬(ThroneScene)에서 선택된 영웅에 해당하는 패널만 활성화된다. 
    {
        for(int i=0; i<panelPool.Length; i++)
        {
            panelPool[i].SetActive(i==index);//현재 인덱스와 같은 패널만 활성화. 인덱스가 1이면 1번째 패널만 활성화하는 식.
        }
    }

    private void OnLeftButtonClick()//좌 버튼 클릭 시
    {
        currentIndex--; //인덱스 감소
        if(currentIndex < 0)
        {
            currentIndex = panelPool.Length-1;//인덱스가 0보다 작아지면 배열의 마지막 인덱스로 설정. 즉, 순환 가능하도록 설정
        }
        SwitchPanel(currentIndex);//패널 전환
    }

    private void OnRightButtonClick()//우 버튼 클릭 시
    {
        currentIndex++;//인덱스 증가
        if(currentIndex >= panelPool.Length)
        {
            currentIndex = 0;//인덱스가 배열 길이보다 커지면 0으로 설정.
        }
        SwitchPanel(currentIndex);//패널 전환
    }
    

}
