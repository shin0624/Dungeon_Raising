using System;
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
        if(Time.timeScale == 0)//���� ������ ���� �Ͻ����� ���¿��ٸ� ������ �簳�Ѵ�.
        {
            Time.timeScale = 1;
        }
        isComleteButtonClicked = false;
        singleplayUICanvas.gameObject.SetActive(true);//Canvas�� �⺻������ ��Ȱ��ȭ�Ǿ� �����Ƿ� Ȱ��ȭ. 
        placeCompleteButton.onClick.AddListener(OnPlaceCompeteButtonClicked);//��ġ �Ϸ� ��ư�� �̺�Ʈ �߰�.
    }

    private void OnPlaceCompeteButtonClicked()//��ġ �Ϸ� ��ư Ŭ�� �� ĵ������ ��Ȱ��ȭ�ǰ� ������ ���۵ȴ�.
    {
        if(singleplayUICanvas.gameObject.activeSelf)
        {
            singleplayUICanvas.gameObject.SetActive(false);
        }
        
        if(!isComleteButtonClicked)
        {
            isComleteButtonClicked = true;//�÷��׸� true�� �ٲپ� �ٸ� ��ũ��Ʈ���� ���� ���� ���θ� �Ǵ��� �� �ְ� �Ѵ�.
            Debug.Log("Game Start!");

            StartUnitFight();
        }
    }

    private void StartUnitFight()
    {
        GameObject[]playerUnits = GameObject.FindGameObjectsWithTag("UNIT");
        GameObject[]enemyUnits = GameObject.FindGameObjectsWithTag("ENEMYUNIT");

        foreach(GameObject unit in playerUnits)
        {
            UnitMoveController unitMoveController = unit.GetComponent<UnitMoveController>();
            if(unitMoveController!=null)
            {
                unitMoveController.StartFight();
            }
        }

        foreach(GameObject unit in enemyUnits)
        {
            UnitMoveController unitMoveController = unit. GetComponent<UnitMoveController>();
            if(unitMoveController!=null)
            {
                unitMoveController.StartFight();
            }
        }
    }


}
