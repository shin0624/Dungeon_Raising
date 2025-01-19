using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToThroneScene : MonoBehaviour
{
    [SerializeField] private Button throneButton;

    void Start()
    {
        throneButton.onClick.AddListener(ClickThroneButton);   
    }

    private void ClickThroneButton()
    {
        LoadThroneScene();
    }

    private void LoadThroneScene()
    {
        SceneManager.LoadScene("ThroneScene");
    }

}
