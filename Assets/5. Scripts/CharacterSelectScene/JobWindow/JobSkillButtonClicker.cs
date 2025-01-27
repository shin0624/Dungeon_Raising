using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSkillButtonClicker : MonoBehaviour
{
    //[���� ���� â] ��� �� ���� �� ��ų ��ư Ŭ�� �� �ش� ��ų�� �´� �ִϸ��̼��� MotionWindow �гο� ��µǵ��� �ϴ� �޼���
    //MotionWindow �гο��� �� ����, ������ �´� ĳ���� �������� ��ġ���ְ�, ��ų ��ư�� ������ ĳ���Ͱ� ���� ��� ���� �ִϸ��̼��� ����ȴ�.
    //�⺻ ���´� IDLE, ��ų ��ư�� Skill01, Skill02, Skill03
    //�� ��ũ��Ʈ�� �� ���� �гο� �߰��ϰ�, �г��� �ڽ����� �ִ� ��ư ��ü�� �ν����Ϳ��� �߰�

    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Animator anim;
    [SerializeField] private Button skillButton01;
    [SerializeField] private Button skillButton02;
    [SerializeField] private Button skillButton03;

    private void Init()
    {
        if(characterPrefab==null)
        {
            characterPrefab = GetComponentInChildren<SPUM_Prefabs>().gameObject;//ĳ���� �������� SPUM���� ����������Ƿ� �ش� ��ũ��Ʈ�� ���� ���ӿ�����Ʈ�� �Ҵ�
        }
        if(anim==null)
        {
            characterPrefab.GetComponentInChildren<Animator>();
        }
    }
    void Start()
    {
        Init();
        AddListenerToButtons();
    }

    private void AddListenerToButtons()// �� ��ų ��ư���� �̺�Ʈ�� ����Ѵ�.
    {
        skillButton01.onClick.RemoveAllListeners();// �ߺ� ������ ����
        skillButton02.onClick.RemoveAllListeners();
        skillButton03.onClick.RemoveAllListeners();

        skillButton01.onClick.AddListener(()=> SwitchAnimationClip(anim, skillButton01)) ;//�� �г��� MotionWindow�� ��ġ�� ĳ���� �������� ������ �ִϸ����Ͱ� �����Ƿ� �̸� �Ű������� ����.
        skillButton02.onClick.AddListener(()=> SwitchAnimationClip(anim, skillButton02)) ;
        skillButton03.onClick.AddListener(()=> SwitchAnimationClip(anim, skillButton03)) ;
    }

    private void SwitchAnimationClip(Animator animator, Button clickedButton)//�⺻ �ִϸ������� ��ų �Ķ���ʹ� ����
    {  
        animator.speed = 0.4f;
        animator.SetBool("7_Skill01", false);
        animator.SetBool("8_Skill02", false);
        animator.SetBool("9_Skill03", false);

        if(clickedButton==skillButton01)//Ŭ���� ��ų ��ư�� ���� �ùٸ� �ִϸ��̼� Ȱ��ȭ
        {
            animator.SetBool("7_Skill01", true);
        }
        else if(clickedButton==skillButton02)
        {
            animator.SetBool("8_Skill02", true);
        }
        else if(clickedButton==skillButton03)
        {
            animator.SetBool("9_Skill03", true);
        }

    }
}
