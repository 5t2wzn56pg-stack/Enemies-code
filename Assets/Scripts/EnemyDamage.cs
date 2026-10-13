
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        SonicController sonic =
            collision.gameObject.GetComponentInParent<SonicController>();

        if (sonic == null)
            return;

        Rigidbody sonicRb =
            collision.gameObject.GetComponentInParent<Rigidbody>();

        EnemyHealth enemy = GetComponentInParent<EnemyHealth>();

        if (sonicRb == null || enemy == null)
            return;

        if (enemy.health <= 0)
            return;

        Collider enemyCollider = GetComponent<Collider>();
        Collider sonicCollider =
            collision.collider;

        if (enemyCollider == null || sonicCollider == null)
            return;

        float enemyTop = enemyCollider.bounds.max.y;
        float sonicBottom = sonicCollider.bounds.min.y;

        bool falling = sonicRb.linearVelocity.y <= 0.5f;
        bool aboveEnemy = sonicBottom >= enemyTop - 0.35f;

        if (falling && aboveEnemy)
        {
            print("SONIC STOMPED ENEMY!");

            enemy.TakeHit(1);

            Vector3 velocity = sonicRb.linearVelocity;
            velocity.y = 10f;
            sonicRb.linearVelocity = velocity;

            return;
        }

        SonicDamage sonicDamage =
            sonic.GetComponent<SonicDamage>();

        if (sonicDamage == null)
        {
            sonicDamage =
                sonic.GetComponentInParent<SonicDamage>();
        }

        if (sonicDamage != null)
        {
            sonicDamage.HitByEnemy();
        }
    }
}
