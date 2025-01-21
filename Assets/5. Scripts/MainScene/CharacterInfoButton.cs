using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoButton : MonoBehaviour
{
    //ĳ���� ����â ��ư ��Ʈ�� Ŭ����
    [SerializeField] private Button characterInfoButton;
    [SerializeField] private GameObject characterInfoDetailPanel;
    void Start()
    {
        characterInfoButton.onClick.AddListener(CharacterButtonClicked);
        characterInfoDetailPanel.SetActive(false);//ó�� ���� �� �г��� False
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
