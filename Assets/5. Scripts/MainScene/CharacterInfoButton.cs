using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        DOTween.Init();
        characterInfoDetailPanel.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
    }
    
    private void CharacterButtonClicked()
    {
        if(!characterInfoDetailPanel.activeSelf)
        {
            characterInfoDetailPanel.SetActive(true);
            StartCoroutine(PopupOpen());
        }
        else
        {
            characterInfoDetailPanel.SetActive(false);
            StartCoroutine(PopupOpen());
        }
    }

    private void PopupAnimation()
    {
        var seq = DOTween.Sequence();
        seq.Append(characterInfoDetailPanel.transform.DOScale(0.8f, 0.2f));
        seq.Append(characterInfoDetailPanel.transform.DOScale(0.6f, 0.2f));
        
    }

    private IEnumerator PopupOpen()
    {        
        PopupAnimation();

        yield return new WaitForSeconds(0.5f);
    }


}
