using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToText : MonoBehaviour
{
    //��ư Ŭ�� �� �ؽ�Ʈ�� ��µǴ� ��ɿ� �������� ����ϴ� ��ũ��Ʈ.
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
