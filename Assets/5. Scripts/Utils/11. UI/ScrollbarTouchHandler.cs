using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScrollbarTouchHandler : MonoBehaviour, IDragHandler
{
    //터치 입력으로 스크롤바 핸들을 조작하기 위한 스크립트
    [SerializeField] private Scrollbar scrollBar;
    private bool isDragging = false;

    public void OnPointerDown()//스크롤바를 터치했을 때
    {
        isDragging = true;
    }
    public void OnPointerUp()//스크롤바에서 손가락을 떼었을 때
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)//스크롤바를 터치한 상태에서 위아래로 드래그하여 Value를 변화시킬 때
    {
        if(isDragging)
        {
            //float scrollValue = Mathf.Clamp01(scrollBar.value + eventData.delta.y * 0.005f);//스크롤바의 Value 범위인 0~1사이의 값을 y방향 터치(이벤트데이터)값에 0.005를 곱한 값과 더하여 자연스러운 스크롤바 value변화를 만든다.
            //scrollBar.value = scrollValue;
        }
    }


    public void OnDragWrapper(BaseEventData data)//매개변수가 있는 메서드는 인스펙터에서 할당이 불가하므로, 이벤트 트리거에서 내부적으로 OnDrag를 호출할 수 있도록 Wrapper메서드를 만든다.
    {
        PointerEventData eventData = (PointerEventData)data;
        OnDrag(eventData);
    }
}
