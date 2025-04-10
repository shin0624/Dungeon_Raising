using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentFloorText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentFloorText; // ���� ���� ǥ���� TextMeshProUGUI ������Ʈ.

    void OnEnable()
    {
        currentFloorText.text = PlayerInfo.Instance.GetPlayerFloor().ToString(); // PlayerInfo���� ���� �� ������ �����ͼ� �ؽ�Ʈ�� ����.
    }

}
