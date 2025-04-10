using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonInfoPrintToPanel : MonoBehaviour
{
    // DungeonScene popupcanvas <-> DungeonInformation 정보를 동기화하는 클래스. DungeonScene에서 Trigger이벤트를 통해 PopupCanvas가 활성화되면, Trigger 이벤트가 발생한 던전입구의 정보가 PopupCanvas에 출력된다.
    //각 던전의 정보는 스크립터블 오브젝트로 관리되며, PopupCanvas에서 전투 시작 버튼을 누르면 해당 던전의 정보와 함께 SinglePlayScene으로 넘어간다.
    //본 클래스는 각 던전 입구 오브젝트에 추가되고, 인스펙터에서 던전 입구 오브젝트에 부합하는 DungeonInformation 스크립터블 오브젝트를 할당할 수 있다.
    //본 클래스의 메서드들은 DungeonEntranceTrigger 클래스에서 호출된다.
    [SerializeField] private TextMeshProUGUI dungeonNameText;
    [SerializeField] private TextMeshProUGUI rewardAmountText01;
    [SerializeField] private TextMeshProUGUI rewardAmountText02;
    [SerializeField] private Image rewardSprite01;
    [SerializeField] private Image rewardSprite02;
    [SerializeField] private DungeonInformation dungeonInformation;//던전 정보 스크립터블 오브젝트
    [SerializeField] private DungeonInfoDeliver dungeonInfoDeliver;

    public void SynchronizeDungeonInfo()//던전 정보를 팝업창에 출력하는 메서드.
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

    public void ClearDungeonInfo()//던전 정보를 초기화하는 메서드. PopupCanvas가 비활성화될 때(전투 시작 버튼을 누르지 않고 팝업을 끄거나, SinglePlayScene으로 진입하여 씬이 전환될 때 호출.)
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
