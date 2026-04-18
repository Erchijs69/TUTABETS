using UnityEngine;
using System.Collections;

public class PointToPointWalker : MonoBehaviour
{
    [Header("Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;

    [Header("Idle")]
    public float idleTime = 2f;

    [Header("Animation")]
    public Animator animator;

    private Transform currentTarget;
    private Transform previousPoint;

    private bool isMoving = true;

    void Start()
    {
        // Start going to B from A
        transform.position = pointA.position;
        currentTarget = pointB;
        previousPoint = pointA;

        StartWalking();
    }

    void Update()
    {
        if (!isMoving) return;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position);
        float distance = direction.magnitude;

        // Normalize direction
        direction.Normalize();

        // Move
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Rotate toward movement direction
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

        // ARRIVAL CHECK (strict)
        if (distance <= 0.05f)
        {
            StartCoroutine(HandleArrival());
        }
    }

    IEnumerator HandleArrival()
{
    isMoving = false;

    // Snap EXACTLY to point
    transform.position = currentTarget.position;

    StopWalking();

    // --- SMOOTH ROTATION ---
    Vector3 lookDir = (previousPoint.position - transform.position).normalized;

    if (lookDir != Vector3.zero)
    {
        Quaternion targetRot = Quaternion.LookRotation(lookDir);

        // Rotate until aligned
        while (Quaternion.Angle(transform.rotation, targetRot) > 0.5f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );

            yield return null; // wait next frame
        }

        // Final snap (very small correction)
        transform.rotation = targetRot;
    }

    // --- IDLE AFTER ROTATION ---
    yield return new WaitForSeconds(idleTime);

    // Swap points
    Transform temp = previousPoint;
    previousPoint = currentTarget;
    currentTarget = temp;

    StartWalking();
    isMoving = true;
}

    void StartWalking()
    {
        if (animator != null)
            animator.SetBool("isWalking", true);
    }

    void StopWalking()
    {
        if (animator != null)
            animator.SetBool("isWalking", false);
    }
}
