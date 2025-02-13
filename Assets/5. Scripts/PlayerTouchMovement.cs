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
        targetPosition = transform.position;  // ���� ��ġ�� �ʱ�ȭ
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // ��ġ ������ ��
            {
                // ��ġ�� ��ġ�� ���� ��ǥ�� ��ȯ
                targetPosition = Camera.main.ScreenToWorldPoint(touch.position);
                isMoving = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // �÷��̾ Ÿ�� ��ġ�� �̵�
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            // �����ߴ��� Ȯ��
            if ((Vector2)transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}
