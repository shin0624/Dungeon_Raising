using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class UnitMoveController : MonoBehaviour
{
    // UNIT, ENEMYUNIT �±׸� ���� ���ֿ��� �Ҵ�Ǵ� Ŭ����. ���ֵ��� ���� ��븦 ã�� Ÿ�ϸ��� ���� �̵��ϴ� ����� �����Ѵ�.
    /*
        - ���� : [���� Ž��] -> [��ǥ ���� ����] -> [Ÿ�ϸ��� ���� �̵�] -> [����] -> [HP�� ���� 0�� �Ǵ� ������ Destroy]
            �÷��̾�� ������ �� ������ Ž�� : ���� ����� �� ������ ã�� FindClosestUnit() �޼���.
            ���� ����� �� ���ְ� ���� �÷��̾�� ������ ��ġ ���̸� ���ؼ� ���� ���� ���
            ������ �� Ÿ�Ͼ� �̵��ϵ��� �ϴ� MoveTowardTarget() �޼���.  �� �� �̵� �ӵ��� __Speed��.
            �̵� �� Ÿ�ϸ��� Ÿ�� ��ǥ�� ����Ͽ� ������ Ÿ�� �߽����� �����Ѵ�.
    */
    private Tilemap fightTilemap;//������ ����� Ÿ�ϸ�.
    private float moveSpeed = 2.0f;//�� ������ ���ǵ�. ���� / ĳ���� / ���� ���� __Information�� ������ __Speed�� �����ϹǷ�, �̸� �����´�.
    private Transform targetUnit;//���� ����� ����
    private string unitTag = "";
    private CombatAnimatorController combatAnimatorController;
    
    private void Start()
    {
        if(fightTilemap==null)
        {
            fightTilemap = GameObject.Find("Layer12_FightTilemap").GetComponent<Tilemap>();
        }
        combatAnimatorController ??= gameObject.GetComponent<CombatAnimatorController>();//�� ������ ���� ��ȭ �޼��尡 ����� Ŭ����.
       
        unitTag = FindTargetTag();//��� ������ �±׸� ����.     
    }
    public void StartFight()//���� ���� �� ȣ��.
    {
        StartCoroutine(FindTargetAndMove());
    }

    private IEnumerator FindTargetAndMove()//���� ����� ���� Ʈ�������� ��ǥ �������� �����ϰ� MoveTowardTarget()�� �����ϴ� �޼���.
    {
        Debug.Log("FindTargetAndMove() called.");
        while(true)
        {
            targetUnit = FindClosestUnit();//���� ����� ������ ã�� �Ҵ�.
            if(targetUnit!=null)
            {
                yield return StartCoroutine(MoveTowardTarget(targetUnit.position));//�� ������ ��ǥ �������� �Ͽ� �̵�.
                combatAnimatorController.SetState("isMoving");//���� ���� : MOVE
            }
            yield return new WaitForSeconds(0.5f);//�̵� ���� ����.
        }
        
    }

    private IEnumerator MoveTowardTarget(Vector3 targetPositon)//������ ��ǥ �������� �̵���Ű�� �޼���.
    {
        Vector3Int currentTile = fightTilemap.WorldToCell(transform.position);//���� Ÿ�ϸ� ��ǥ�� �� ��ǥ�� ��ȯ.
        Vector3Int targetTIle = fightTilemap.WorldToCell(targetPositon);//��ǥ���� Ÿ�ϸ� ��ǥ�� �� ��ǥ�� ��ȯ.

        Vector3Int nextMove = GetNextMove(currentTile, targetTIle);//�� Ÿ�� ��ǥ �� �Ÿ��� �������� ���� �̵��� Ÿ���� ����.
        Vector3 worldMovePosition = fightTilemap.GetCellCenterWorld(nextMove);// ���� �̵��� Ÿ���� Vector3Int�� ��ǥ���� Ÿ�� �߽� ��ġ ���� ��ǥ�� ��ȯ.

        Debug.Log("MoveTowardTarget() called.");
        while(Vector2.Distance(transform.position, worldMovePosition) > 0.1f)// ���� ������ <-> ���� �̵��� Ÿ�� �߽���ǥ �� �Ÿ��� ��ġ�� �� ����
        {
            transform.position = Vector2.MoveTowards(transform.position, worldMovePosition, moveSpeed * Time.deltaTime);//���� �̵� �ӵ��� �̵��ϸ� ������ ���� �����Ѵ�.
            yield return null;//�� ������ ���
        }
        
    }   

    private Transform FindClosestUnit()//���� ����� ������ ã�� �� ������ Ʈ�������� �����ϴ� �޼���.
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag(unitTag);
        Transform closestUnit = null;//���� ����� ������ Ʈ������.
        float minDistance = Mathf.Infinity;//�ּ� �Ÿ��� ���� ���Ѵ� ������. => ���� ���� �Ÿ� (2�������Ͱ� Distance)�� ����� �Ǿ�� �ּ� �Ÿ��� �ִ� ������ ã�� �� �ֱ� ����.
        
        foreach(GameObject unit in units)
        {
            float distance = Vector2.Distance(transform.position, unit.transform.position);//���� gameObject�� ��� ���� �� �Ÿ��� ����.
            if(distance < minDistance)//��� �Ÿ��� ��
            {
                minDistance = distance;//�ּ� �Ÿ� ����
                closestUnit = unit.transform;//���� ����� ������ Ʈ������ ����.
            }
        }
        Debug.Log("FindClosestUnit() called.");
        return closestUnit;//���� ����� ������ ����.   
    }

    private Vector3Int GetNextMove(Vector3Int current, Vector3Int target)//���� Ÿ�Ͽ��� ���� �̵��� Ÿ���� �����ϴ� �޼���.
    {
        Debug.Log("GetNextMove() called.");
        Vector3Int direction = target - current;//Ÿ�� Ÿ�� ��ǥ�� - ���� Ÿ�� ��ǥ�� => �� Ÿ�� �� �Ÿ� ���� ���� ���͸� ���Ѵ�.
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))//���⺤���� x���� y�� �̻��� ���, ���� ��ġ���� x�������� �̵��Ѵ�.
        {
            return current + new Vector3Int(Mathf.Clamp(direction.x, -1, 1), 0, 0);//���⺤�� x���� �ּ� -1 ~ �ִ� 1�� �����Ѵ�. �� ���� �̵��� �ִ� 1Ÿ�� �� �����ؾ� �ϱ� ����.
        }
        else//���⺤�� x���� y�� �̸��� ���, ���� ��ġ���� y�������� �̵��Ѵ�.
        {
            return current + new Vector3Int(0, Mathf.Clamp(direction.y, -1, 1), 0);
        }
    }

    private string FindTargetTag()
    {
        string targetTag = "";
        if(gameObject.tag=="UNIT")//�� Ŭ������ gameObject�� �÷��̾�� �����̸�, ��� �±״� ENEMYUNIT.
        {
            targetTag = "ENEMYUNIT";
        }
        else if(gameObject.tag == "ENEMYUNIT")//�� Ŭ������ gameObject�� �� �����̸�, ��� �±״� UNIT.
        {
            targetTag = "UNIT";
        }
        else
        {
            targetTag = "Dungeon";
        }
        Debug.Log("FindTargetTag() called.");;
        return targetTag;
    }

    private void OnTriggerEnter2D(Collider2D collision)// �� Ŭ������ ����� ��ü �� �ö��̴� �浹 �� ���� ����.
    {
        if(SceneManager.GetActiveScene().name == "DungeonScene")
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
        {
            if(collision.CompareTag(unitTag))
            {
                StopAllCoroutines();//�̵� ����
                combatAnimatorController.SetState("isAttacking");
                Debug.Log("Fight !");
            }
        }


    }
}
