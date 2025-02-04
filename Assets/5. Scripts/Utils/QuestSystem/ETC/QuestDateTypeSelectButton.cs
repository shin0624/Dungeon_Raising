using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDateTypeSelectButton : MonoBehaviour
{
    //�� ��ũ��Ʈ���� ���ǵ� ��ư �޼������ ���ؼ� ����Ʈ�� QuestDateType�� ��ȭ�Ѵ�. (Everyday / Weekend) -> QuestDateType�� ���� ����Ʈ �гο� ��µǴ� ����Ʈ�� ��ȭ.
    [SerializeField] private Button everydayButton;
    [SerializeField] private Button weekendButton;
    [SerializeField] private Sprite buttonClckedSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject[] panels;

    private QuestDate questDate;
    private void OnEnable()
    {
        questDate = QuestDate.Everyday;//����Ʈ â�� ������ �� �׻� �⺻�� ���� ����Ʈ�� Ȧ��ٿ� �Ǿ��ִ�.
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
            everydayButton.image.sprite = buttonClckedSprite;//Ŭ���� ������ ��������Ʈ�� ����
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
            weekendButton.image.sprite = buttonClckedSprite;//Ŭ���� ������ ��������Ʈ�� ����
            everydayButton.image.sprite = defaultSprite;
        }
        Debug.Log($"QuestDate : {questDate}");
    }
}
