using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoTo__Scene : MonoBehaviour
{
    //공용 씬 전환 스크립트 -> 버튼 클릭 시 다음 씬 이동 등 간단한 씬 이동에서 공용으로 사용
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
