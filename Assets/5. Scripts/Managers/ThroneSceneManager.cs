using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroneSceneManager : MonoBehaviour
{
    //왕좌 씬의 패널 전환 등을 담당하는 매니저
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private GameObject dungeonFightPanel;
    [SerializeField] private Button heroButton;
    [SerializeField] private Button dungeonFightButton;
    [SerializeField] private Image characteristicImage;
    void Start()
    {
        Init();
    }

    private void HeroButtonClicked()//영웅 버튼 클릭 시 영웅 패널 활성화. 던전 결투 패널 비활성화
    {
         characteristicImage.gameObject.SetActive(true);
        dungeonFightPanel.SetActive(false);
        heroPanel.SetActive(true);
    }
    
    private void DungeonFightButtonClicked()//던전 결투 버튼 클릭 시 던전 결투 패널 활성화. 영웅 패널 비활성화
    {
        characteristicImage.gameObject.SetActive(false);
        heroPanel.SetActive(false);
        dungeonFightPanel.SetActive(true);
    }

    private void Init()
    {
        NullCheck();
        heroButton.onClick.AddListener(HeroButtonClicked);//영웅 버튼 이벤트 리스너 등록
        dungeonFightButton.onClick.AddListener(DungeonFightButtonClicked);//던전 결투 버튼 이벤트 리스너 등록
        heroPanel.SetActive(true);//처음 씬에 들어오면 영웅 패널이 먼저 활성화되어 있는 상태.
        dungeonFightPanel.SetActive(false);
        characteristicImage.gameObject.SetActive(true);
    }

    private void NullCheck()
    {
        if(heroPanel==null)
        {
            Debug.Log("HeroPanel is NULL");
            heroPanel = GameObject.Find("HeroPanel");
        }
        else
        {
            Debug.Log("HeroPanel Set Active");
        }

        if(dungeonFightPanel==null)
        {
            Debug.Log("DungeonFightPanel is NULL");
            dungeonFightPanel = GameObject.Find("DungeonFightPanel");
        }
        else
        {
            Debug.Log("DungeonFightPanel Set Active");
        }

        if(characteristicImage==null)
        {
            Debug.Log("CharacteristicImage is NULL");
            dungeonFightPanel = GameObject.Find("CharacteristicImage");
        }
    }
}
