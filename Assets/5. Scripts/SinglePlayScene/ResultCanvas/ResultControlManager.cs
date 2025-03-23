using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResultControlManager : MonoBehaviour
{
    // resultCanvas�� ��� ó���� �����ϴ� ����Ʈ���� Ŭ����.
    [SerializeField] private ResultUIController resultUIController;

    private void Start()//�� �̺�Ʈ ���. ����Ʈ ��Ҵ� �� Ŭ������ Start���� �ʱ�ȭ �ǹǷ�, ���� �ֱ⸦ Start�� �����.
    {
        UnitStatusVisualizer.OnUnitCountChanged += UpdateUnitCount;
        EnemyStatusVisualizer.OnEnemyCountChanged += UpdateEnemyCount;
    }

    private void OnDisable()//�� ��Ȱ��ȭ �� �̺�Ʈ ��� ����.
    {
        UnitStatusVisualizer.OnUnitCountChanged -= UpdateUnitCount;
        EnemyStatusVisualizer.OnEnemyCountChanged -= UpdateEnemyCount;
    }

    private void UpdateUnitCount(int count)// �÷��̾� ���� ����Ʈ�� ī��Ʈ�� ������Ʈ. ī��Ʈ�� 0�� �Ǹ� �÷��̾� �й�.
    {
        if(count == 0)
        {
            resultUIController.PlayerLose();
            Debug.Log("player lose");
        }
    }

    private void UpdateEnemyCount(int count)//���ʹ� ����Ʈ�� ī��Ʈ�� ������Ʈ. ī��Ʈ�� 0�� �Ǹ� �÷��̾� �¸�.
    {
        if(count == 0)
        {
            resultUIController.PlayerWin();
            Debug.Log("player win");
        }
    }
}
