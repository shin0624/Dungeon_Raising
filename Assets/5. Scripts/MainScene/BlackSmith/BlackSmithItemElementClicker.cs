using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithItemElementClicker : MonoBehaviour
{
    [SerializeField] private Button itemListElementButton;//아이템 리스트 엘리먼트의 버튼 컴포넌트
    [SerializeField] private Image beforeImage;//레벨업 버튼 아래에 표시될 아이템 이미지
    [SerializeField] private Image afterImage;//승급 버튼 아래에 표시될 아이템 이미지
    [SerializeField] private TextMeshProUGUI beforeLevel;// [ 현재레벨(itemLevel) / 최대레벨(10)]
    [SerializeField] private TextMeshProUGUI beforeStatus;// 무기일 경우 [공격력(offensivePower) n], 방어구일 경우 [방어력(defensivePower) n]
    [SerializeField] private TextMeshProUGUI afterLevel;// [ 현재레벨(itemLevel) + 1 / 최대레벨(10)]
    [SerializeField] private TextMeshProUGUI afterStatus; //무기일 경우 [공격력(offensivePower) + 상승치 n], 방어구일 경우 [방어력(defensivePower) = 상승치 n]
    [SerializeField] private TextMeshProUGUI levelUpCost;
    [SerializeField] private TextMeshProUGUI gradeUpCost;

    // 레벨 및 스테이터스 상승치 등은 추후 수치 정립이 완료되면 추가. 

    public void AddListenerToElementButton(GameObject newItem, ArmorItem armorItem)//리스너 등록을 캡슐화하여 BlackSmithItemList.cs의 코루틴에 넣는다.
    {
        itemListElementButton.onClick.AddListener(() => ElementLink(newItem, armorItem));

        Debug.Log("리스트 각 버튼에 리스너 등록됨.");
    }

    private void ElementLink(GameObject newItem, ArmorItem armorItem)//BlackSmithItemList.cs의 PopulateItemList()에서 리스트에 출력된 아이템 버튼 클릭 시 해당 아이템 정보를 RightPivot에 표시하는 메서드
    {   
       Debug.Log($"{armorItem.itemParts} 부위 아이템 리스트에서 {armorItem.itemName}클릭함. ");

       beforeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
       afterImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;

       beforeLevel.text = $"Lv {armorItem.itemLevel} / 10";
       afterLevel.text = $"Lv {armorItem.itemLevel + 1} / 10";

       levelUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.levelUpCost}";
       gradeUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.gradeUpCost}";

        if(armorItem.itemParts == ItemPart.Weapon) 
        {
            beforeStatus.text = $"공격력 {armorItem.offensivePower}";
            afterStatus.text = $"공격력 {armorItem.offensivePower + 10}";//상승치는 일단 임시로 설정.
        }
        else
        {
            beforeStatus.text = $"방어력 {armorItem.defensivePower}";
            afterStatus.text = $"방어력 {armorItem.defensivePower + 10}";//상승치는 일단 임시로 설정.
        }
    }


}
