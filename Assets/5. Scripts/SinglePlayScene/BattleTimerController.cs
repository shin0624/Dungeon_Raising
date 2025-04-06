using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleTimerController : MonoBehaviour// 게임 진행 시 타이머 기능을 담당하는 스크립트. 2분(120초)이내에 클리어 시 승리, 초과 시 패배.
{
    private TextMeshProUGUI timerText; // 타이머를 표시할 TextMeshProUGUI 컴포넌트.
    private float timeLimit = 120f; // 타이머의 시간 제한 (초 단위).
    private float timeRemaining; // 남은 시간.
    private bool isTimerRunning = false; // 타이머가 실행 중인지 여부를 나타내는 변수.

    private void Start()
    {
        if(isTimerRunning)
        {
            isTimerRunning = false;
        }
        timeRemaining = timeLimit; // 남은 시간을 시간 제한으로 초기화.
    }

    private void TimerStart()
    {
        isTimerRunning = true; // 타이머 실행.
        //DOTWeen의 타이머 기능을 사용해도 괜찮을 듯?

    }

}
