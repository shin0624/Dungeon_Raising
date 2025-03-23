using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultControlManager : MonoBehaviour
{
    // resultCanvas와 결과 처리를 제어하는 게이트웨이 클래스.
    [SerializeField] private ResultUIController resultUIController;

    private void Start()//각 이벤트 등록. 리스트 요소는 각 클래스의 Start에서 초기화 되므로, 생명 주기를 Start로 맞춘다.
    {
        UnitStatusVisualizer.OnUnitCountChanged += UpdateUnitCount;
        EnemyStatusVisualizer.OnEnemyCountChanged += UpdateEnemyCount;
    }

    private void OnDisable()//씬 비활성화 시 이벤트 등록 해제.
    {
        UnitStatusVisualizer.OnUnitCountChanged -= UpdateUnitCount;
        EnemyStatusVisualizer.OnEnemyCountChanged -= UpdateEnemyCount;
    }

    private void UpdateUnitCount(int count)// 플레이어 유닛 리스트의 카운트를 업데이트. 카운트가 0이 되면 플레이어 패배.
    {
        if(count == 0)
        {
            resultUIController.PlayerLose();
            Debug.Log("player lose");
        }
    }

    private void UpdateEnemyCount(int count)//에너미 리스트의 카운트를 업데이트. 카운트가 0이 되면 플레이어 승리.
    {
        if(count == 0)
        {
            resultUIController.PlayerWin();
            Debug.Log("player win");
        }
    }
}
