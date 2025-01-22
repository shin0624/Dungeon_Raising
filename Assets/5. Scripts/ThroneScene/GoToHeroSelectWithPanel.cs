using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToHeroSelectWithPanel : MonoBehaviour
{
   // 영웅 이미지를 클릭하면 HeroSelectScene으로 이동 후, 클릭한 영웅에 맞는 패널이 출력되어야 함. 
   //이 스크립트는 영웅이미지와 영웅 패널을 연결하고, HeroSelectScene으로 이동할 때 클릭한 영웅 데이터를 넘겨주는 역할.
    [SerializeField] private Button heroImageButton01;
    [SerializeField] private Button heroImageButton02;
    [SerializeField] private Button heroImageButton03;

    void Start()//영웅을 클릭하면 그 영웅에 해당하는 패널 정보(int형 playerID)를 HeroSelectScene으로 넘겨주어야 함.
    {
        heroImageButton01.onClick.AddListener(() => HeroButtonOnclick(1));
        heroImageButton02.onClick.AddListener(() => HeroButtonOnclick(2));
        heroImageButton03.onClick.AddListener(() => HeroButtonOnclick(3));
    }

    public void HeroButtonOnclick(int heroID)//영웅 이미지 클릭 시 HeroSelectScene으로 이동하고, 클릭한 영웅에 맞는 패널을 출력하기 위해 영웅 ID를 넘겨준다.
    {
        PlayerPrefs.SetInt("HeroID", heroID);//영웅의 ID는 int형 1,2,3...으로 구분되는 HeroID로 설정한다.
        PlayerPrefs.Save();//선택된 영웅의 id를 PlayerPrefs에 저장.

        Debug.Log($"선택된 영웅 ID : {heroID}");
        SceneManager.LoadScene("HeroSelectScene");//HeroSelectScene으로 이동.
        
    }
}
