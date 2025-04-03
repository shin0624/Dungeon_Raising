using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
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
            DOTWeenUIAnimation.PopupAnimationInUI(characterInfoDetailPanel, 0.8f, 0.2f, 0.6f, 0.2f);
        }
        else
        {

            DOTWeenUIAnimation.PopupDownAnimationInUI(characterInfoDetailPanel, characterInfoDetailPanel.transform.localScale * 0.5f, 0.2f);//���� ũ�Ⱑ �������� �پ��� ��Ȱ��ȭ.
        }
    }
}
