using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    [SerializeField] private Button button;
    void Start()
    {
        button.onClick.AddListener(TestClick);
    }

    private void TestClick()
    {
        Debug.Log("�׽�Ʈ ��ư Ŭ��");
    }
}
