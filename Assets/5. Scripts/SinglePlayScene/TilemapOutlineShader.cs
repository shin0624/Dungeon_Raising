using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapOutlineShader : MonoBehaviour
{
    // 유닛 드래그 상태에서 개별 타일 테두리를 강조하는 스크립트. PlaceButtonFunction.cs에서 유닛 드래그 중일 때 호출된다.
    //PlaceButtonFunction.cs에서 호출되어 유닛 주변 타일의 색을 변경하여 시각적 피드백을 주는 역할. 즉, 배치 버튼에 종속되는 기능으로, 배치 버튼이 아닌 다른 버튼이 눌린 상태라면 이 기능은 호출되지 않는다.


   [SerializeField] private Tilemap baseTilemap;    // 기존 타일맵
    [SerializeField] private Tilemap outlineTilemap; // 테두리 타일맵
    [SerializeField] private Tile outlineTile;       // 테두리 타일

    private Color color01 = new Color(1, 1, 1, 1);
    private Color color02 = new Color(1, 0, 0, 1);
    private bool isDragging = false;

    private void Update()
    {
        if (isDragging) 
        {
            // 드래그 중일 때만 색상 변화 적용
            float lerpValue = Mathf.PingPong(Time.time, 1);
            outlineTilemap.color = Color.Lerp(color01, color02, lerpValue);
        }
    }

    public void DrawTilemapOutlines()
    {
        outlineTilemap.ClearAllTiles(); // 기존 테두리 제거
        isDragging = true; // 드래그 시작

        BoundsInt bounds = baseTilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (baseTilemap.HasTile(pos))
            {
                outlineTilemap.SetTile(pos, outlineTile); // 테두리 타일로 채움 
            }
        }
    }

    public void ResetOutlineColor()
    {
        isDragging = false; // 드래그 종료
        outlineTilemap.color = color01; // 원래 색상으로 복구
    }


}
