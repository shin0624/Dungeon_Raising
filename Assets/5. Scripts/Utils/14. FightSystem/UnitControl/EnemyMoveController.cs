using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class EnemyMoveController : MonoBehaviour
{
    /*  전투 <-> 데이터 전달 <-> 데미지 계산 로직 재설계를 위한 UnitMoveController 분할 (플레이어블 캐릭터용 UnitMoveController | 에너미, 보스 용 EnemyMoveController)
        태그 재설정 : 플레이어(Unit_Player), 영웅(Unit_Hero), 병사(Unit_Soldier), 에너미(Unit_Enemy), 보스(Unit_Boss)

            유닛들이 서로 상대를 찾고 타일맵을 따라 이동하는 기능을 구현한다.
    
            [유닛 탐색] -> [목표 방향 설정] -> [타일맵을 따라 이동] -> [공격] -> [HP가 먼저 0이 되는 유닛은 Destroy]
            플레이어블 유닛이 적 유닛을 탐색 : 가장 가까운 적 유닛을 찾는 FindClosestUnit() 메서드.
            가장 가까운 적 유닛과 현재 플레이어블 유닛의 위치 차이를 구해서 방향 벡터 계산
            유닛이 한 타일씩 이동하도록 하는 MoveTowardTarget() 메서드.  이 때 이동 속도는 __Speed값.
            이동 시 타일맵의 타일 좌표를 고려하여 유닛을 타일 중심으로 정렬한다.
    */
    private Tilemap fightTilemap;//전투가 실행될 타일맵.
    private float moveSpeed = 2.0f;//각 유닛의 스피드. 영웅 / 캐릭터 / 병사 마다 __Information에 설정된 __Speed가 존재하므로, 이를 가져온다.
    private Transform targetUnit;//가장 가까운 유닛
    private CombatAnimatorController combatAnimatorController;
    private Coroutine moveCoroutine;//전투 시작 시 이동 코루틴을 멈추어야 하므로, 이동 코루틴을 변수에 저장.

    private void Start()
    {
        if(fightTilemap==null)
        {
            fightTilemap = GameObject.Find("Layer12_FightTilemap").GetComponent<Tilemap>();
        }
        combatAnimatorController ??= gameObject.GetComponent<CombatAnimatorController>();//각 유닛의 상태 변화 메서드가 선언된 클래스. 
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
            //targetUnit = FindClosestUnit().transform;//가장 가까운 유닛을 찾고 할당.
            GameObject closest = FindClosestUnit();
            if(closest==null)
            {
                yield return new WaitForSeconds(0.5f);// 가장 가까운 유닛을 찾지 못했을 경우 재탐색 대기
                continue;//재탐색 시도
            }
            targetUnit = closest.transform;

            if(moveCoroutine!=null) StopCoroutine(moveCoroutine);//기존 이동 중지.
             moveCoroutine = StartCoroutine(MoveTowardTarget(targetUnit.position));//그 유닛을 목표 지점으로 하여 이동.

            yield return new WaitForSeconds(0.5f);//이동 간격 조절.
        }
    }

    private IEnumerator MoveTowardTarget(Vector3 targetPositon)//유닛을 목표 지점으로 이동시키는 메서드.
    {
        combatAnimatorController.StartMove();//이동 애니메이션 시작.

        if(targetUnit==null)//목표 유닛이 제거되면 이동을 종료하고 FindTargetAndMove()루프로 돌아가 다시 타겟을 찾아야 한다.
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(FindTargetAndMove());
            yield break;
        }
        Vector3Int currentTile = fightTilemap.WorldToCell(transform.position);//현재 타일맵 좌표를 셀 좌표로 변환.
        Vector3Int targetTIle = fightTilemap.WorldToCell(targetPositon);//목표지점 타일맵 좌표를 셀 좌표로 변환.

        Vector3Int nextMove = GetNextMove(currentTile, targetTIle);//두 타일 좌표 간 거리를 바탕으로 다음 이동할 타일을 결정.
        Vector3 worldMovePosition = fightTilemap.GetCellCenterWorld(nextMove);// 다음 이동할 타일의 Vector3Int형 좌표값을 타일 중심 위치 월드 좌표로 변환.

        //Debug.Log("MoveTowardTarget() called.");

        while(Vector2.Distance(transform.position, worldMovePosition) > 0.4f)// 현재 포지션 <-> 다음 이동할 타일 중심좌표 간 거리가 일치할 때 까지
        {
            if (targetUnit == null)//목표 유닛이 사라지면 이동 중지 후 다시 목표 유닛을 탐색한다.
            {
                if(moveCoroutine!=null) StopCoroutine(moveCoroutine);
                moveCoroutine = StartCoroutine(FindTargetAndMove());
                yield break; 
            } 
            transform.position = Vector2.MoveTowards(transform.position, worldMovePosition, moveSpeed * Time.deltaTime);//유닛 이동 속도로 이동하며 포지션 값을 변경한다.
            yield return null;//한 프레임 대기
        }

        combatAnimatorController.StopMove();//이동 종료 후 애니메이션 종료.
        combatAnimatorController.StartAutoBattle();
    }     

    public GameObject FindClosestUnit()//가장 가까운 유닛을 찾아 그 유닛의 트랜스폼을 리턴하는 메서드.
    {
        List<GameObject> units = FindPlayableUnits();
        GameObject closestUnit = null;//가장 가까운 유닛의 트랜스폼.
        float minDistance = Mathf.Infinity;//최소 거리는 양의 무한대 값으로. => 유닛 간의 거리 (2차원벡터값 Distance)가 양수가 되어야 최소 거리에 있는 유닛을 찾을 수 있기 때문.
        
        foreach(GameObject unit in units)
        {
            float distance = Vector2.Distance(transform.position, unit.transform.position);//현재 gameObject와 상대 유닛 간 거리를 저장.
            if(distance < minDistance)//양수 거리일 때
            {
                minDistance = distance;//최소 거리 설정
                closestUnit = unit;//가장 가까운 유닛 설정.
            }
        }
        //Debug.Log("FindClosestUnit() called.");
        return closestUnit;//가장 가까운 유닛을 리턴.   
    }

    private Vector3Int GetNextMove(Vector3Int current, Vector3Int target)//현재 타일에서 다음 이동할 타일을 결정하는 메서드.
    {
        //Debug.Log("GetNextMove() called.");
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

    private List<GameObject> FindPlayableUnits()// 기존 FindClosestUnit에서는 GameObject배열에서 FindGameObjectsWithTag를 사용했기 때문에 하나의 태그밖에 찾지 못했지만, 유닛들을 담는 공간을 리스트로 변경 및 플레이어블 캐릭터를 찾는 방법을 메서드로 빼내고 널체크를 추가.
    {
        List<GameObject> playableUnits = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit_Soldier"))
        {
            GameObject.FindGameObjectWithTag("Unit_Player"),
            GameObject.FindGameObjectWithTag("Unit_Hero")
        };
        playableUnits.RemoveAll(unit => unit == null); // null 체크 추가

        return playableUnits;
    }
}
