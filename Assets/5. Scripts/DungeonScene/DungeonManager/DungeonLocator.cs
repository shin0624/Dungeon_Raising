using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonLocator : MonoBehaviour// ���� �÷��̾��� currentFloor�� �ش��ϴ� Scene__�� �ε�� �� ȣ��Ǵ� Ŭ����.
                                            // �� Scene���� ������ ���� ���� SO�� ������ 5���� ����Ʈ�� ������.
                                            // TowerProgressManager�� �޼���� ���� �÷��̾ �����Ϸ��� ���� ���� Ŭ���� ����, �� Ŭ���� ���θ� Ȯ���Ѵ�.
                                            // ���� �����ϰ��� �ϴ� ���� Ŭ������� �ʾ��� �� && �� ���� Ŭ������� ���� ������ ������ �� ==> �ش� ������ �̵� ����.
                                            // currentFloor / 10 ���� �ش��ϴ� ���� (0 ~ 5)�̰�, 5�� ���� �÷ξ��̹Ƿ� 0~4���� �� 5���� ���� SO ����Ʈ�� �غ��Ѵ�.
                                            // Scene�� �ε�Ǹ�, Scene�� ��ġ�� ���� Ʈ�������� ����Ʈ ���� SO�� �������� ��ġ�Ѵ�.
                                            // ����id�� DungeonInformation.dungeonID(string)
{
    void Start()
    {
        
    }

}
