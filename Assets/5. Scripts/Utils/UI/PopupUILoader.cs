using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupUILoader : MonoBehaviour
{
    //팝업 패널에서 공용으로 쓸 수 있는 팝업UI로더 클래스. 팝업은 버튼으로 활성화되고, 팝업 내 Cloase버튼으로 비활성화된다. 또한 팝업이 활성화된 상태에서는 팝업 뒤의 버튼은 작동하지 않아야 한다.
    //아래 깔린 다른 UI들의 이벤트를 막기 위해서, 팝업 뒤에 Blocker라는 투명한 이미지를 팝업 뒤에 붙여놓는다. 이 Blocker가 아래 깔린 UI들 대신 Raycast를 받게 해서 의도치않은 클릭 이벤트를 방지한다. 가장 쉬운 방법임.
    //Blocker를 팝업 패널의 가장 선두에 있는 자식으로 순서를 설정해 주어야, 가장 먼저 렌더링되고 가장 마지막에 그려지므로, Raycast를 먼저 받을 수 있다.
    [SerializeField] private Button popupButton;
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Image blocker;//메인 Panel - PopUpPanelAnchor의 첫번째 자식인 Blocker이미지. 두번째 자식은 팝업 패널이다. 팝업패널보다 선두에 존재하고 부모 캔버스 크기와 동일하게 맞추어 놓았기에 팝업패널 뒤의 오브젝트는 클릭되지 않는다.
    void Start()
    {
        BlockerCheck();
        popupButton.onClick.AddListener(PopupButtonClicked);
        popupPanel.SetActive(false);
    }

    private void Update() 
    {
        BlockerDisable();//팝업 패널은 스크립트 외부에 설정된 closebutton으로도 끄고 켤 수 있기 때문에, 팝업패널이 꺼지면 블로커도 같이 꺼지도록 제어해야 함.
    }

    private void BlockerCheck()
    {
        if(blocker==null)
        {
            blocker = GameObject.Find("Blocker").GetComponent<Image>();
        }
        blocker.gameObject.SetActive(false);//팝업 패널이 켜지지 않았다면 블로커도 비활성화 상태.
    }

    private void BlockerActive()
    {
        if(!blocker.gameObject.activeSelf)
        {
            blocker.gameObject.SetActive(true);
        }
    }

    private void PopupButtonClicked()
    {
        if(!popupPanel.activeSelf)
        {
            popupPanel.SetActive(true);
            BlockerActive();
        }
    }

    private void BlockerDisable()
    {
        if(popupPanel.activeSelf)
        {
            blocker.gameObject.SetActive(true);
        }
        else
        {
            blocker.gameObject.SetActive(false);
        }
    }

}
