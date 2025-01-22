using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToHeroSelectWithPanel : MonoBehaviour
{
   // ���� �̹����� Ŭ���ϸ� HeroSelectScene���� �̵� ��, Ŭ���� ������ �´� �г��� ��µǾ�� ��. 
   //�� ��ũ��Ʈ�� �����̹����� ���� �г��� �����ϰ�, HeroSelectScene���� �̵��� �� Ŭ���� ���� �����͸� �Ѱ��ִ� ����.
    [SerializeField] private Button heroImageButton01;
    [SerializeField] private Button heroImageButton02;
    [SerializeField] private Button heroImageButton03;

    void Start()//������ Ŭ���ϸ� �� ������ �ش��ϴ� �г� ����(int�� playerID)�� HeroSelectScene���� �Ѱ��־�� ��.
    {
        heroImageButton01.onClick.AddListener(() => HeroButtonOnclick(1));
        heroImageButton02.onClick.AddListener(() => HeroButtonOnclick(2));
        heroImageButton03.onClick.AddListener(() => HeroButtonOnclick(3));
    }

    public void HeroButtonOnclick(int heroID)//���� �̹��� Ŭ�� �� HeroSelectScene���� �̵��ϰ�, Ŭ���� ������ �´� �г��� ����ϱ� ���� ���� ID�� �Ѱ��ش�.
    {
        PlayerPrefs.SetInt("HeroID", heroID);//������ ID�� int�� 1,2,3...���� ���еǴ� HeroID�� �����Ѵ�.
        PlayerPrefs.Save();//���õ� ������ id�� PlayerPrefs�� ����.

        Debug.Log($"���õ� ���� ID : {heroID}");
        SceneManager.LoadScene("HeroSelectScene");//HeroSelectScene���� �̵�.
        
    }
}
