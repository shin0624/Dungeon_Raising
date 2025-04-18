using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Plugins;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoicePanelController : MonoBehaviour//이동하고자 하는 층 수를 선택하는 FloorChoicePanel의 컨트롤러.
{
    // 현재 towerFloor값을 가져와서 UI에 표시("현재 층수") 
    // 각 버튼에는 LockImage가 있으니, ChoicePanel이 오픈될 때 마다 현재 플레이어의 클리어 여부를 체크하고 LockImage를 제어.
    // LockImage가 비활성화된 버튼을 클릭하면 해당 층으로 이동한다. LockImage는 버튼보다 위에 렌더링되기 때문에, 클리어하지 않은 층의 버튼을 클릭해도 해당 층으로 이동하지 않음.
    // 또한, 버튼을 클릭하면 해당 층 및 던전의 클리어 여부를 체크하고, 체크에 통과해야 해당 층으로 진입 가능.

    [SerializeField] private TextMeshProUGUI currentFloorText;//현재 층수를 표시하는 TextMeshProUGUI.
    [SerializeField] private Button[] floorButtons;//층 버튼들. 각 버튼은 LockImage를 포함하고 있음.
    [SerializeField] private Image[] lockImages;//층 버튼의 LockImage들.
    [SerializeField] private GoToCurrentFloor goToCurrentFloor;//현재 층으로 이동하는 메서드가 있는 스크립트.
    private int currentFloor;//현재 층수.

    private void OnEnable()
    {
        currentFloor = PlayerInfo.Instance.GetPlayerFloor();//현재 층수를 가져옴.
        currentFloorText.text = $"현재 층 수 : {currentFloor}층";//현재 층수를 가져와서 TextMeshProUGUI에 표시.
        AttachFuncToButtons();// 각 버튼에 리스너 등록.

        goToCurrentFloor.UnlockingFloor(currentFloor, lockImages);//현재 층 및 이전 층들의 LockImage를 비활성화.
    }

    private void OnDisable()
    {
        DettachFuncToButtons();//캔버스가 비활성화되면 버튼 리스너 등록을 해제.
    }

    private void AttachFuncToButtons()// 각 버튼에 타워 이동 메서드를 리스너로 등록.
    {
        int[] floorEntries = {0, 10, 20, 30, 40, 50};
        for(int i = 0; i < floorButtons.Length; i++)
        {
            int floor = floorEntries[i];//버튼에 해당하는 층 수를 가져옴. 클로저 캡쳐 방지를 위해 로컬 변수 사용.
            floorButtons[i].onClick.AddListener(() => goToCurrentFloor.CheckCurrentFloor(floor));//버튼 클릭 시 CheckCurrentFloor 메서드를 호출하여 해당 층으로 이동.
        }
    }

    private void DettachFuncToButtons()
    {
        foreach(Button btn in floorButtons)
        {
            btn.onClick.RemoveAllListeners();//각 버튼에 추가된 클릭 이벤트를 제거.
        }
    }
    

    

}
