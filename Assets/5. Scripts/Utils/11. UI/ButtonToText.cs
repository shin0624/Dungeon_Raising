using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToText : MonoBehaviour
{
    //버튼 클릭 시 텍스트가 출력되는 기능에 공용으로 사용하는 스크립트.
    [SerializeField] private Button button;
    [SerializeField] private Image textImage;
    void Start()
    {
        textImage.gameObject.SetActive(false);
        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        StartCoroutine(ButtonClickedDelayAction());
    }

    private IEnumerator ButtonClickedDelayAction()
    {
        textImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        textImage.gameObject.SetActive(false);
    }
}
