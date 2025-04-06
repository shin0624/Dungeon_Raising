using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleTimerController : MonoBehaviour// ���� ���� �� Ÿ�̸� ����� ����ϴ� ��ũ��Ʈ. 2��(120��)�̳��� Ŭ���� �� �¸�, �ʰ� �� �й�.
{
    private TextMeshProUGUI timerText; // Ÿ�̸Ӹ� ǥ���� TextMeshProUGUI ������Ʈ.
    private float timeLimit = 120f; // Ÿ�̸��� �ð� ���� (�� ����).
    private float timeRemaining; // ���� �ð�.
    private bool isTimerRunning = false; // Ÿ�̸Ӱ� ���� ������ ���θ� ��Ÿ���� ����.

    private void Start()
    {
        if(isTimerRunning)
        {
            isTimerRunning = false;
        }
        timeRemaining = timeLimit; // ���� �ð��� �ð� �������� �ʱ�ȭ.
    }

    private void TimerStart()
    {
        isTimerRunning = true; // Ÿ�̸� ����.
        //DOTWeen�� Ÿ�̸� ����� ����ص� ������ ��?

    }

}
