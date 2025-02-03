using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DutyButtonController : MonoBehaviour
{
    //퀘스트 버튼을 클릭하면 DutyPanel Anchor된 QuestPanel이 활성화됨 -> 퀘스트 버튼을 다시 누르면 비활성화됨.
    //인벤토리 버튼과 공유하는 스크립트
    [SerializeField] private Button dutyButton;
    [SerializeField] private GameObject dutyPanel;

    private void OnEnable() 
    {
        dutyPanel.SetActive(false);
    }
    
    private void Start()
    { 
        dutyButton.onClick.AddListener(OnDutyButtonClicked);
    }

    private void OnDutyButtonClicked()
    {
        if(!dutyPanel.activeSelf)//패널이 닫혀 있을 때 버튼 클릭 시 
        {
            dutyPanel.SetActive(true);//패널 활성화
        }
        else//패널이 열려 있을 때 버튼 클릭 시
        {
            dutyPanel.SetActive(false);//버튼 비활성화
        }
    }
}
