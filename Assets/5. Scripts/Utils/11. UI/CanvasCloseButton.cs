using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCloseButton : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Canvas popupCanvas;
    [SerializeField] private GameObject targetPanel;
    void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        if(popupCanvas.gameObject.activeSelf)
        {
            popupCanvas.gameObject.SetActive(false);
            if(Time.timeScale==0)
            {
                Time.timeScale = 1;
                DOTWeenUIAnimation.PopupDownAnimationInUI(targetPanel, Vector3.zero, 0.2f);
            }
        }
        else
        {
            return;
        }
    }
}
