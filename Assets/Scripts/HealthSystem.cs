using UnityEngine;

public class HealthSystem : MonoBehaviour,IHitable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnHit(Collider attacker, float damage)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject,0.5f);
    }
}
