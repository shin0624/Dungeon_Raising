using UnityEngine;

public interface IBlackSmithManager// 대장간 매니저 인터페이스. 강화/승급 로직의 순환 참조를 피하기 위해 인터페이스로 분리.
{
    void SetActiveClicker(BlackSmithItemElementClicker clicker); // 클릭된 아이템의 정보를 표시하는 Clicker를 활성화하는 메서드
    void PerformArmorItemLevelUp();
    void PerformArmorItemAdvancement();

}
