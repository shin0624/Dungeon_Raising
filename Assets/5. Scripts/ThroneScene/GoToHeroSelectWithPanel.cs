using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToHeroSelectWithPanel : MonoBehaviour
{
   // ���� �̹����� Ŭ���ϸ� HeroSelectScene���� �̵� ��, Ŭ���� ������ �´� �г��� ��µǾ�� ��. �� ��ũ��Ʈ�� �����̹����� ���� �г��� �����ϰ�, HeroSelectScene���� �̵��� �� Ŭ���� ���� �����͸� �Ѱ��ִ� ����.
    [SerializeField] private Button heroImageButton01;
    [SerializeField] private Button heroImageButton02;
    [SerializeField] private Button heroImageButton03;
    [SerializeField] private GameObject heroPanel01;//���� �гε��� prefab������ prefab���� ����Ǿ� ����.
    [SerializeField] private GameObject heroPanel02;
    [SerializeField] private GameObject heroPanel03;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void HeroButtonOnclick( )
    {
        //���� �̹��� Ŭ�� �� HeroSelectScene���� �̵��ϰ�, Ŭ���� ������ �´� �г��� ����ϴ� �Լ�.


    }
}
