using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToHeroSelectWithPanel : MonoBehaviour
{
   // 영웅 이미지를 클릭하면 HeroSelectScene으로 이동 후, 클릭한 영웅에 맞는 패널이 출력되어야 함. 이 스크립트는 영웅이미지와 영웅 패널을 연결하고, HeroSelectScene으로 이동할 때 클릭한 영웅 데이터를 넘겨주는 역할.
    [SerializeField] private Button heroImageButton01;
    [SerializeField] private Button heroImageButton02;
    [SerializeField] private Button heroImageButton03;
    [SerializeField] private GameObject heroPanel01;//영웅 패널들은 prefab폴더에 prefab으로 저장되어 있음.
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
        //영웅 이미지 클릭 시 HeroSelectScene으로 이동하고, 클릭한 영웅에 맞는 패널을 출력하는 함수.


    }
}
