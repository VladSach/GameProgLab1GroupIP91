using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement2D : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D body;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement")]
    [SerializeField] private float speed = 75f;
    [SerializeField] private float jumpHeight = 35f;
    [SerializeField] private float castingLength = 0.9f;
    [SerializeField] private bool isJumped;

    private float horizontalMovement;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMovement = GetInput().x;
        if (Input.GetButtonDown("Jump") && isJumped) Jump();
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        Move();
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Move()
    {
        body.AddForce(new Vector2(horizontalMovement, 0f) * speed);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, 0f);
        body.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }

    private void CheckCollisions()
    {
        isJumped = Physics2D.Raycast(transform.position * castingLength,
                                     Vector2.down, castingLength, groundLayer);
    }

    // Debug
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.down * castingLength);
    //}

}
