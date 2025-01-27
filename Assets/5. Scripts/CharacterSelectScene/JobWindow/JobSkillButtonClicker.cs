using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobSkillButtonClicker : MonoBehaviour
{
    //[직업 선택 창] 출력 후 직업 별 스킬 버튼 클릭 시 해당 스킬에 맞는 애니메이션이 MotionWindow 패널에 출력되도록 하는 메서드
    //MotionWindow 패널에는 각 성별, 직업에 맞는 캐릭터 프리팹이 위치해있고, 스킬 버튼을 누르면 캐릭터가 현재 재생 중인 애니메이션이 변경된다.
    //기본 상태는 IDLE, 스킬 버튼은 Skill01, Skill02, Skill03
    //본 스크립트를 각 직업 패널에 추가하고, 패널의 자식으로 있는 버튼 객체를 인스펙터에서 추가

    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private Animator anim;
    [SerializeField] private Button skillButton01;
    [SerializeField] private Button skillButton02;
    [SerializeField] private Button skillButton03;

    private void Init()
    {
        if(characterPrefab==null)
        {
            characterPrefab = GetComponentInChildren<SPUM_Prefabs>().gameObject;//캐릭터 프리팹은 SPUM으로 만들어졌으므로 해당 스크립트를 가진 게임오브젝트를 할당
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

    private void AddListenerToButtons()// 각 스킬 버튼마다 이벤트를 등록한다.
    {
        skillButton01.onClick.RemoveAllListeners();// 중복 리스너 방지
        skillButton02.onClick.RemoveAllListeners();
        skillButton03.onClick.RemoveAllListeners();

        skillButton01.onClick.AddListener(()=> SwitchAnimationClip(anim, skillButton01)) ;//각 패널의 MotionWindow에 위치한 캐릭터 프리팹은 각각의 애니메이터가 있으므로 이를 매개변수로 전달.
        skillButton02.onClick.AddListener(()=> SwitchAnimationClip(anim, skillButton02)) ;
        skillButton03.onClick.AddListener(()=> SwitchAnimationClip(anim, skillButton03)) ;
    }

    private void SwitchAnimationClip(Animator animator, Button clickedButton)//기본 애니메이터의 스킬 파라미터는 동일
    {  
        animator.speed = 0.4f;
        animator.SetBool("7_Skill01", false);
        animator.SetBool("8_Skill02", false);
        animator.SetBool("9_Skill03", false);

        if(clickedButton==skillButton01)//클릭된 스킬 버튼에 따라 올바른 애니메이션 활성화
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
