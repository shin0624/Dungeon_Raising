using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class UnitMoveController : MonoBehaviour
{
    // UNIT, ENEMYUNIT 태그를 갖는 유닛에게 할당되는 클래스. 유닛들이 서로 상대를 찾고 타일맵을 따라 이동하는 기능을 구현한다.
    /*
        - 로직 : [유닛 탐색] -> [목표 방향 설정] -> [타일맵을 따라 이동] -> [공격] -> [HP가 먼저 0이 되는 유닛은 Destroy]
            플레이어블 유닛이 적 유닛을 탐색 : 가장 가까운 적 유닛을 찾는 FindClosestUnit() 메서드.
            가장 가까운 적 유닛과 현재 플레이어블 유닛의 위치 차이를 구해서 방향 벡터 계산
            유닛이 한 타일씩 이동하도록 하는 MoveTowardTarget() 메서드.  이 때 이동 속도는 __Speed값.
            이동 시 타일맵의 타일 좌표를 고려하여 유닛을 타일 중심으로 정렬한다.
    */
    private Tilemap fightTilemap;//전투가 실행될 타일맵.
    private float moveSpeed = 2.0f;//각 유닛의 스피드. 영웅 / 캐릭터 / 병사 마다 __Information에 설정된 __Speed가 존재하므로, 이를 가져온다.
    private Transform targetUnit;//가장 가까운 유닛
    private string unitTag = "";
    private CombatAnimatorController combatAnimatorController;
    
    private void Start()
    {
        if(fightTilemap==null)
        {
            fightTilemap = GameObject.Find("Layer12_FightTilemap").GetComponent<Tilemap>();
        }
        combatAnimatorController ??= gameObject.GetComponent<CombatAnimatorController>();//각 유닛의 상태 변화 메서드가 선언된 클래스.
       
        unitTag = FindTargetTag();//상대 유닛의 태그를 설정.     
    }
    public void StartFight()//게임 시작 시 호출.
    {
        StartCoroutine(FindTargetAndMove());
    }

    private IEnumerator FindTargetAndMove()//가장 가까운 적의 트랜스폼을 목표 지점으로 설정하고 MoveTowardTarget()을 실행하는 메서드.
    {
        Debug.Log("FindTargetAndMove() called.");
        while(true)
        {
            targetUnit = FindClosestUnit();//가장 가까운 유닛을 찾고 할당.
            if(targetUnit!=null)
            {
                yield return StartCoroutine(MoveTowardTarget(targetUnit.position));//그 유닛을 목표 지점으로 하여 이동.
                combatAnimatorController.SetState("isMoving");//상태 변경 : MOVE
            }
            yield return new WaitForSeconds(0.5f);//이동 간격 조절.
        }
        
    }

    private IEnumerator MoveTowardTarget(Vector3 targetPositon)//유닛을 목표 지점으로 이동시키는 메서드.
    {
        Vector3Int currentTile = fightTilemap.WorldToCell(transform.position);//현재 타일맵 좌표를 셀 좌표로 변환.
        Vector3Int targetTIle = fightTilemap.WorldToCell(targetPositon);//목표지점 타일맵 좌표를 셀 좌표로 변환.

        Vector3Int nextMove = GetNextMove(currentTile, targetTIle);//두 타일 좌표 간 거리를 바탕으로 다음 이동할 타일을 결정.
        Vector3 worldMovePosition = fightTilemap.GetCellCenterWorld(nextMove);// 다음 이동할 타일의 Vector3Int형 좌표값을 타일 중심 위치 월드 좌표로 변환.

        Debug.Log("MoveTowardTarget() called.");
        while(Vector2.Distance(transform.position, worldMovePosition) > 0.1f)// 현재 포지션 <-> 다음 이동할 타일 중심좌표 간 거리가 일치할 때 까지
        {
            transform.position = Vector2.MoveTowards(transform.position, worldMovePosition, moveSpeed * Time.deltaTime);//유닛 이동 속도로 이동하며 포지션 값을 변경한다.
            yield return null;//한 프레임 대기
        }
        
    }   

    private Transform FindClosestUnit()//가장 가까운 유닛을 찾아 그 유닛의 트랜스폼을 리턴하는 메서드.
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag(unitTag);
        Transform closestUnit = null;//가장 가까운 유닛의 트랜스폼.
        float minDistance = Mathf.Infinity;//최소 거리는 양의 무한대 값으로. => 유닛 간의 거리 (2차원벡터값 Distance)가 양수가 되어야 최소 거리에 있는 유닛을 찾을 수 있기 때문.
        
        foreach(GameObject unit in units)
        {
            float distance = Vector2.Distance(transform.position, unit.transform.position);//현재 gameObject와 상대 유닛 간 거리를 저장.
            if(distance < minDistance)//양수 거리일 때
            {
                minDistance = distance;//최소 거리 설정
                closestUnit = unit.transform;//가장 가까운 유닛의 트랜스폼 설정.
            }
        }
        Debug.Log("FindClosestUnit() called.");
        return closestUnit;//가장 가까운 유닛을 리턴.   
    }

    private Vector3Int GetNextMove(Vector3Int current, Vector3Int target)//현재 타일에서 다음 이동할 타일을 결정하는 메서드.
    {
        Debug.Log("GetNextMove() called.");
        Vector3Int direction = target - current;//타겟 타일 좌표값 - 현재 타일 좌표값 => 두 타일 간 거리 차로 방향 벡터를 구한다.
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))//방향벡터의 x값이 y값 이상일 경우, 현재 위치에서 x방향으로 이동한다.
        {
            return current + new Vector3Int(Mathf.Clamp(direction.x, -1, 1), 0, 0);//방향벡터 x값을 최소 -1 ~ 최대 1로 제한한다. 한 번의 이동은 최대 1타일 씩 가능해야 하기 때문.
        }
        else//방향벡터 x값이 y값 미만일 경우, 현재 위치에서 y방향으로 이동한다.
        {
            return current + new Vector3Int(0, Mathf.Clamp(direction.y, -1, 1), 0);
        }
    }

    private string FindTargetTag()
    {
        string targetTag = "";
        if(gameObject.tag=="UNIT")//본 클래스의 gameObject가 플레이어블 유닛이면, 상대 태그는 ENEMYUNIT.
        {
            targetTag = "ENEMYUNIT";
        }
        else if(gameObject.tag == "ENEMYUNIT")//본 클래스의 gameObject가 적 유닛이면, 상대 태그는 UNIT.
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

    private void OnTriggerEnter2D(Collider2D collision)// 본 클래스가 적용될 객체 간 컬라이더 충돌 시 전투 시작.
    {
        if(SceneManager.GetActiveScene().name == "DungeonScene")
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
        {
            if(collision.CompareTag(unitTag))
            {
                StopAllCoroutines();//이동 중지
                combatAnimatorController.SetState("isAttacking");
                Debug.Log("Fight !");
            }
        }


    }
}
