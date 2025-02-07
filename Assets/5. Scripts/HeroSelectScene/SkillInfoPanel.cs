using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoPanel : MonoBehaviour
{
    [SerializeField] private Button skillButton01;
    [SerializeField] private Button skillButton02;
    [SerializeField] private Button skillButton03;
    [SerializeField] private GameObject panel01;
    [SerializeField] private GameObject panel02;
    [SerializeField] private GameObject panel03;

    private void OnEnable() 
    {
        skillButton01.onClick.AddListener(Button01Clicked);
        skillButton02.onClick.AddListener(Button02Clicked);
        skillButton03.onClick.AddListener(Button03Clicked);

        panel01.SetActive(false);
        panel02.SetActive(false);
        panel03.SetActive(false);

    }
    private void OnDisable() 
    {
        panel01.SetActive(false);
        panel02.SetActive(false);
        panel03.SetActive(false);
        skillButton01.onClick.RemoveAllListeners();
        skillButton02.onClick.RemoveAllListeners();
        skillButton03.onClick.RemoveAllListeners();
    }

    private void Button01Clicked()
    {
        if(!panel01.activeSelf)
        {
            panel01.SetActive(true);
        }
        else if(panel01.activeSelf)
        {
            panel01.SetActive(false);
        }
        panel02.SetActive(false);
        panel03.SetActive(false);
    }
    private void Button02Clicked()
    {   
        if(!panel02.activeSelf)
        {
            panel02.SetActive(true);
        }
        else if(panel02.activeSelf)
        {
            panel02.SetActive(false);
        }
        panel01.SetActive(false);
        panel03.SetActive(false);
    }
    private void Button03Clicked()
    {
        if(!panel03.activeSelf)
        {
            panel03.SetActive(true);
        }
        else if(panel03.activeSelf)
        {
            panel03.SetActive(false);
        }
        panel01.SetActive(false);
        panel02.SetActive(false);
    }
}
