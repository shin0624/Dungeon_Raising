using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPanelController : MonoBehaviour
{
    // Script - Utils - QuestSystem�� ���ǵ� ����Ʈ �ý����� MainScene�� QuestPanel - QuestListPanel�� �����ϱ� ���� Ŭ����.
    // QuestListPanel�� EverydayQuestList, WeekendQuestList��� ���� �гη� ���еȴ�. �� ����Ʈ�� ���� ��Ҵ� ����.
    /*  ___QuestList ��ü(�θ�) : ����Ʈ â
            - ___QuestElement(Image) : ����Ʈ ��� �� �� ����Ʈ ��ü
                - QuestGoal(TMPro text) : ����Ʈ �̸�(��ǥ)

                - Progress(Image -> �̹��� Ÿ�� Fill)
                    - ProgressText(TMPro text)  : ����Ʈ ���൵( 50/50 ) <-> (currentCount / RequiredCount)

                - RewardImage(Image) : ���� ��ȭ �̹���
                    - RewardAmount(TMPro text) : ���� ��ȭ ���� (int currency)

                - CompleteButton(Button)
                    - CompleteText(TMPro text)

        �����ؾ� �� �� : ����Ʈ �̸�(��ǥ), ����Ʈ ���൵, �Ϸ�/�̿Ϸ� ����, ���� ��ȭ ���� �� �̹���

        --> ��� ����? : ����Ʈ����Ʈ �г� - ���� ����Ʈ ����Ʈ - ���� ����Ʈ ������Ʈ
                                            - �ְ� ����Ʈ ����Ʈ - �ְ� ����Ʈ ������Ʈ
                                            --> ����, �ְ� ��ü ������ ����
                                            --> ��ũ��Ʈ�� ����� ������Ʈ�� ������ȭ�ؼ�, ���Ŀ� ����Ʈ �ڵ� ���� ����
    */
    [SerializeField] private Quest currentQuest;//����Ʈ ��ü(��ũ���ͺ� ������Ʈ �Ҵ�)
    [SerializeField] private TextMeshProUGUI questGoal;// ����Ʈ ��ǥ
    [SerializeField] private TextMeshProUGUI progressText;// ���൵
    [SerializeField] private TextMeshProUGUI interactButtonText; // ����Ʈ ���൵�� ���� �޶����� ��ư�� �ؽ�Ʈ. Available : "����" / InProgress : "������", ��Ȱ��ȭ / Completed : "�Ϸ�" -> �Ϸ� ��ư Ŭ�� �� ��Ȱ��ȭ.
    [SerializeField] private Button interactButton; //����Ʈ ���൵�� ���� ��ư�� �Ҵ�Ǵ� �̺�Ʈ�� �ٸ��� ����. �ϳ��� ��ư���� ����, �Ϸ� �̺�Ʈ�� �����Ѵ�.
    [SerializeField] private Image progressImage;//fillAmount�� ������ ���൵  �̹���

    private void OnEnable()//����Ʈ �г��� Ȱ��ȭ�Ǹ� ����Ʈ ������ ���� 
    {
        SetButtonState();
        SetQuest(currentQuest);
        SetButtonListener();
        
    } 
    private void Update() 
    {
         SetButtonState();
         SetQuest(currentQuest);
         SetButtonListener();
         TempFunc();
    }

    private void TempFunc()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            QuestManager.UpdateQuestProgress(currentQuest, 0, 1);
        }
    }

    private void SetQuest(Quest quest)
    {
        if (quest == null) return;
            questGoal.text = quest.questName ?? "Unknown Quest"; // �⺻�� ����

        questGoal.text = currentQuest.questName;
        progressText.text = $"{currentQuest.objectives[0].currentCount} / {currentQuest.objectives[0].requiredCount}";
        progressImage.fillAmount = currentQuest.objectives[0].currentCount;
    }

    private void SetButtonState()//���� ����Ʈ ���൵�� ���� ��ư�� text�� ��ȣ�ۿ� ���� ���θ� ����.
    {
       switch(currentQuest.status)
       {
        case QuestStatus.Available :
            interactButtonText.text = "����";
            interactButton.interactable = true;
            break;

        case QuestStatus.InProgress :  
            interactButtonText.text = "������";
            interactButton.interactable = false;
            break;

        case QuestStatus.Completed :
            interactButtonText.text = "�Ϸ�";
            interactButton.interactable = true;
            break;

        default : //Ȥ�� �������ͽ� ���� ������ ��� ��ư�� ������ �ʰ� ����. 
            interactButtonText.text = "X";
            interactButton.interactable = false;
            break;
       }
    }

    private void SetButtonListener()
    {
        switch(currentQuest.status)
        {
            case QuestStatus.Available :
                interactButton.onClick.AddListener(OnAvailableButtonClicked);
                break;

            case QuestStatus.InProgress :
                interactButton.onClick.RemoveAllListeners();
                break;

            case QuestStatus.Completed :
                interactButton.onClick.AddListener(OnCompleteButtonClicked);
                break;

            default : //Ȥ�� �������ͽ� ���� ������ ��� ��ư�� ������ �ʰ� ����. 
                interactButton.onClick.RemoveAllListeners();
                break;
        }
    }

    private void OnAvailableButtonClicked()//���� ��ư Ŭ�� ��
    {
        QuestManager.AcceptQuest(currentQuest);//����Ʈ ���� ���� �� ���� ���� ���� ����Ʈ�� �߰��� ��� �����Ǿ� ����.
    }

    private void OnCompleteButtonClicked()//�Ϸ� ��ư Ŭ�� ��
    {
        QuestManager.CompleteQuest(currentQuest);//����Ʈ Ŭ����
    }
}
