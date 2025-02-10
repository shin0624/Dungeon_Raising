using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithPlayerItemInfo : MonoBehaviour
{
    /* 보유 장비 정보와 ui요소를 연동하는 클래스.
    1. armorItem의 각 부위에 해당하는 (2*3)의 그리드 위에 버튼이 존재하고, 각 버튼 클릭 시 버튼 위로 출력되는 빈 패널에 플레이어 인벤토리(아이템DB)에 저장된 armorItem 목록을 확인할 수 있다.
    2. 버튼은 기본 장비 이미지로 존재하다가, 패널에서 강화 대상 아이템 클릭 시 그 아이템의 이미지로 바뀐다.
    3. 버튼 클릭 시 UpperCenterCorner위치에 비활상태이던 아이템 리스트 패널이 활성화. -> 패널에는 한 줄에 하나씩 소지한 아이템이 표시.
        -> 해당 패널의 부모 버튼을 한번 더 클릭하면 아이템 리스트 패널이 닫힌다.
    4. 한 줄은 버튼 컴포넌트가 존재하며, 좌측에는 아이템 이미지, 우측에는 아이템 이름이 표시.
    5. 선택된 아이템 정보를 변수에 저장하여 다른 대장간 제어 클래스에서 참조할 수 있게 한다.
    */

    [SerializeField] private Button[] armorItemButtons = new Button[6];//장비 아이템 버튼들. 순서대로 weapon, arm, head, waist, chest, leg
    [SerializeField] private GameObject[] armorItemInfoPanels = new GameObject[6];//각 버튼에 비활성화된 상태로 존재하는 아이템리스트 출력 패널들
    [SerializeField] private ItemDatabase itemDatabase;//아이템 데이터베이스 스크립터블 오브젝트

    [SerializeField] private BlackSmithItemList[] blackSmithItemList = new BlackSmithItemList[6];

    private void OnEnable() 
    {
        AttachAllListeners();//리스너 등록
    }

    private void OnDisable() 
    {
        DettachAllListeners();//리스너 해제
    }

    private void AttachAllListeners()//각 장비 아이템 부위별 버튼에 이벤트 리스너를 등록한다.
    {
        DettachAllListeners();//혹시 남아있는 리스너가 있을 경우를 방지하기 위해, 일단 모두 제거 후 리스너를 등록한다.
        armorItemButtons[0].onClick.AddListener(() => armorItemButtonClicked(0, ItemPart.Weapon));
        armorItemButtons[1].onClick.AddListener(() => armorItemButtonClicked(1, ItemPart.Arm));
        armorItemButtons[2].onClick.AddListener(() => armorItemButtonClicked(2, ItemPart.Head));
        armorItemButtons[3].onClick.AddListener(() => armorItemButtonClicked(3, ItemPart.Waist));
        armorItemButtons[4].onClick.AddListener(() => armorItemButtonClicked(4, ItemPart.Chest));
        armorItemButtons[5].onClick.AddListener(() => armorItemButtonClicked(5, ItemPart.Leg));
        Debug.Log("대장간 리스너 버튼 장착");
    }

    private void DettachAllListeners()//창이 비활성화 되면 모든 리스너를 제거.
    {
        foreach(Button armorItemButton in armorItemButtons)
        {
            armorItemButton.onClick.RemoveAllListeners();
        }
        Debug.Log("대장간 리스너 버튼 해제");
    }

    private void armorItemButtonClicked(int num, ItemPart part)//매개변수로 아이템 부위를 넣어서, 버튼을 클릭하면 해당하는 부위 아이템 리스트 활성화.
    {
        Debug.Log($"아이템 버튼 클릭됨 : {part}");
        bool isExist = itemDatabase.armorItems.Exists(i => i.itemParts == part);//해당 파트의 아이템이 있다면 true, 없으면 false.
        if(isExist)
        {
            if(armorItemInfoPanels[num].activeSelf)//이미 해당 리스트가 열려있으면 닫는다.
            {
                armorItemInfoPanels[num].SetActive(false);
            }
            else//해당 리스트가 닫혀있을 때에만 리스트를 활성화.
            {
                for(int i=0; i<armorItemInfoPanels.Length; i++)
                {
                    armorItemInfoPanels[i].SetActive(false);
                }
                armorItemInfoPanels[num].SetActive(true);
                blackSmithItemList[num].DelayPopulateItemList(part);

                Debug.Log($"아이템 버튼 클릭 : {part}");
            }
        }
        else
        {
            Debug.Log($"{part}에 해당하는 아이템을 보유하고 있지 않습니다.");
        }
        
    }
}
