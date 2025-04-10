using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentFloorText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentFloorText; // 현재 층을 표시할 TextMeshProUGUI 컴포넌트.

    void OnEnable()
    {
        currentFloorText.text = PlayerInfo.Instance.GetPlayerFloor().ToString(); // PlayerInfo에서 현재 층 정보를 가져와서 텍스트에 설정.
    }

}
