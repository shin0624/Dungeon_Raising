using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ResetPlaceButtonFunction : MonoBehaviour
{
    //초기화 버튼 클릭 시, 씬 위에 스폰된 유닛 인스턴스의 position을 처음 스폰되었던 position으로 다시 옮기는 클래스.
    [SerializeField] private Tilemap characterResetLayer;
    [SerializeField] private Tilemap heroResetLayer;
    [SerializeField] private Tilemap soldierResetLayer;
    private AutoPlaceButtonFunction autoPlaceButtonFunction;
 
    void Start()
    {
        autoPlaceButtonFunction = gameObject.GetComponent<AutoPlaceButtonFunction>();
        StartResetPlace();
    }

    public void StartResetPlace()//재설정한 레이어의 타일 위치로 유닛 인스턴스들을 재배치.
    {
        StartCoroutine(ResetPlaceFunction());
    }

    private IEnumerator ResetPlaceFunction()//기존 Getter, Setter방식을 사용하지 않기에 타일맵 참조값이 계속 변화하지 않을 것.
    {
        autoPlaceButtonFunction.FindUnitsPosition(characterResetLayer, heroResetLayer, soldierResetLayer);//순서대로 캐릭터, 영웅, 병사 타일맵을 매개변수로 넣는다.
        yield return null;
        autoPlaceButtonFunction.MoveAllUnits(characterResetLayer, heroResetLayer, soldierResetLayer);
    }

}
