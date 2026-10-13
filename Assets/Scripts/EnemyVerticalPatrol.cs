using UnityEngine;

public class EnemyVerticalPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float lowerBoundary = -2f;
    public float upperBoundary = 2f;

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
        nextPosition.y += direction * speed * Time.fixedDeltaTime;

        if (nextPosition.y >= upperBoundary)
        {
            direction = -1;
            nextPosition.y = upperBoundary;
        }
        else if (nextPosition.y <= lowerBoundary)
        {
            direction = 1;
            nextPosition.y = lowerBoundary;
        }

        rb.MovePosition(nextPosition);
    }
}