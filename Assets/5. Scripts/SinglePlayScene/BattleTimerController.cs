using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class BattleTimerController : MonoBehaviour// 게임 진행 시 타이머 기능을 담당하는 스크립트. 2분(120초)이내에 클리어 시 승리, 초과 시 패배.
{
    [SerializeField] private TextMeshProUGUI timerText; // 타이머를 표시할 TextMeshProUGUI 컴포넌트.
    [SerializeField] private ResultUIController resultUIController;
    [SerializeField] private float timeLimit = 120.0f; // 타이머의 시간 제한 (2분).
    private float timeRemaining; // 남은 시간.
    private bool isTimerRunning = false; // 타이머가 실행 중인지 여부를 나타내는 변수.
    private Tween timerTween; // DOTween의 Tween 객체를 사용하여 타이머를 제어.

    private void OnEnable()
    {
        timeRemaining = timeLimit; // 남은 시간을 시간 제한으로 초기화.
        TimerStart(); // 타이머 시작.
    }

    private void OnDisable()//타이머 도중에 씬 전환이나 재시작 등으로 BattleTimerController가 제거된다면, 타이머가 백그라운드에서 계속 돌아갈 수 있으므로, 종료 처리 필요.
    {
        timerTween?.Kill(); // DOTween의 타이머를 종료. -> 만약 타이머가 null이라면 Kill()을 호출하지 않도록.
    }

    private void TimerStart()//타이머 시작 메서드. isTimerRunning이 true가 되면 timeRemaining이 RealTime만큼 --되고, 0이 되면 패배.
    {
        isTimerRunning = true; // 타이머 실행.

        DOTWeenTimerFunc();//DOTWeen의 타이머 기능을 사용하여 타이머를 시작.
    }

    public void DOTWeenTimerFunc()//DOTWeen의 타이머 기능을 사용하기 위한 메서드. DOTWeen의 타이머 기능을 사용하면 코루틴을 사용하지 않고도 쉽게 타이머를 구현할 수 있다.
    {
        if(isTimerRunning)//타이머 실행 중 플래그가 true일 때에만 함수가 실행되도록.
        {
            timerTween = DOTween.To(() => timeRemaining, x => timeRemaining = x, 0, timeLimit).
                                                                            SetEase(Ease.Linear).
                                                                            OnUpdate(() => {
                                                                                int minutes = Mathf.FloorToInt(timeRemaining / 60);//남은 시간을 분과 초로 나누어 표시.
                                                                                int seconds = Mathf.FloorToInt(timeRemaining % 60);//남은 시간을 초로 나누어 표시.
                                                                                timerText.text = $"{minutes:00} : {seconds:00}";//타이머 텍스트 업데이트.
                                                                            }).
                                                                            OnComplete( () => {     //remainingTime을 0까지 줄이는 타이머 기능. timeLimit초 동안 remainingTime이 0으로 줄어든다.
                                                                                isTimerRunning = false;
                                                                                resultUIController.PlayerLose();//타이머가 0이 되면 패배UI 호출.
                                                                            });
        }
        else
        {
            timeRemaining = timeLimit;//타이머가 실행 중이 아닐 때는 remainingTime을 timeLimit으로 초기화.
        }
    }
}
