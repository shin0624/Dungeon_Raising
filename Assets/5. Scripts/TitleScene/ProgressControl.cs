using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ProgressControl : MonoBehaviour
{
    [SerializeField] private Slider sliderProgress;
    [SerializeField] private TextMeshProUGUI textProgressData;
    [SerializeField] private float progressTime;


    
    public void Play(UnityAction action = null)//외부에서 로딩 바를 재생할 때 호출하는 메소드. OnProgress코루틴을 실행한다. 재생이 완료되었을 때 원하는 메소드를 실행할 수 있도록 UnityAction타입 매개변수를 받는다.
    {
        StartCoroutine(OnProgress(action));
        
    }

    private IEnumerator OnProgress(UnityAction action)
    {   
        float current = 0;
        float percent = 0;
        while(percent <1)//progressTime에 설정된 시간 동안 percent값이 0에서 1로 증가 -> progressTime동안 while 재생
        {
            current+=Time.deltaTime;
            percent = current / progressTime;

            textProgressData.text = $"Now Loading... {sliderProgress.value*100:F0}%";//화면에 출력되는 로딩 진행도 텍스트는 sliderProgress.Value * 100 -> 게이지가 채워짐에 따라 0 ~ 100까지 숫자를 출력
            sliderProgress.value = Mathf.Lerp(0,1,percent);//progressTime 동안 게이지 최소값 0 ~ 최대값 1까지 채워짐
        
            yield return null;
        }
        action?.Invoke();//action이 null이 아니면 action메서드 실행. 즉 while 반복 종료 시 action.Invoke()로 action에 등록되어있는 메서드 실행. 
       
    }

}
