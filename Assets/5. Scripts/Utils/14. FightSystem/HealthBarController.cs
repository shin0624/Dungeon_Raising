using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthBarController : MonoBehaviour
{
    // ���� ������ �Ӹ� ���� ǥ�õǴ� HP Bar�� �����ϴ� Ŭ����. 3D���� ������� Image ������Ʈ�� fillAmount�� �����ϴ� ����� ����� �� ���⿡, HP Bar ��������Ʈ�� localScale.x���� �����ϴ� ����� ����� ��.
    // ������ healthPoint�� �پ�� �� HP Bar�� ���� ũ�⸦ �����Ѵ�.  HP Bar  �������� ��������Ʈ �������� ����, [ hpbar������ - hpbar �����κ� ] ���� ����. 
    // �� Ŭ������ ���� HP Bar �������� �׻� ������ �ڽ����� �����Ѵ�.
    
    [SerializeField] private Transform target;//hpbar�� ���� ������ Ʈ������
    private Vector3 offset = new Vector3(0.0f, 1.5f, 0.0f);//���� �Ӹ� ���� ��ġ�� ����.
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
            //target.position = barTransform.position + offset; // HP Bar�� ������ ��ġ�� ���󰡵��� ����.
        
        }

        barTransform = transform.Find("HP Bar");//���� �������� �ڽ� �� HP Bar�� �����´�.
        if(barTransform!=null)
        {
            originalScaleX = barTransform.localScale.x;//HP Bar�� x������ ���� �����´�.
        }
    }

    
    void Update()
    {
       
    }

    public void SetHealthPoint(float currentHP, float maxHP)//hp ������ ���� hp bar�� x������ ũ�⸦ �����ϴ� �޼���. ������ ���ݹ޾� ���� hp�� ������ �� �� �޼��带 ���� ȣ���Ѵ�.
    {
        if(barTransform!=null)
        {
            float healthPercent = Mathf.Clamp01(currentHP / maxHP);
            barTransform.localScale = new Vector3(originalScaleX * healthPercent, barTransform.localScale.y, barTransform.localScale.z);// HP Bar�� x ������ ���� ü�� ������ ���� �����ǵ��� ����. y,zũ��� �״�� ����.
        }
    }



}
