using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResetPlaceButtonFunction : MonoBehaviour
{
    //�ʱ�ȭ ��ư Ŭ�� ��, �� ���� ������ ���� �ν��Ͻ��� position�� ó�� �����Ǿ��� position���� �ٽ� �ű�� Ŭ����.
    [SerializeField] private Tilemap characterResetLayer;
    [SerializeField] private Tilemap heroResetLayer;
    [SerializeField] private Tilemap soldierResetLayer;
    private AutoPlaceButtonFunction autoPlaceButtonFunction;
 
    void Start()
    {
        autoPlaceButtonFunction = gameObject.GetComponent<AutoPlaceButtonFunction>();
        StartResetPlace();
    }

    public void StartResetPlace()//�缳���� ���̾��� Ÿ�� ��ġ�� ���� �ν��Ͻ����� ���ġ.
    {
        StartCoroutine(ResetPlaceFunction());
    }

    private IEnumerator ResetPlaceFunction()//���� Getter, Setter����� ������� �ʱ⿡ Ÿ�ϸ� �������� ��� ��ȭ���� ���� ��.
    {
        autoPlaceButtonFunction.FindUnitsPosition(characterResetLayer, heroResetLayer, soldierResetLayer);//������� ĳ����, ����, ���� Ÿ�ϸ��� �Ű������� �ִ´�.
        yield return null;
        autoPlaceButtonFunction.MoveAllUnits(characterResetLayer, heroResetLayer, soldierResetLayer);
    }

}
