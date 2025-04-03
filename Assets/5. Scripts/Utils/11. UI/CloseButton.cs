using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject targetPanel;
    void Start()
    {
        closeButton.onClick.AddListener(CloseButtonClicked);
    }

    private void CloseButtonClicked()
    {
        if(targetPanel.activeSelf)
        {
            //targetPanel.SetActive(false);
            DOTWeenUIAnimation.PopupDownAnimationInUI(targetPanel, Vector3.zero, 0.2f);
        }
        else
        {
            return;
        }
    }
}
