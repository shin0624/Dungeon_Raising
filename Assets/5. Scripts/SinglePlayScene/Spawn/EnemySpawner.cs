using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    // SinglePlayScene���� ���ʹ� �ν��Ͻ��� �����ϴ� Ŭ����. ���� ���� ��ũ���ͺ� ������Ʈ�� ����Ѵ�.
    // DungeonScene���� Trigger�̺�Ʈ�� ���� SinglePlayScene���� ������ ��, DungeonScene�� DungeonInfoDeliver.cs�� Set__Info()�� ���� ���޹��� ���ʹ�, ���� Ÿ���� �����Ͽ� Ÿ�Կ� �´� �������� �ν��Ͻ��Ѵ�.
    // ��ǥ Ž�� �� �ν��Ͻ��� AutoSpawn__.cs�� ���� ������ ����Ѵ�. ���ʹ̵��� ���� �Է��� ���� ��ǥ �̵��� �Ұ��ϹǷ�, ��ǥ �̵� �޼���� �������� �ʴ´�.
    // ���ʹ� �������� ����ȭ�� GameObject �迭�� [0]���� ���ʷ� �������� �Ҵ�. �� ���� �� ���� Type�� Enemy,BossType ����ü�� ����� ������ �°� Switch������ �ۼ��Ͽ� Ȯ�强�� ���δ�.
    // �ν��Ͻ� �޼���� SinglePlayScene�� Ȱ��ȭ�� �� ȣ��Ǵ� OnEnable()���� ȣ��ȴ�.
    void Start()
    {
        
    }


}
