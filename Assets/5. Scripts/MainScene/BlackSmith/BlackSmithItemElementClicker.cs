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
    [SerializeField] private AdvancementAlert advancementAlert;//승급 실패 시 알림을 표시하기 위한 AdvancementAlert 스크립트 참조. 승급 실패 시 UI로 알림을 표시하기 위해 필요.

    //250629 : 실제 아이템 레벨업과 승급을 수행하는 기능 추가를 위해 ItemLevelUp.cs로 armorItem 매개변수를 전달하는 로직 추가
    [SerializeField] private ItemLevelUp itemLevelUp;//아이템 레벨업 스크립트. 아이템 레벨업 및 승급을 수행하는 메서드를 호출하기 위해 필요.
    private IBlackSmithManager blackSmithManager;//대장간 매니저 인터페이스 참조(순환 참조 방지)
    private ArmorItem currentSelectedItem;//현재 선택된 아이템을 저장하기 위한 변수. 레벨업 및 승급을 수행할 때 사용.

    private void Start()
    {
        // 인터페이스를 구현한 매니저 찾기
        blackSmithManager = FindObjectOfType<BlackSmithUIController>();

        if (blackSmithManager == null)
        {
            Debug.LogError("BlackSmithManager를 찾을 수 없습니다!");
        }
    }
    public void AddListenerToElementButton(GameObject newItem, ArmorItem armorItem)//리스너 등록을 캡슐화하여 BlackSmithItemList.cs의 코루틴에 넣는다.
    {
        Button itemElementbutton = newItem.GetComponent<Button>();
        itemElementbutton.onClick.AddListener(() => ElementLink(newItem, armorItem));
    }

    private void ElementLink(GameObject newItem, ArmorItem armorItem)//BlackSmithItemList.cs의 PopulateItemList()에서 리스트에 출력된 아이템 버튼 클릭 시 해당 아이템 정보를 RightPivot에 표시하는 메서드
    {
        //250506 수정 : RightPivot을 LevelUpPanel과 GradeUpPanel로 나누어, 각각의 패널에 맞는 정보를 표시하도록 수정.
        //250629 수정 : 레벨업 및 승급을 수행하는 기능 추가를 위해 ItemLevelUp.cs의 armorItem 매개변수를 전달하는 로직 추가

        currentSelectedItem = armorItem;//현재 선택된 아이템을 저장. 
        blackSmithManager?.SetActiveClicker(this);// 매니저에 현재 활성 클리커로 등록

        if (reinforcementPanels[0].activeSelf)//레벨업 패널이 활성화 되어있다면
        {
            beforeLevelUpImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            afterLevelUpImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            beforeLevel.text = $"Lv {armorItem.itemLevel} / 10";
            afterLevel.text = $"Lv {armorItem.itemLevel + 1} / 10";
            levelUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.levelUpCost}";
            switch (armorItem.itemParts)
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
        else if (reinforcementPanels[1].activeSelf)//승급 패널이 활성화 되어있다면
        {
            beforeGradeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            afterGradeImage.sprite = newItem.transform.Find("ArmorIcon").GetComponent<Image>().sprite;
            gradeUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {armorItem.gradeUpCost}";
            beforeGrade.text = armorItem.itemGrade.ToString();
            afterGrade.text = ((Grade)((int)armorItem.itemGrade + 1)).ToString();//enum은 내부적으로 int값으로 동작하므로, itemGrade + 1이 허용된다. 하지만, ToString()으로 text출력을 위해서는 타입 캐스트가 필요.
            if (afterGrade.text == "5")//BlackStone 등급은 승급이 불가능.
            {
                afterGrade.text = "BlackStone";
                gradeUpCost.text = "승급 불가";
            }
        }
    }

    public void OnItemLevelUp()// 레벨업 버튼 클릭 시 호출되는 메서드
    {
        if (currentSelectedItem != null)
        {
            if (currentSelectedItem == null)
            {
                Debug.LogWarning("선택된 아이템이 없습니다.");
                return;
            }

            if (itemLevelUp == null)
            {
                Debug.LogError("ItemLevelUp 참조가 설정되지 않았습니다!");
                return;
            }
            itemLevelUp.PerformItemLevelUp(currentSelectedItem);// ItemLevelUp의 레벨업 메서드 호출
            UpdateLevelUpUI();
        }
    }

    public void OnItemAdvancement()// 승급 버튼 클릭 시 호출되는 메서드
    {
        if (currentSelectedItem == null)
        {
            Debug.LogWarning("선택된 아이템이 없습니다.");
            return;
        }
        if (currentSelectedItem.itemLevel < 10)
        {
            ShowAdvancementFailedUI("아이템 레벨이 10이 되어야 승급이 가능합니다.");
            return;
        }
        if (itemLevelUp == null)
        {
            Debug.LogError("ItemLevelUp 참조가 설정되지 않았습니다!");
            return;
        }
        bool advancementSuccess = itemLevelUp.PerformItemAdvancement(currentSelectedItem);// ItemLevelUp의 승급 메서드 호출
        if (advancementSuccess)
        {
            currentSelectedItem.itemLevel = 1; // 승급 시 레벨 초기화. 승급하면 다시 1레벨로 돌아가야 다음 승급을 위해 레벨업이 가능.
            Debug.Log($"아이템 승급 성공: {currentSelectedItem.itemName} - 현재 등급: {currentSelectedItem.itemGrade}, 현재 재화: {PlayerInfo.Instance.GetplayerGold()}");
            UpdateAdvancementUI();
            UpdateLevelUpUI();
        }

    }


    // 레벨업 UI 업데이트
    private void UpdateLevelUpUI()
    {
        if (reinforcementPanels[0].activeSelf && currentSelectedItem != null)
        {
            beforeLevel.text = $"Lv {currentSelectedItem.itemLevel} / 10";
            afterLevel.text = $"Lv {Mathf.Min(currentSelectedItem.itemLevel + 1, 10)} / 10";
            levelUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {currentSelectedItem.levelUpCost}";

            switch (currentSelectedItem.itemParts)
            {
                case ItemPart.Weapon:
                    beforeStatus.text = $"공격력 {currentSelectedItem.offensivePower}";
                    afterStatus.text = $"공격력 {currentSelectedItem.offensivePower + 10}";
                    break;
                default:
                    beforeStatus.text = $"방어력 {currentSelectedItem.defensivePower}";
                    afterStatus.text = $"방어력 {currentSelectedItem.defensivePower + 10}";
                    break;
            }
        }
    }

    // 승급 UI 업데이트
    private void UpdateAdvancementUI()
    {
        if (reinforcementPanels[1].activeSelf && currentSelectedItem != null)
        {
            gradeUpCost.text = $"{PlayerInfo.Instance.GetplayerGold()} / {currentSelectedItem.gradeUpCost}";
            beforeGrade.text = currentSelectedItem.itemGrade.ToString();

            int nextGradeInt = (int)currentSelectedItem.itemGrade + 1;
            if (nextGradeInt >= 4) // BlackStone
            {
                afterGrade.text = "BlackStone";
                gradeUpCost.text = "승급 불가";
            }
            else
            {
                afterGrade.text = ((Grade)nextGradeInt).ToString();
            }
        }
    }

    private void ShowAdvancementFailedUI(string message)// 승급 실패 메시지를 UI로 표시하는 메서드
    {
        advancementAlert.AdvancementFailed(message);// AdvancementAlert 스크립트의 AdvancementFailed 메서드를 호출하여 실패 메시지를 표시
    }
}
