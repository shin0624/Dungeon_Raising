using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;  // 현재 위치로 초기화
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // 터치 시작할 때
            {
                // 터치한 위치를 월드 좌표로 변환
                targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
                isMoving = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // 플레이어를 타겟 위치로 이동
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            // 도착했는지 확인
            if ((Vector2)transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}
