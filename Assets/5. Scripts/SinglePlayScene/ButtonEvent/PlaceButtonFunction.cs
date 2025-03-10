using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceButtonFunction : MonoBehaviour
{   // 배치 버튼의 기능을 담당하는 스크립트.
    //유저가 직접 터치를 통해 유닛을 배치할 수 있는 기능. 서로 다른 레이어 간 이동이 가능하며, 드래그를 통해 캐릭터를 목적지 타일로 위치시킨다. 이 떄 목적지 타일에 다른 유닛이 존재한다면 원래 위치로 복귀한다.
    //클릭한 위치의 타일맵 좌표(Vector3Int)를 받아와서 캐릭터 선택. -> 드래그 -> 터치 종료(최종 목적지 타일이 해당 레이어에 존재하는지 확인하고, 가능하면 해당 레이어로 이동, 불가능하면 원래 위치로 복귀. 동 레이어에서의 이동은 해당 없음.)
    // 유닛의 배치 시 목표 타일맵의 중심 위치로 이동하기 위해, 드래그 중 위치 보정도 필요하다.
    [SerializeField] private Tilemap[] tileLayers;// 유닛 프리팹들이 소환될 3개의 타일맵 레이어. [0] : 캐릭터 레이어 / [1] : 영웅 레이어 / [2] : 병사 레이어
    [SerializeField] private TilemapOutlineShader outlineShader;//타일맵 테두리 효과를 적용하는 스크립트
    private Vector3Int startTile;//드래그 시작 위치
    private Tilemap currentLayer;// 현재 유닛 프리팹이 존재하는 타일맵 레이어.
    private bool isDragging = false;//드래그 중인지 여부를 판단하는 플래그.
    private Vector3 offset;//드래그 시작 위치와 현재 위치의 차이를 저장하는 변수.
    private Transform selectedUnit = null;//선택된 유닛

    public void Update()
    {
        if(Input.GetMouseButtonDown(0)) OnMouseDown();
        if(isDragging) OnMouseDrag(); 
        if(Input.GetMouseButtonUp(0)) OnMouseUp();
    }

    public void OnMouseDown()// 마우스(터치) 클릭 시 시작 위치를 저장한다.
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//마우스 클릭 위치를 월드 좌표로 변환
       RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);//마우스 클릭 위치에 레이캐스트를 쏴서 클릭한 위치에 존재하는 오브젝트를 찾는다.

       if(hit.collider!=null )
       {
            if(hit.collider.CompareTag("Unit_Player") || hit.collider.CompareTag("Unit_Hero") || hit.collider.CompareTag("Unit_Soldier"))   
            {
                selectedUnit = hit.collider.transform;//선택된 유닛을 저장한다.

                outlineShader.DrawTilemapOutlines();//드래그 중일 때 타일맵 테두리 색상을 변경한다.

                currentLayer = GetCurrentLayer(selectedUnit.position);//선택된 유닛이 존재하는 타일맵 레이어를 찾는다. --> 드래그 시작 시 현재 유닛이 속한 레이어를 정확히 찾아야 함. 이전에는 GetCurrentLayer()에서 startTile을 이용했으나, startTile이 설정되기 전에 호출되어 null 오류가 발생하였음. 이를 해결하기 위해 selectedUnit.position을 이용해 currentLayer를 즉시 설정.
                if(currentLayer == null) 
                    return;//레이어를 찾지 못하면 종료.

                startTile = currentLayer.WorldToCell(selectedUnit.position);//월드 좌표를 타일 좌표로 변환하여 시작 위치로 저장
                offset = selectedUnit.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
            }
       }
       else
       {
         isDragging = false; // 유닛을 선택하지 못하면 드래그 취소
       }
    }

    public void OnMouseDrag()//유닛을 잡고 목적지 타일로 이동한다.
    {
        if(selectedUnit!=null)//선택된 유닛이 존재하는 경우
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;//유닛의 드래그 중 위치는 마우스 위치에 offset을 더한 값.
            newPosition.z = 0; // 2D라서 z값 고정
            selectedUnit.position = newPosition;
        }
    }

    public void OnMouseUp()//드래그 종료 시 목적지 타일에 유닛을 배치한다.
    {
        if (selectedUnit == null)
            return; // 선택된 유닛이 없으면 실행 중지
        
        outlineShader.ResetOutlineColor();//드래그 종료 시 타일맵 테두리 색상을 원래 색상으로 변경한다.

        isDragging = false;//드래그 종료
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//마우스 클릭 위치를 월드 좌표로 변환
        Vector3Int targetTile = FindValidTile(worldPos);//월드 좌표를 다시 타일 좌표로 변환하여 이동하려는 위치의 타일을 찾음.
            
        if(targetTile!=Vector3Int.zero)//이동하려는 위치의 타일이 존재하는 경우
        {
            Tilemap targetLayer = GetCurrentLayer(worldPos);//이동하려는 위치의 타일이 존재하는 레이어를 찾음.
            if(targetLayer!=null)
            {
                selectedUnit.position = targetLayer.GetCellCenterWorld(targetTile);//이동하려는 위치의 타일의 중심으로 이동
                currentLayer = targetLayer;//현재 레이어 업데이트.
            }
        }
        else//목적지 타일이 없을 경우
        {
            selectedUnit.position = currentLayer.GetCellCenterWorld(startTile);//시작 위치로 복귀
        }
        selectedUnit = null;//선택된 유닛 초기화
    }

    private Tilemap GetCurrentLayer(Vector3 position)//현재 유닛 프리팹이 존재하는 타일맵 레이어를 찾는 메서드. 유닛이 존재하는 타일맵을 찾아 현재 레이어로 설정한다.
    {
        Vector3Int tilePos;
        foreach(Tilemap layer in tileLayers)
        {
            tilePos = layer.WorldToCell(position);//월드 좌표를 타일 좌표로 변환. 가장 먼저 찾은 유효한 타일의 위치를 반환하도록 변경하여, Vector3 좌표를 받아 어떤 레이어에 해당 타일이 존재하는지 체크하도록 한다.
            if(layer.HasTile(tilePos)) return layer;//해당 타일이 존재하는 경우 해당 레이어를 반환
        }
        return null;//해당 위치에 타일이 존재하지 않는 경우 null을 반환
       
    }

    private Vector3Int FindValidTile(Vector3 worldPos)//프리팹을 드래그하여 목적지 타일까지 위치시킨 후, 그 위치가 유효한지 확인한다. 3개의 레이어를 모두 검사하여 이동 가능여부를 체크한다.
    {
        Vector3Int tilePos;
        foreach(Tilemap layer in tileLayers)
        {
            tilePos = layer.WorldToCell(worldPos);//월드 좌표를 타일 좌표로 변환
            if(layer.HasTile(tilePos)) return tilePos;//해당 타일이 존재하는 경우 해당 타일 좌표를 반환
        }
        return Vector3Int.zero;//해당 위치에 타일이 존재하지 않는 경우 Vector3Int.zero를 반환
    }
}
