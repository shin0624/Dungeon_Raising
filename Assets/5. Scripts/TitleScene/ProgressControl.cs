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


    
    public void Play(UnityAction action = null)//�ܺο��� �ε� �ٸ� ����� �� ȣ���ϴ� �޼ҵ�. OnProgress�ڷ�ƾ�� �����Ѵ�. ����� �Ϸ�Ǿ��� �� ���ϴ� �޼ҵ带 ������ �� �ֵ��� UnityActionŸ�� �Ű������� �޴´�.
    {
        StartCoroutine(OnProgress(action));
        
    }

    private IEnumerator OnProgress(UnityAction action)
    {   
        float current = 0;
        float percent = 0;
        while(percent <1)//progressTime�� ������ �ð� ���� percent���� 0���� 1�� ���� -> progressTime���� while ���
        {
            current+=Time.deltaTime;
            percent = current / progressTime;

            textProgressData.text = $"Now Loading... {sliderProgress.value*100:F0}%";//ȭ�鿡 ��µǴ� �ε� ���൵ �ؽ�Ʈ�� sliderProgress.Value * 100 -> �������� ä������ ���� 0 ~ 100���� ���ڸ� ���
            sliderProgress.value = Mathf.Lerp(0,1,percent);//progressTime ���� ������ �ּҰ� 0 ~ �ִ밪 1���� ä����
        
            yield return null;
        }
        action?.Invoke();//action�� null�� �ƴϸ� action�޼��� ����. �� while �ݺ� ���� �� action.Invoke()�� action�� ��ϵǾ��ִ� �޼��� ����. 
       
    }

}
