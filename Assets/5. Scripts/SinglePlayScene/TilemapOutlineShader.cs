using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapOutlineShader : MonoBehaviour
{
    // ���� �巡�� ���¿��� ���� Ÿ�� �׵θ��� �����ϴ� ��ũ��Ʈ. PlaceButtonFunction.cs���� ���� �巡�� ���� �� ȣ��ȴ�.
    //PlaceButtonFunction.cs���� ȣ��Ǿ� ���� �ֺ� Ÿ���� ���� �����Ͽ� �ð��� �ǵ���� �ִ� ����. ��, ��ġ ��ư�� ���ӵǴ� �������, ��ġ ��ư�� �ƴ� �ٸ� ��ư�� ���� ���¶�� �� ����� ȣ����� �ʴ´�.


   [SerializeField] private Tilemap baseTilemap;    // ���� Ÿ�ϸ�
    [SerializeField] private Tilemap outlineTilemap; // �׵θ� Ÿ�ϸ�
    [SerializeField] private Tile outlineTile;       // �׵θ� Ÿ��

    private Color color01 = new Color(1, 1, 1, 1);
    private Color color02 = new Color(1, 0, 0, 1);
    private bool isDragging = false;

    private void Update()
    {
        if (isDragging) 
        {
            // �巡�� ���� ���� ���� ��ȭ ����
            float lerpValue = Mathf.PingPong(Time.time, 1);
            outlineTilemap.color = Color.Lerp(color01, color02, lerpValue);
        }
    }

    public void DrawTilemapOutlines()
    {
        outlineTilemap.ClearAllTiles(); // ���� �׵θ� ����
        isDragging = true; // �巡�� ����

        BoundsInt bounds = baseTilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (baseTilemap.HasTile(pos))
            {
                outlineTilemap.SetTile(pos, outlineTile); // �׵θ� Ÿ�Ϸ� ä�� 
            }
        }
    }

    public void ResetOutlineColor()
    {
        isDragging = false; // �巡�� ����
        outlineTilemap.color = color01; // ���� �������� ����
    }


}
