using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDateTypeSelectButton : MonoBehaviour
{
    //본 스크립트에서 정의된 버튼 메서드들을 통해서 퀘스트의 QuestDateType이 변화한다. (Everyday / Weekend) -> QuestDateType에 따라 퀘스트 패널에 출력되는 리스트가 변화.
    [SerializeField] private Button everydayButton;
    [SerializeField] private Button weekendButton;
    [SerializeField] private Sprite buttonClckedSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject[] panels;

    private QuestDate questDate;
    private void OnEnable()
    {
        questDate = QuestDate.Everyday;//퀘스트 창을 열었을 때 항상 기본은 일일 퀘스트가 홀드다운 되어있다.
        everydayButton.image.sprite = defaultSprite;
        weekendButton.image.sprite = defaultSprite;
    }
    private void Start() 
    {
        everydayButton.onClick.AddListener(OnEverydayButtonClicked);
        weekendButton.onClick.AddListener(OnWeekendButtonClicked);    
    }
    private void OnEverydayButtonClicked()
    {
        if(questDate!=QuestDate.Everyday)
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);
            questDate = QuestDate.Everyday;
            everydayButton.image.sprite = buttonClckedSprite;//클릭된 상태의 스프라이트로 유지
            weekendButton.image.sprite = defaultSprite;
        }
        Debug.Log($"QuestDate : {questDate}");
    }

    private void OnWeekendButtonClicked()
    {
        if(questDate != QuestDate.Weekend)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
            questDate = QuestDate.Weekend;
            weekendButton.image.sprite = buttonClckedSprite;//클릭된 상태의 스프라이트로 유지
            everydayButton.image.sprite = defaultSprite;
        }
        Debug.Log($"QuestDate : {questDate}");
    }
}
