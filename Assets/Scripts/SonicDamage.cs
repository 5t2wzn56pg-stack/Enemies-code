
using UnityEngine;

public class SonicDamage : MonoBehaviour
{
    private bool isDead = false;
    private bool isInvincible = false;

    public float knockbackForce = 8f;
    public float invincibilityDuration = 1.5f;

    private Rigidbody rb;
    private SonicController sonicController;
    private Collider[] colliders;
    private Renderer[] renderers;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sonicController = GetComponent<SonicController>();

        colliders = GetComponentsInChildren<Collider>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void HitByEnemy()
    {
        if (isDead || isInvincible)
            return;

        // If Sonic has rings, lose them and survive.
        if (sonicController != null && sonicController.rings > 0)
        {
            sonicController.rings = 0;

            print("Sonic lost all his rings!");

            isInvincible = true;

            if (rb != null)
            {
                rb.linearVelocity = new Vector3(
                    rb.linearVelocity.x,
                    0f,
                    rb.linearVelocity.z
                );

                rb.AddForce(
                    new Vector3(0f, 1f, -0.25f) * knockbackForce,
                    ForceMode.VelocityChange
                );
            }

            CancelInvoke(nameof(EndInvincibility));
            Invoke(nameof(EndInvincibility), invincibilityDuration);

            return;
        }

        // Sonic has zero rings: die.
        Die();
    }

    private void EndInvincibility()
    {
        isInvincible = false;
    }

    private void Die()
    {
        if (isDead)
            return;

        isDead = true;

        print("SONIC HAS ZERO RINGS! GAME OVER!");

        if (sonicController != null)
            sonicController.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        foreach (Collider col in colliders)
            col.enabled = false;

        foreach (Renderer rend in renderers)
            rend.enabled = false;
    }
}
