using UnityEngine;

public abstract class MoveEntity : GameEntity
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float patrolRadius = 2f;
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 targetPosition;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private float timer;
    private float waitTime = 5f;

    private void Awake()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition;
    }

    private void Update()
    {
        if (!IsInteracting)
            MoveLogic();

        timer -= Time.deltaTime;
    }

    private void MoveLogic()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if ((transform.position - targetPosition).sqrMagnitude < 0.1f && timer <= 0)
        {
            // 목표 지점에 도착했으므로 새로운 랜덤 목표 설정
            SetNextTarget();
            timer = waitTime;
        }

        UpdateAnimation();
    }

    private void SetNextTarget()
    {
        Vector3 randomOffset = (Vector3)Random.insideUnitCircle * patrolRadius;
        targetPosition = initialPosition + randomOffset;
    }

    private void UpdateAnimation()
    {
        Vector2 dir = targetPosition - transform.position;
        animator.SetBool(IsMoving, dir.magnitude > .5f);
        spriteRenderer.flipX = dir.x < 0;
    }
}
