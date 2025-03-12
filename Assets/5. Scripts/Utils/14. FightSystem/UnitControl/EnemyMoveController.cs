using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class EnemyMoveController : MonoBehaviour
{
    /*  ���� <-> ������ ���� <-> ������ ��� ���� �缳�踦 ���� UnitMoveController ���� (�÷��̾�� ĳ���Ϳ� UnitMoveController | ���ʹ�, ���� �� EnemyMoveController)
        �±� �缳�� : �÷��̾�(Unit_Player), ����(Unit_Hero), ����(Unit_Soldier), ���ʹ�(Unit_Enemy), ����(Unit_Boss)

            ���ֵ��� ���� ��븦 ã�� Ÿ�ϸ��� ���� �̵��ϴ� ����� �����Ѵ�.
    
            [���� Ž��] -> [��ǥ ���� ����] -> [Ÿ�ϸ��� ���� �̵�] -> [����] -> [HP�� ���� 0�� �Ǵ� ������ Destroy]
            �÷��̾�� ������ �� ������ Ž�� : ���� ����� �� ������ ã�� FindClosestUnit() �޼���.
            ���� ����� �� ���ְ� ���� �÷��̾�� ������ ��ġ ���̸� ���ؼ� ���� ���� ���
            ������ �� Ÿ�Ͼ� �̵��ϵ��� �ϴ� MoveTowardTarget() �޼���.  �� �� �̵� �ӵ��� __Speed��.
            �̵� �� Ÿ�ϸ��� Ÿ�� ��ǥ�� ����Ͽ� ������ Ÿ�� �߽����� �����Ѵ�.
    */
    private Tilemap fightTilemap;//������ ����� Ÿ�ϸ�.
    private float moveSpeed = 2.0f;//�� ������ ���ǵ�. ���� / ĳ���� / ���� ���� __Information�� ������ __Speed�� �����ϹǷ�, �̸� �����´�.
    private Transform targetUnit;//���� ����� ����
    private CombatAnimatorController combatAnimatorController;
    private Coroutine moveCoroutine;//���� ���� �� �̵� �ڷ�ƾ�� ���߾�� �ϹǷ�, �̵� �ڷ�ƾ�� ������ ����.

    private void Start()
    {
        if(fightTilemap==null)
        {
            fightTilemap = GameObject.Find("Layer12_FightTilemap").GetComponent<Tilemap>();
        }
        combatAnimatorController ??= gameObject.GetComponent<CombatAnimatorController>();//�� ������ ���� ��ȭ �޼��尡 ����� Ŭ����. 
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
            //targetUnit = FindClosestUnit().transform;//���� ����� ������ ã�� �Ҵ�.
            GameObject closest = FindClosestUnit();
            if(closest==null)
            {
                yield return new WaitForSeconds(0.5f);// ���� ����� ������ ã�� ������ ��� ��Ž�� ���
                continue;//��Ž�� �õ�
            }
            targetUnit = closest.transform;

            if(moveCoroutine!=null) StopCoroutine(moveCoroutine);//���� �̵� ����.
             moveCoroutine = StartCoroutine(MoveTowardTarget(targetUnit.position));//�� ������ ��ǥ �������� �Ͽ� �̵�.

            yield return new WaitForSeconds(0.5f);//�̵� ���� ����.
        }
    }

    private IEnumerator MoveTowardTarget(Vector3 targetPositon)//������ ��ǥ �������� �̵���Ű�� �޼���.
    {
        combatAnimatorController.StartMove();//�̵� �ִϸ��̼� ����.

        if(targetUnit==null)//��ǥ ������ ���ŵǸ� �̵��� �����ϰ� FindTargetAndMove()������ ���ư� �ٽ� Ÿ���� ã�ƾ� �Ѵ�.
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(FindTargetAndMove());
            yield break;
        }
        Vector3Int currentTile = fightTilemap.WorldToCell(transform.position);//���� Ÿ�ϸ� ��ǥ�� �� ��ǥ�� ��ȯ.
        Vector3Int targetTIle = fightTilemap.WorldToCell(targetPositon);//��ǥ���� Ÿ�ϸ� ��ǥ�� �� ��ǥ�� ��ȯ.

        Vector3Int nextMove = GetNextMove(currentTile, targetTIle);//�� Ÿ�� ��ǥ �� �Ÿ��� �������� ���� �̵��� Ÿ���� ����.
        Vector3 worldMovePosition = fightTilemap.GetCellCenterWorld(nextMove);// ���� �̵��� Ÿ���� Vector3Int�� ��ǥ���� Ÿ�� �߽� ��ġ ���� ��ǥ�� ��ȯ.

        //Debug.Log("MoveTowardTarget() called.");

        while(Vector2.Distance(transform.position, worldMovePosition) > 0.4f)// ���� ������ <-> ���� �̵��� Ÿ�� �߽���ǥ �� �Ÿ��� ��ġ�� �� ����
        {
            if (targetUnit == null)//��ǥ ������ ������� �̵� ���� �� �ٽ� ��ǥ ������ Ž���Ѵ�.
            {
                if(moveCoroutine!=null) StopCoroutine(moveCoroutine);
                moveCoroutine = StartCoroutine(FindTargetAndMove());
                yield break; 
            } 
            transform.position = Vector2.MoveTowards(transform.position, worldMovePosition, moveSpeed * Time.deltaTime);//���� �̵� �ӵ��� �̵��ϸ� ������ ���� �����Ѵ�.
            yield return null;//�� ������ ���
        }

        combatAnimatorController.StopMove();//�̵� ���� �� �ִϸ��̼� ����.
        combatAnimatorController.StartAutoBattle();
    }     

    public GameObject FindClosestUnit()//���� ����� ������ ã�� �� ������ Ʈ�������� �����ϴ� �޼���.
    {
        List<GameObject> units = FindPlayableUnits();
        GameObject closestUnit = null;//���� ����� ������ Ʈ������.
        float minDistance = Mathf.Infinity;//�ּ� �Ÿ��� ���� ���Ѵ� ������. => ���� ���� �Ÿ� (2�������Ͱ� Distance)�� ����� �Ǿ�� �ּ� �Ÿ��� �ִ� ������ ã�� �� �ֱ� ����.
        
        foreach(GameObject unit in units)
        {
            float distance = Vector2.Distance(transform.position, unit.transform.position);//���� gameObject�� ��� ���� �� �Ÿ��� ����.
            if(distance < minDistance)//��� �Ÿ��� ��
            {
                minDistance = distance;//�ּ� �Ÿ� ����
                closestUnit = unit;//���� ����� ���� ����.
            }
        }
        //Debug.Log("FindClosestUnit() called.");
        return closestUnit;//���� ����� ������ ����.   
    }

    private Vector3Int GetNextMove(Vector3Int current, Vector3Int target)//���� Ÿ�Ͽ��� ���� �̵��� Ÿ���� �����ϴ� �޼���.
    {
        //Debug.Log("GetNextMove() called.");
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

    private List<GameObject> FindPlayableUnits()// ���� FindClosestUnit������ GameObject�迭���� FindGameObjectsWithTag�� ����߱� ������ �ϳ��� �±׹ۿ� ã�� ��������, ���ֵ��� ��� ������ ����Ʈ�� ���� �� �÷��̾�� ĳ���͸� ã�� ����� �޼���� ������ ��üũ�� �߰�.
    {
        List<GameObject> playableUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit_Soldier"))
        {
            GameObject.FindGameObjectWithTag("Unit_Player"),
            GameObject.FindGameObjectWithTag("Unit_Hero")
        };
        playableUnits.RemoveAll(unit => unit == null); // null üũ �߰�

        return playableUnits;
    }
}
