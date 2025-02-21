using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUIController : MonoBehaviour
{
    //SinglePlayScene�� ó�� ���� �� SinglePlayUICanvas�� Ȱ��ȭ�ǵ��� �ϴ� ��ũ��Ʈ. ĵ������ ���Ǵ� ��ư �� ĵ���� ��Ȱ��ȭ�� ����ϴ� "��ġ�Ϸ�"��ư�� �Ҵ���.
    //�ٸ� ��ư���� ���� Ȯ�强 �� ���� ���̸� ���� �ٸ� ��ũ��Ʈ���� �����Ѵ�.
    [SerializeField] private Canvas singleplayUICanvas;

    [SerializeField] private Button placeCompleteButton;//��ġ �Ϸ� ��ư
    private bool isComleteButtonClicked = false;// ��ġ �Ϸᰡ �����Ǿ����� �˸��� �÷���.

    private void Start()
    {
        if(singleplayUICanvas!=null && !singleplayUICanvas.gameObject.activeSelf)
        {
            isComleteButtonClicked = false;
            singleplayUICanvas.gameObject.SetActive(true);//Canvas�� �⺻������ ��Ȱ��ȭ�Ǿ� �����Ƿ� Ȱ��ȭ.
            placeCompleteButton.onClick.AddListener(OnPlaceCompeteButtonClicked);//��ġ �Ϸ� ��ư�� �̺�Ʈ �߰�.
        }
        if(Time.timeScale == 0)//���� ������ ���� �Ͻ����� ���¿��ٸ� ������ �簳�Ѵ�.
        {
            Time.timeScale = 1;
        }
    }

    private void OnPlaceCompeteButtonClicked()//��ġ �Ϸ� ��ư Ŭ�� �� ĵ������ ��Ȱ��ȭ�ǰ� ������ ���۵ȴ�.
    {
        singleplayUICanvas.gameObject.SetActive(false);
        if(!isComleteButtonClicked)
        {
            isComleteButtonClicked = true;//�÷��׸� true�� �ٲپ� �ٸ� ��ũ��Ʈ���� ���� ���� ���θ� �Ǵ��� �� �ְ� �Ѵ�.
        }
    }

}
