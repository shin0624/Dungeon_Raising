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
    // 2.

    public float CalculateDamage(float thisAttackPoint, float thisAttackSpeed, float otherDefensePoint)//�ʴ� ���ط�(DPS)�� ����� ���� �� ������ ��� �޼���.
    {
        /*�������ͽ��� ���� [ ���ݷ� < ���� ] �̱� ������, ���ݷ����� �߻��ϴ� Damage�� �������� ���� ����ǰ�, �̰��� �����Ǿ� ü���� �Ҹ�Ǵ� ������. 
		���⿡ ���� �ӵ��� �߰��Ͽ� �ʴ� ���ط�(DPS)�� ����� ������ ������ �ʿ�. ��, ���� �ӵ��� ���� ���� Damage �������� ������.
        [  (���ݷ� * (100 / (100+����)) * ���ݼӵ� ] ���� ���� ���� �и� ������ ���Ͽ� ���ݷ¿��� ���¸�ŭ Damage ���� ->  �� ���� 0�� �ӵ��� ���Ͽ� �������� ���� ���ҵ� ���� �޲� �� ����.
        (ex. ���ݷ� 20, ���� 100�̶��, Damage�� 1/2���ҵ� 10������, ���� �ӵ��� 1.5�̸� Damage = 15, ���� �ӵ��� 3�̸� 30.
		���� �޼���� ���� �ӵ��� ���߾� ����ǹǷ�, ���ݼӵ��� ���� ���� ������ �������� ������ �þ��. ��, ��� ���ֿ��� ������ �������� �� �� �ִ�.*/
        
        float rawDamage = thisAttackPoint * (100 / (100 + otherDefensePoint));//gameObject�� ���ݷ� * (100 / (100 + ��� �� ����))
        return rawDamage * thisAttackSpeed;//gameObject�� ���ݼӵ��� ���Ͽ� ����.
    }

    public T GetUnitType<T>(GameObject unit) where T : Component//UnitMoveController���� targetUnit�� ã�� �� Ÿ���� Ư������ ���� ���׸� �޼���. targetUnit�� Hero����, Character����, Soldier���� �˾ƾ� ������ ������ ���� ������ ������ü�� �������ͽ� ���� ���꿡 ����� �� ����.
    {
        if (unit == null)
        {
            Debug.LogError("unit is null");
            return null;
        }

        T dataManager = unit.GetComponent<T>();//���׸� Ÿ������ dataManager ������Ʈ�� ã�ƾ� �Ѵ�. 
        if(dataManager!=null)
        {
            return dataManager;
        }

        Debug.LogError($"{typeof(T).Name} ������Ʈ�� {unit.name}�� �������� ����.");
        return null;
        //--> Ÿ ��ũ��Ʈ���� SoldierDataManager sol = GetUnitType<SoldierDataManager>(unit); �� �����ϰ� null�� ��ȯ�Ǿ��ٸ�, �Ű����� unit�� soldier�� �ƴ϶�� ��.
    }


    

}
