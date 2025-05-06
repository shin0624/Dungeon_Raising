using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithItemElementClicker : MonoBehaviour
{
    [SerializeField] private Image beforeLevelUpImage;//레벨업 아이템 이미지
    [SerializeField] private Image afterLevelUpImage;//레벨업 아이템 이미지
    [SerializeField] private Image beforeGradeImage;//승급 아이템 이미지
    [SerializeField] private Image afterGradeImage;//승급  아이템 이미지
    [SerializeField] private TextMeshProUGUI beforeLevel;// [ 현재레벨(itemLevel) / 최대레벨(10)]
    [SerializeField] private TextMeshProUGUI beforeStatus;// 무기일 경우 [공격력(offensivePower) n], 방어구일 경우 [방어력(defensivePower) n]
    [SerializeField] private TextMeshProUGUI afterLevel;// [ 현재레벨(itemLevel) + 1 / 최대레벨(10)]
    [SerializeField] private TextMeshProUGUI afterStatus; //무기일 경우 [공격력(offensivePower) + 상승치 n], 방어구일 경우 [방어력(defensivePower) = 상승치 n]
    [SerializeField] private TextMeshProUGUI levelUpCost;
    [SerializeField] private TextMeshProUGUI gradeUpCost;
    [SerializeField] private GameObject[] reinforcementPanels = new GameObject[2];// 레벨업 패널과 승급 패널을 구분하기 위한 배열. [0] : 레벨업 패널, [1] : 승급 패널
    [SerializeField] private TextMeshProUGUI beforeGrade;
    [SerializeField] private TextMeshProUGUI afterGrade; 
    

    // 레벨 및 스테이터스 상승치 등은 추후 수치 정립이 완료되면 추가. 

    public void AddListenerToElementButton(GameObject newItem, ArmorItem armorItem)//리스너 등록을 캡슐화하여 BlackSmithItemList.cs의 코루틴에 넣는다.
    {
        Button itemElementbutton = newItem.GetComponent<Button>();
        itemElementbutton.onClick.AddListener(() => ElementLink(newItem, armorItem));
    }

    private void ElementLink(GameObject newItem, ArmorItem armorItem)//BlackSmithItemList.cs의 PopulateItemList()에서 리스트에 출력된 아이템 버튼 클릭 시 해당 아이템 정보를 RightPivot에 표시하는 메서드
    {   
       //250506 수정 : RightPivot을 LevelUpPanel과 GradeUpPanel로 나누어, 각각의 패널에 맞는 정보를 표시하도록 수정.

        if(reinforcementPanels[0].activeSelf)//레벨업 패널이 활성화 되어있다면
        {
            beforeLevelUpImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            afterLevelUpImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            beforeLevel.text = $"Lv {armorItem.itemLevel} / 10";
            afterLevel.text = $"Lv {armorItem.itemLevel + 1} / 10";
            levelUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.levelUpCost}";
            switch(armorItem.itemParts)
            {
                case ItemPart.Weapon:
                    beforeStatus.text = $"공격력 {armorItem.offensivePower}";
                    afterStatus.text = $"공격력 {armorItem.offensivePower + 10}";//상승치는 일단 임시로 설정.
                    break;
                default:
                    beforeStatus.text = $"방어력 {armorItem.defensivePower}";
                    afterStatus.text = $"방어력 {armorItem.defensivePower + 10}";//상승치는 일단 임시로 설정.
                    break;
            }
        }
        else if(reinforcementPanels[1].activeSelf)//승급 패널이 활성화 되어있다면
        {
            beforeGradeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            afterGradeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            gradeUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.gradeUpCost}";
            beforeGrade.text = armorItem.itemGrade.ToString();
            afterGrade.text = ((Grade)((int)armorItem.itemGrade + 1)).ToString();//enum은 내부적으로 int값으로 동작하므로, itemGrade + 1이 허용된다. 하지만, ToString()으로 text출력을 위해서는 타입 캐스트가 필요.
            if(afterGrade.text == "5")//BlackStone 등급은 승급이 불가능.
            {
                afterGrade.text = "BlackStone";
                gradeUpCost.text = "승급 불가";
            }
        }
    }


}
