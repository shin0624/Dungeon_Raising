using UnityEngine;

public interface IBlackSmithManager// ���尣 �Ŵ��� �������̽�. ��ȭ/�±� ������ ��ȯ ������ ���ϱ� ���� �������̽��� �и�.
{
    void SetActiveClicker(BlackSmithItemElementClicker clicker); // Ŭ���� �������� ������ ǥ���ϴ� Clicker�� Ȱ��ȭ�ϴ� �޼���
    void PerformArmorItemLevelUp();
    void PerformArmorItemAdvancement();

}
