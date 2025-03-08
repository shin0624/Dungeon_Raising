using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitDataSender : MonoBehaviour
{
    //__DataManager.cs�� �����Ͽ�, ü��, ���� �� �������ͽ� ���� �޼��� ���� �ۼ��Ѵ�. ���⼭ �ۼ��� �޼���� CombatAnimatorController.cs���� ���ȴ�.
    //�ۼ��� �޼��� : ü��, ����, ��ų ���ط�, ���ݷ�, ����, ���� �ӵ�, �̵� �ӵ� �� �������ͽ� ��ȭ�� �ʿ��� ���
    // 1. ���� ��� : gameObject�� ���ݷ�, ���� �ӵ� <-> ��� ������ ü��, ���� �ʿ�
    
    public static UnitDataSender Instance {get; private set;}// UnitDataSender�� SinglePlayScene���� �׻� �ϳ��� �����ϹǷ�, ���� ���� CombatAnimatorController�� ������ unitDataSender�� �����ϵ��� �̱��� ��
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
