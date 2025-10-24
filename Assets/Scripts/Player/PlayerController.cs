using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;
    private Animator animator;

    private Vector2 movementDirection;
    private float speed = 3f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rigid.velocity = movementDirection * speed;
    }

    private void LateUpdate()
    {
        animator.SetBool(IsMoving, movementDirection.magnitude > .5f);

        if (movementDirection.x != 0)
            spriteRenderer.flipX = movementDirection.x < 0;
    }

    private void OnMove(InputValue inputValue)
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }
}
