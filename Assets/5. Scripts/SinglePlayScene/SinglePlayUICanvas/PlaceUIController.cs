using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUIController : MonoBehaviour
{
    //SinglePlayScene에 처음 진입 시 SinglePlayUICanvas가 활성화되도록 하는 스크립트. 캔버스에 사용되는 버튼 중 캔버스 비활성화를 담당하는 "배치완료"버튼만 할당함.
    //다른 버튼들은 추후 확장성 및 관리 용이를 위해 다른 스크립트에서 제어한다.
    [SerializeField] private Canvas singleplayUICanvas;
    [SerializeField] private Button placeCompleteButton;//배치 완료 버튼
    [SerializeField] private EnemyStatusVisualizer enemyStatusVisualizer;
    [SerializeField] private UnitStatusVisualizer unitStatusVisualizer;
    [SerializeField] private GameObject timerPanel;//타이머 패널.
    private bool isComleteButtonClicked = false;// 배치 완료가 결정되었음을 알리는 플래그.

    private void Start()
    {
        if(Time.timeScale == 0)//이전 씬에서 게임 일시정지 상태였다면 게임을 재개한다.
        {
            Time.timeScale = 1;
        }

        isComleteButtonClicked = false;
        singleplayUICanvas.gameObject.SetActive(true);//Canvas는 기본적으로 비활성화되어 있으므로 활성화. 
        timerPanel.SetActive(false);//타이머 패널은 비활성화.
        placeCompleteButton.onClick.AddListener(OnPlaceCompeteButtonClicked);//배치 완료 버튼에 이벤트 추가.
    }

    private void OnPlaceCompeteButtonClicked()//배치 완료 버튼 클릭 시 캔버스가 비활성화되고 게임이 시작된다.
    {
        if(singleplayUICanvas.gameObject.activeSelf)
        {
            singleplayUICanvas.gameObject.SetActive(false);
        }
        
        if(!isComleteButtonClicked)
        {
            isComleteButtonClicked = true;//플래그를 true로 바꾸어 다른 스크립트에서 게임 시작 여부를 판단할 수 있게 한다.
            Debug.Log("Game Start!");
            StartUnitFight();
            timerPanel.SetActive(true);//타이머 패널 활성화.
            
        }
    }

    private void StartUnitFight()//플레이어 유닛과 적 유닛의 정보를 가져오고, 각각 전투 실행 메서드를 호출한다.
    {
        List<GameObject>playerUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit_Soldier"))
        {
            GameObject.FindGameObjectWithTag("Unit_Player"),
            GameObject.FindGameObjectWithTag("Unit_Hero")
        };

        List<GameObject>enemyUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit_Enemy"))
        {
            GameObject.FindGameObjectWithTag("Unit_Boss")
        };

        foreach(GameObject unit in playerUnits)
        {
            UnitMoveController unitMoveController = unit.GetComponent<UnitMoveController>();
            if(unitMoveController!=null)
            {
                unitMoveController.StartFight();
                unitStatusVisualizer.StartUpdateUnitStatus();
                
            }
        }

        foreach(GameObject unit in enemyUnits)
        {
            EnemyMoveController enemyMoveController = unit.GetComponent<EnemyMoveController>();
            if(enemyMoveController!=null)
            {
                enemyMoveController.StartFight();
                enemyStatusVisualizer.StartUpdateUnitStatus();
            }
        }
    }


}
