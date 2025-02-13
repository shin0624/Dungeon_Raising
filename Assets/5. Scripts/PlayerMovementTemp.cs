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
        // Ű���� �Է� (WASD �Ǵ� ����Ű)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // �밢�� �̵� �ӵ� ���� (����ȭ)
        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        // Rigidbody�� ����� �̵� (������ �浹 �ڵ� ó��)
        rb.velocity = moveInput * moveSpeed;
    }
}
