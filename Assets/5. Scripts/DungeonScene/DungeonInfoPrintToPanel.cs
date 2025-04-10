using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonInfoPrintToPanel : MonoBehaviour
{
    // DungeonScene popupcanvas <-> DungeonInformation ������ ����ȭ�ϴ� Ŭ����. DungeonScene���� Trigger�̺�Ʈ�� ���� PopupCanvas�� Ȱ��ȭ�Ǹ�, Trigger �̺�Ʈ�� �߻��� �����Ա��� ������ PopupCanvas�� ��µȴ�.
    //�� ������ ������ ��ũ���ͺ� ������Ʈ�� �����Ǹ�, PopupCanvas���� ���� ���� ��ư�� ������ �ش� ������ ������ �Բ� SinglePlayScene���� �Ѿ��.
    //�� Ŭ������ �� ���� �Ա� ������Ʈ�� �߰��ǰ�, �ν����Ϳ��� ���� �Ա� ������Ʈ�� �����ϴ� DungeonInformation ��ũ���ͺ� ������Ʈ�� �Ҵ��� �� �ִ�.
    //�� Ŭ������ �޼������ DungeonEntranceTrigger Ŭ�������� ȣ��ȴ�.
    [SerializeField] private TextMeshProUGUI dungeonNameText;
    [SerializeField] private TextMeshProUGUI rewardAmountText01;
    [SerializeField] private TextMeshProUGUI rewardAmountText02;
    [SerializeField] private Image rewardSprite01;
    [SerializeField] private Image rewardSprite02;
    [SerializeField] private DungeonInformation dungeonInformation;//���� ���� ��ũ���ͺ� ������Ʈ
    [SerializeField] private DungeonInfoDeliver dungeonInfoDeliver;

    public void SynchronizeDungeonInfo()//���� ������ �˾�â�� ����ϴ� �޼���.
    {
        dungeonNameText.text = dungeonInformation.dungeonName;
        rewardAmountText01.text = dungeonInformation.firstRewardAmount.ToString();
        rewardAmountText02.text = dungeonInformation.secondRewardAmount.ToString();
        rewardSprite01.sprite = dungeonInformation.firstRewardSprite;
        rewardSprite02.sprite = dungeonInformation.secondRewardSprite;

        dungeonInfoDeliver.SetEnemyInfo(dungeonInformation);
        dungeonInfoDeliver.SetBossInfo(dungeonInformation);
        dungeonInfoDeliver.SetDungeonInfo(dungeonInformation);
    }

    public void ClearDungeonInfo()//���� ������ �ʱ�ȭ�ϴ� �޼���. PopupCanvas�� ��Ȱ��ȭ�� ��(���� ���� ��ư�� ������ �ʰ� �˾��� ���ų�, SinglePlayScene���� �����Ͽ� ���� ��ȯ�� �� ȣ��.)
    {
        dungeonNameText.text = "";
        rewardAmountText01.text = "";
        rewardAmountText02.text = "";
        rewardSprite01.sprite = null;
        rewardSprite02.sprite = null;

        dungeonInfoDeliver.ClearEnemyInfo();
        dungeonInfoDeliver.ClearBossInfo();
        dungeonInfoDeliver.ClearDungeonInfo();
    }
}
