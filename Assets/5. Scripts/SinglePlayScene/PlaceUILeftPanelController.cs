using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUILeftPanelController : MonoBehaviour
{
    /*SinglePlayScene�� ���� �г� ��ư �Ҵ�� ����� ����ϴ� ��ũ��Ʈ.
    �� �� ��ư �� ��� ����
        1. ��ġ ��ư : ������ ���� ��ġ�� ���� ������ ��ġ�� �� �ִ� ��ư. ���� Ȱ��ȭ�Ǹ� �⺻���� ���õǾ��ִ� ��ư�̴�.
        2. �ڵ� ��ġ ��ư : ���� �Է� ���� �ڵ����� ������ ��ġ�Ǵ� ��ư. Ŭ�� �� ������ ������ ������� �ʵ忡 �ڵ���ġ�ȴ�.
        3. �ʱ�ȭ ��ư : ���� ��ġ�� ���� ��ġ�� �ʱ�ȭ�Ͽ� �� Ȱ��ȭ ���� ��ġ ���·� �ǵ�����.
    */
    [Header("Buttons")]
    [SerializeField] private Button placeButton;//��ġ ��ư
    [SerializeField] private Button autoPlaceButton;//�ڵ� ��ġ ��ư
    [SerializeField] private Button resetPlaceButton;//��ġ �ʱ�ȭ ��ư

    [Header("Scripts")]
    [SerializeField] private PlaceButtonFunction placeButtonFunction;//��ġ ��ư�� ����� ����ϴ� ��ũ��Ʈ.
    [SerializeField] private AutoPlaceButtonFunction autoPlaceButtonFunction;//�ڵ� ��ġ ����� ����ϴ� ��ũ��Ʈ.


    private void OnEnable()//ĵ���� Ȱ��ȭ �� ��ư�� ������ �߰�.
    {
        placeButtonFunction.enabled = true;//��ġ ��ư ����� �⺻���� Ȱ��ȭ�Ǿ� �ִ�.
        autoPlaceButtonFunction.enabled = false;//�ڵ� ��ġ ����� �⺻������ ��Ȱ��ȭ. 
        AddListenerToButtons();
    }

    private void AddListenerToButtons()//��� ��ư�� �����ʸ� �߰��Ѵ�.
    {
        placeButton.onClick.AddListener(OnPlaceButtonClicked);
        autoPlaceButton.onClick.AddListener(OnAutoPlaceButtonClicked);
        resetPlaceButton.onClick.AddListener(OnResetPlaceButtonClicked);
    }

    private void OnPlaceButtonClicked()// ������ ���� ��ġ�� ���� ������ ��ġ�� �� �ִ� ���. ���� �ٸ� ���̾� �� �̵��� �����ϸ�, �巡�׸� ���� ĳ���͸� ������ Ÿ�Ϸ� ��ġ��Ų��. �� �� ������ Ÿ�Ͽ� �ٸ� ������ �����Ѵٸ� ���� ��ġ�� �����Ѵ�. 
    {
        placeButtonFunction.enabled = true;//��ġ ��ư ����� Ȱ��ȭ�Ѵ�. ��ǲ ó���� PlaceButtonFunction.cs���� ����Ѵ�. ��ġ ��ư ����� ��Ȱ��ȭ�Ǿ� ������ TilemapOutlineShader.cs�� ������� ����.
    }

    private void OnAutoPlaceButtonClicked()// �ڵ� ��ġ ��ư : ���� �Է� ���� �ڵ����� ������ ��ġ�Ǵ� ��ư. Ŭ�� �� ������ ������ ������� �ʵ忡 �ڵ���ġ�ȴ�.
    {
        if(!autoPlaceButtonFunction.enabled)
        {
            autoPlaceButtonFunction.enabled = true;//�ڵ� ��ġ ����� Ȱ��ȭ �Ѵ�. �ڵ� ��ġ �Ŀ��� �÷��̾ ���� ��ġ�� ���Ƿ� ������ �� �ֵ��� placeButtonFunction ������Ʈ�� Ȱ��ȭ�� �����Ѵ�.
        }
        else
        {
            autoPlaceButtonFunction.StartAutoPlace();//�ڵ���ġ ��� Ȱ��ȭ �� �ٽ� �ڵ���ġ ��ư�� ������ �ٽ� �� �� ���ֵ��� �ڵ���ġ�ȴ�.
        }
        
    }

    private void OnResetPlaceButtonClicked()// �ʱ�ȭ ��ư : ���� ��ġ�� ���� ��ġ�� �ʱ�ȭ�Ͽ� �� Ȱ��ȭ ���� ��ġ ���·� �ǵ�����.
    {

    }
}
