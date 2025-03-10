using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceButtonFunction : MonoBehaviour
{   // ��ġ ��ư�� ����� ����ϴ� ��ũ��Ʈ.
    //������ ���� ��ġ�� ���� ������ ��ġ�� �� �ִ� ���. ���� �ٸ� ���̾� �� �̵��� �����ϸ�, �巡�׸� ���� ĳ���͸� ������ Ÿ�Ϸ� ��ġ��Ų��. �� �� ������ Ÿ�Ͽ� �ٸ� ������ �����Ѵٸ� ���� ��ġ�� �����Ѵ�.
    //Ŭ���� ��ġ�� Ÿ�ϸ� ��ǥ(Vector3Int)�� �޾ƿͼ� ĳ���� ����. -> �巡�� -> ��ġ ����(���� ������ Ÿ���� �ش� ���̾ �����ϴ��� Ȯ���ϰ�, �����ϸ� �ش� ���̾�� �̵�, �Ұ����ϸ� ���� ��ġ�� ����. �� ���̾���� �̵��� �ش� ����.)
    // ������ ��ġ �� ��ǥ Ÿ�ϸ��� �߽� ��ġ�� �̵��ϱ� ����, �巡�� �� ��ġ ������ �ʿ��ϴ�.
    [SerializeField] private Tilemap[] tileLayers;// ���� �����յ��� ��ȯ�� 3���� Ÿ�ϸ� ���̾�. [0] : ĳ���� ���̾� / [1] : ���� ���̾� / [2] : ���� ���̾�
    [SerializeField] private TilemapOutlineShader outlineShader;//Ÿ�ϸ� �׵θ� ȿ���� �����ϴ� ��ũ��Ʈ
    private Vector3Int startTile;//�巡�� ���� ��ġ
    private Tilemap currentLayer;// ���� ���� �������� �����ϴ� Ÿ�ϸ� ���̾�.
    private bool isDragging = false;//�巡�� ������ ���θ� �Ǵ��ϴ� �÷���.
    private Vector3 offset;//�巡�� ���� ��ġ�� ���� ��ġ�� ���̸� �����ϴ� ����.
    private Transform selectedUnit = null;//���õ� ����

    public void Update()
    {
        if(Input.GetMouseButtonDown(0)) OnMouseDown();
        if(isDragging) OnMouseDrag(); 
        if(Input.GetMouseButtonUp(0)) OnMouseUp();
    }

    public void OnMouseDown()// ���콺(��ġ) Ŭ�� �� ���� ��ġ�� �����Ѵ�.
    {
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//���콺 Ŭ�� ��ġ�� ���� ��ǥ�� ��ȯ
       RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);//���콺 Ŭ�� ��ġ�� ����ĳ��Ʈ�� ���� Ŭ���� ��ġ�� �����ϴ� ������Ʈ�� ã�´�.

       if(hit.collider!=null )
       {
            if(hit.collider.CompareTag("Unit_Player") || hit.collider.CompareTag("Unit_Hero") || hit.collider.CompareTag("Unit_Soldier"))   
            {
                selectedUnit = hit.collider.transform;//���õ� ������ �����Ѵ�.

                outlineShader.DrawTilemapOutlines();//�巡�� ���� �� Ÿ�ϸ� �׵θ� ������ �����Ѵ�.

                currentLayer = GetCurrentLayer(selectedUnit.position);//���õ� ������ �����ϴ� Ÿ�ϸ� ���̾ ã�´�. --> �巡�� ���� �� ���� ������ ���� ���̾ ��Ȯ�� ã�ƾ� ��. �������� GetCurrentLayer()���� startTile�� �̿�������, startTile�� �����Ǳ� ���� ȣ��Ǿ� null ������ �߻��Ͽ���. �̸� �ذ��ϱ� ���� selectedUnit.position�� �̿��� currentLayer�� ��� ����.
                if(currentLayer == null) 
                    return;//���̾ ã�� ���ϸ� ����.

                startTile = currentLayer.WorldToCell(selectedUnit.position);//���� ��ǥ�� Ÿ�� ��ǥ�� ��ȯ�Ͽ� ���� ��ġ�� ����
                offset = selectedUnit.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
            }
       }
       else
       {
         isDragging = false; // ������ �������� ���ϸ� �巡�� ���
       }
    }

    public void OnMouseDrag()//������ ��� ������ Ÿ�Ϸ� �̵��Ѵ�.
    {
        if(selectedUnit!=null)//���õ� ������ �����ϴ� ���
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;//������ �巡�� �� ��ġ�� ���콺 ��ġ�� offset�� ���� ��.
            newPosition.z = 0; // 2D�� z�� ����
            selectedUnit.position = newPosition;
        }
    }

    public void OnMouseUp()//�巡�� ���� �� ������ Ÿ�Ͽ� ������ ��ġ�Ѵ�.
    {
        if (selectedUnit == null)
            return; // ���õ� ������ ������ ���� ����
        
        outlineShader.ResetOutlineColor();//�巡�� ���� �� Ÿ�ϸ� �׵θ� ������ ���� �������� �����Ѵ�.

        isDragging = false;//�巡�� ����
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//���콺 Ŭ�� ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3Int targetTile = FindValidTile(worldPos);//���� ��ǥ�� �ٽ� Ÿ�� ��ǥ�� ��ȯ�Ͽ� �̵��Ϸ��� ��ġ�� Ÿ���� ã��.
            
        if(targetTile!=Vector3Int.zero)//�̵��Ϸ��� ��ġ�� Ÿ���� �����ϴ� ���
        {
            Tilemap targetLayer = GetCurrentLayer(worldPos);//�̵��Ϸ��� ��ġ�� Ÿ���� �����ϴ� ���̾ ã��.
            if(targetLayer!=null)
            {
                selectedUnit.position = targetLayer.GetCellCenterWorld(targetTile);//�̵��Ϸ��� ��ġ�� Ÿ���� �߽����� �̵�
                currentLayer = targetLayer;//���� ���̾� ������Ʈ.
            }
        }
        else//������ Ÿ���� ���� ���
        {
            selectedUnit.position = currentLayer.GetCellCenterWorld(startTile);//���� ��ġ�� ����
        }
        selectedUnit = null;//���õ� ���� �ʱ�ȭ
    }

    private Tilemap GetCurrentLayer(Vector3 position)//���� ���� �������� �����ϴ� Ÿ�ϸ� ���̾ ã�� �޼���. ������ �����ϴ� Ÿ�ϸ��� ã�� ���� ���̾�� �����Ѵ�.
    {
        Vector3Int tilePos;
        foreach(Tilemap layer in tileLayers)
        {
            tilePos = layer.WorldToCell(position);//���� ��ǥ�� Ÿ�� ��ǥ�� ��ȯ. ���� ���� ã�� ��ȿ�� Ÿ���� ��ġ�� ��ȯ�ϵ��� �����Ͽ�, Vector3 ��ǥ�� �޾� � ���̾ �ش� Ÿ���� �����ϴ��� üũ�ϵ��� �Ѵ�.
            if(layer.HasTile(tilePos)) return layer;//�ش� Ÿ���� �����ϴ� ��� �ش� ���̾ ��ȯ
        }
        return null;//�ش� ��ġ�� Ÿ���� �������� �ʴ� ��� null�� ��ȯ
       
    }

    private Vector3Int FindValidTile(Vector3 worldPos)//�������� �巡���Ͽ� ������ Ÿ�ϱ��� ��ġ��Ų ��, �� ��ġ�� ��ȿ���� Ȯ���Ѵ�. 3���� ���̾ ��� �˻��Ͽ� �̵� ���ɿ��θ� üũ�Ѵ�.
    {
        Vector3Int tilePos;
        foreach(Tilemap layer in tileLayers)
        {
            tilePos = layer.WorldToCell(worldPos);//���� ��ǥ�� Ÿ�� ��ǥ�� ��ȯ
            if(layer.HasTile(tilePos)) return tilePos;//�ش� Ÿ���� �����ϴ� ��� �ش� Ÿ�� ��ǥ�� ��ȯ
        }
        return Vector3Int.zero;//�ش� ��ġ�� Ÿ���� �������� �ʴ� ��� Vector3Int.zero�� ��ȯ
    }
}
