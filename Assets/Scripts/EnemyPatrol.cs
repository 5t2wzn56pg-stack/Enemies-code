
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float leftBoundary = -4f;
    public float rightBoundary = 4f;

    private Rigidbody rb;
    private int direction = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        Vector3 nextPosition = rb.position;
        nextPosition.x += direction * speed * Time.fixedDeltaTime;

        if (nextPosition.x >= rightBoundary)
        {
            direction = -1;
            nextPosition.x = rightBoundary;
        }
        else if (nextPosition.x <= leftBoundary)
        {
            direction = 1;
            nextPosition.x = leftBoundary;
        }

        rb.MovePosition(nextPosition);
    }
}
