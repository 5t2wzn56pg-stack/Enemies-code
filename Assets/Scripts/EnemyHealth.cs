
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    public float respawnDelay = 15f;

    private Vector3 startingPosition;
    private Quaternion startingRotation;
    private Rigidbody rb;
    private bool isDead;

    private bool originalKinematic;
    private bool originalUseGravity;

    private EnemyPatrol patrol;
    private EnemyVerticalPatrol verticalPatrol;
    private EnemyDamage enemyDamage;

    private Collider[] colliders;
    private Renderer[] renderers;

    private void Awake()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            originalKinematic = rb.isKinematic;
            originalUseGravity = rb.useGravity;
        }

        patrol = GetComponent<EnemyPatrol>();
        verticalPatrol = GetComponent<EnemyVerticalPatrol>();
        enemyDamage = GetComponent<EnemyDamage>();

        colliders = GetComponentsInChildren<Collider>(true);
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    public void TakeHit(int damage)
    {
        if (isDead)
            return;

        health -= damage;

        if (health <= 0)
        {
            StartCoroutine(RespawnEnemy());
        }
    }

    private IEnumerator RespawnEnemy()
    {
        isDead = true;

        if (patrol != null)
            patrol.enabled = false;

        if (verticalPatrol != null)
            verticalPatrol.enabled = false;

        if (enemyDamage != null)
            enemyDamage.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        foreach (Collider col in colliders)
            col.enabled = false;

        foreach (Renderer rend in renderers)
            rend.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        transform.position = startingPosition;
        transform.rotation = startingRotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = originalUseGravity;
            rb.isKinematic = originalKinematic;
        }

        health = 1;
        isDead = false;

        foreach (Renderer rend in renderers)
            rend.enabled = true;

        foreach (Collider col in colliders)
            col.enabled = true;

        if (enemyDamage != null)
            enemyDamage.enabled = true;

        if (patrol != null)
            patrol.enabled = true;

        if (verticalPatrol != null)
            verticalPatrol.enabled = true;
    }
}
