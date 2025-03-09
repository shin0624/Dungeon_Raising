using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarController : MonoBehaviour
{
    // 유닛 프리팹 머리 위에 표시되는 HP Bar를 제어하는 클래스. 3D에서 만들었던 Image 컴포넌트의 fillAmount를 제어하는 방식을 사용할 수 없기에, HP Bar 스프라이트의 localScale.x값을 제어하는 방식을 사용할 것.
    // 유닛의 healthPoint가 줄어들 때 HP Bar의 가로 크기를 변경한다.  HP Bar  프리팹은 스프라이트 렌더러를 갖고, [ hpbar프레임 - hpbar 빨간부분 ] 으로 구성. 
    // 이 클래스가 붙은 HP Bar 프리팹은 항상 유닛의 자식으로 존재한다.
    
    [SerializeField] private Transform target;//hpbar를 붙일 유닛의 트랜스폼
    private Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);//유닛 머리 위로 위치를 조정.
    private Transform barTransform;
    private float originalScaleX;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name!= "SinglePlayScene")
         {
             gameObject.SetActive(false);
             enabled = false;
             return;
         }
        // gameObject.SetActive(false);
        //     enabled = false;
        //     return;
    }
    
    void Start()
    {
        if(target==null)
        {
            target = gameObject.transform;
            //target.position = barTransform.position + offset; // HP Bar가 유닛의 위치를 따라가도록 설정.
        
        }

        barTransform = transform.Find("HP Bar");//현재 프리팹의 자식 중 HP Bar를 가져온다.
        if(barTransform!=null)
        {
            originalScaleX = barTransform.localScale.x;//HP Bar의 x스케일 값을 가져온다.
        }
    }

    
    void Update()
    {
       
    }

    public void SetHealthPoint(float currentHP, float maxHP)//hp 비율에 따라 hp bar의 x스케일 크기를 조절하는 메서드. 유닛이 공격받아 실제 hp가 감소할 때 이 메서드를 같이 호출한다.
    {
        if(barTransform!=null)
        {
            float healthPercent = Mathf.Clamp01(currentHP / maxHP);
            barTransform.localScale = new Vector3(originalScaleX * healthPercent, barTransform.localScale.y, barTransform.localScale.z);// HP Bar의 x 스케일 값이 체력 비율에 따라 조절되도록 설정. y,z크기는 그대로 유지.
        }
    }



}
