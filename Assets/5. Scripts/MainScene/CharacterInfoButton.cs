using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
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
            DOTWeenUIAnimation.PopupAnimationInUI(characterInfoDetailPanel, 0.8f, 0.2f, 0.6f, 0.2f);
        }
        else
        {

            DOTWeenUIAnimation.PopupDownAnimationInUI(characterInfoDetailPanel, characterInfoDetailPanel.transform.localScale * 0.5f, 0.2f);//끄면 크기가 절반으로 줄어들며 비활성화.
        }
    }
}
