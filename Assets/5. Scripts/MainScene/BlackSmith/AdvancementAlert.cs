using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdvancementAlert : MonoBehaviour
{
    [SerializeField] private GameObject failedPanel; // 승급 실패 패널
    [SerializeField] private TextMeshProUGUI failedMessageText; // 실패 메시지 텍스트
    public void AdvancementFailed(string message)
    {
        failedMessageText.text = message;
        failedPanel.SetActive(true);
        DOTWeenUIAnimation.PopupDownAnimationInUI(failedPanel, failedPanel.transform.localScale, 2.0f); // 패널을 작아지게 하며 비활성화
    }
}
