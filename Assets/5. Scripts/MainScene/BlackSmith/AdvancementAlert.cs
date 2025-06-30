using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdvancementAlert : MonoBehaviour
{
    [SerializeField] private GameObject failedPanel; // �±� ���� �г�
    [SerializeField] private TextMeshProUGUI failedMessageText; // ���� �޽��� �ؽ�Ʈ
    public void AdvancementFailed(string message)
    {
        failedMessageText.text = message;
        failedPanel.SetActive(true);
        DOTWeenUIAnimation.PopupDownAnimationInUI(failedPanel, failedPanel.transform.localScale, 2.0f); // �г��� �۾����� �ϸ� ��Ȱ��ȭ
    }
}
