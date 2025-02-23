using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScrollbarTouchHandler : MonoBehaviour, IDragHandler
{
    //��ġ �Է����� ��ũ�ѹ� �ڵ��� �����ϱ� ���� ��ũ��Ʈ
    [SerializeField] private Scrollbar scrollBar;
    private bool isDragging = false;

    public void OnPointerDown()//��ũ�ѹٸ� ��ġ���� ��
    {
        isDragging = true;
    }
    public void OnPointerUp()//��ũ�ѹٿ��� �հ����� ������ ��
    {
        isDragging = false;
    }

    public void OnDrag(PointerEventData eventData)//��ũ�ѹٸ� ��ġ�� ���¿��� ���Ʒ��� �巡���Ͽ� Value�� ��ȭ��ų ��
    {
        if(isDragging)
        {
            //float scrollValue = Mathf.Clamp01(scrollBar.value + eventData.delta.y * 0.005f);//��ũ�ѹ��� Value ������ 0~1������ ���� y���� ��ġ(�̺�Ʈ������)���� 0.005�� ���� ���� ���Ͽ� �ڿ������� ��ũ�ѹ� value��ȭ�� �����.
            //scrollBar.value = scrollValue;
        }
    }


    public void OnDragWrapper(BaseEventData data)//�Ű������� �ִ� �޼���� �ν����Ϳ��� �Ҵ��� �Ұ��ϹǷ�, �̺�Ʈ Ʈ���ſ��� ���������� OnDrag�� ȣ���� �� �ֵ��� Wrapper�޼��带 �����.
    {
        PointerEventData eventData = (PointerEventData)data;
        OnDrag(eventData);
    }
}
