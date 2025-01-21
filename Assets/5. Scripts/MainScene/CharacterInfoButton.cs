using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoButton : MonoBehaviour
{
    //캐릭터 정보창 버튼 컨트롤 클래스
    [SerializeField] private Button characterInfoButton;
    [SerializeField] private GameObject characterInfoDetailPanel;
    void Start()
    {
        characterInfoButton.onClick.AddListener(CharacterButtonClicked);
        characterInfoDetailPanel.SetActive(false);//처음 시작 시 패널은 False
    }
    
    private void CharacterButtonClicked()
    {
        if(!characterInfoDetailPanel.activeSelf)
        {
            characterInfoDetailPanel.SetActive(true);
        }
        else
        {
            characterInfoDetailPanel.SetActive(false);
        }
    }
}
