using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimatorController : MonoBehaviour
{
    //���� �� ���ֵ��� ���� �ִϸ��̼��� �����ϴ� Ŭ����. 
    //Animator�� FSM�� �����ϴ� �ִϸ��̼� Ŭ���� [ IDLE - MOVE - ATTACK - SKILL01 - SKILL02 - SKILL 03 - DEATH ]�̸�
    //�� Ʈ�������� �Ķ���ʹ� boolŸ�� [ isMoving, isAttacking, useSkill01, useSkill02, useSkill03, isDead ]�̴�.
    //DEATH���´� �ֿ켱���� ó���Ǿ�� ��. AnyState���� isDead ==true���Ǹ� ��� ��ȯ�ǵ���.

    [SerializeField] private Animator anim;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
