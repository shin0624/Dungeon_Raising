using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    //메뉴 버튼을 클릭하면 MenuPivot에 Anchor된 MenuPanel이 활성화됨 -> 메뉴 버튼을 다시 누르면 비활성화됨.
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject menuPanel;
    void Start()
    {
        menuButton.onClick.AddListener(OnMenuButtonClicked);
    }

    private void OnMenuButtonClicked()
    {
        if(!menuPanel.activeSelf)//메뉴 패널이 닫혀 있을 때 메뉴 버튼 클릭 시 
        {
            menuPanel.SetActive(true);//메뉴 패널 활성화
        }
        else//메뉴 패널이 열려 있을 때 메뉴 버튼 클릭 시
        {
            menuPanel.SetActive(false);//메뉴 버튼 비활성화
        }
    }
}
