using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestPanelController : MonoBehaviour
{
    //Script - Utils - QuestSystem�� ���ǵ� ����Ʈ �ý����� MainScene�� QuestPanel - QuestListPanel�� �����ϱ� ���� Ŭ����.
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

        �����ؾ� �� �� : ����Ʈ �̸�(��ǥ), ����Ʈ ���൵, ����/�ְ� ����, �Ϸ�/�̿Ϸ� ����, ���� ��ȭ ���� �� �̹���
    */

    [SerializeField] private TextMeshProUGUI questGoal;//����Ʈ ��ǥ
    [SerializeField] private TextMeshProUGUI progressText;//���൵
    [SerializeField] private TextMeshProUGUI completeText; // ����Ʈ ���൵�� ���� �޶����� �ؽ�Ʈ. Available : "����" / InProgress : "�Ϸ�", ��Ȱ��ȭ / Completed : "�Ϸ�" -> �Ϸ� ��ư Ŭ�� �� ��Ȱ��ȭ.
    [SerializeField] private Button completeButton; //����Ʈ ���൵�� ���� ��ư�� �Ҵ�Ǵ� �̺�Ʈ�� �ٸ��� ����. �ϳ��� ��ư���� ����, �Ϸ� �̺�Ʈ�� �����Ѵ�.
    private QuestDate questDate;//����, �ְ� ����
    private Quest currentQuest;//����Ʈ ��ü

    private void Start() 
    {
        
    }

    private void SetQuest(Quest quest)
    {

    }

    private void OnAvailableButtonClicked()
    {

    }

    private void OnCompleteButtonClicked()
    {
        
    }

    



}
