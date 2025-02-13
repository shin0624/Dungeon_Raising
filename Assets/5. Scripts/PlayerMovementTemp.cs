using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTemp : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 키보드 입력 (WASD 또는 방향키)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // 대각선 이동 속도 조절 (정규화)
        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        // Rigidbody를 사용한 이동 (벽과의 충돌 자동 처리)
        rb.velocity = moveInput * moveSpeed;
    }
}
