using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class BattleTimerController : MonoBehaviour// ���� ���� �� Ÿ�̸� ����� ����ϴ� ��ũ��Ʈ. 2��(120��)�̳��� Ŭ���� �� �¸�, �ʰ� �� �й�.
{
    [SerializeField] private TextMeshProUGUI timerText; // Ÿ�̸Ӹ� ǥ���� TextMeshProUGUI ������Ʈ.
    [SerializeField] private ResultUIController resultUIController;
    [SerializeField] private float timeLimit = 120.0f; // Ÿ�̸��� �ð� ���� (2��).
    private float timeRemaining; // ���� �ð�.
    private bool isTimerRunning = false; // Ÿ�̸Ӱ� ���� ������ ���θ� ��Ÿ���� ����.
    private Tween timerTween; // DOTween�� Tween ��ü�� ����Ͽ� Ÿ�̸Ӹ� ����.

    private void OnEnable()
    {
        timeRemaining = timeLimit; // ���� �ð��� �ð� �������� �ʱ�ȭ.
        TimerStart(); // Ÿ�̸� ����.
    }

    private void OnDisable()//Ÿ�̸� ���߿� �� ��ȯ�̳� ����� ������ BattleTimerController�� ���ŵȴٸ�, Ÿ�̸Ӱ� ��׶��忡�� ��� ���ư� �� �����Ƿ�, ���� ó�� �ʿ�.
    {
        timerTween?.Kill(); // DOTween�� Ÿ�̸Ӹ� ����. -> ���� Ÿ�̸Ӱ� null�̶�� Kill()�� ȣ������ �ʵ���.
    }

    private void TimerStart()//Ÿ�̸� ���� �޼���. isTimerRunning�� true�� �Ǹ� timeRemaining�� RealTime��ŭ --�ǰ�, 0�� �Ǹ� �й�.
    {
        isTimerRunning = true; // Ÿ�̸� ����.

        DOTWeenTimerFunc();//DOTWeen�� Ÿ�̸� ����� ����Ͽ� Ÿ�̸Ӹ� ����.
    }

    public void DOTWeenTimerFunc()//DOTWeen�� Ÿ�̸� ����� ����ϱ� ���� �޼���. DOTWeen�� Ÿ�̸� ����� ����ϸ� �ڷ�ƾ�� ������� �ʰ� ���� Ÿ�̸Ӹ� ������ �� �ִ�.
    {
        if(isTimerRunning)//Ÿ�̸� ���� �� �÷��װ� true�� ������ �Լ��� ����ǵ���.
        {
            timerTween = DOTween.To(() => timeRemaining, x => timeRemaining = x, 0, timeLimit).
                                                                            SetEase(Ease.Linear).
                                                                            OnUpdate(() => {
                                                                                int minutes = Mathf.FloorToInt(timeRemaining / 60);//���� �ð��� �а� �ʷ� ������ ǥ��.
                                                                                int seconds = Mathf.FloorToInt(timeRemaining % 60);//���� �ð��� �ʷ� ������ ǥ��.
                                                                                timerText.text = $"{minutes:00} : {seconds:00}";//Ÿ�̸� �ؽ�Ʈ ������Ʈ.
                                                                            }).
                                                                            OnComplete( () => {     //remainingTime�� 0���� ���̴� Ÿ�̸� ���. timeLimit�� ���� remainingTime�� 0���� �پ���.
                                                                                isTimerRunning = false;
                                                                                resultUIController.PlayerLose();//Ÿ�̸Ӱ� 0�� �Ǹ� �й�UI ȣ��.
                                                                            });
        }
        else
        {
            timeRemaining = timeLimit;//Ÿ�̸Ӱ� ���� ���� �ƴ� ���� remainingTime�� timeLimit���� �ʱ�ȭ.
        }
    }
}
