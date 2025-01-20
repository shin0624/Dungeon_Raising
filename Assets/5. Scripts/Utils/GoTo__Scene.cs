using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoTo__Scene : MonoBehaviour
{
    //���� �� ��ȯ ��ũ��Ʈ -> ��ư Ŭ�� �� ���� �� �̵� �� ������ �� �̵����� �������� ���
    [SerializeField] private string nextSceneName = "";
    [SerializeField] private Button button;
    void Start()
    {
        button.onClick.AddListener(GoToScene);
    }

    private void GoToScene()
    {
        SceneManager.LoadScene(nextSceneName);
        Debug.Log($"Move to {nextSceneName}.");
    }
}
