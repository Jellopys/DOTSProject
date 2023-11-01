using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class HealthComponent : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private float health;

    private CapsuleCollider capsuleCollider;

    void Awake()
    {
        health = maxHealth;
        capsuleCollider = GetComponent<CapsuleCollider>();
    }


    public void TakeDamage(float damage)
    {
        
        Mathf.Clamp(health -= damage, 0, maxHealth);
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("dead");
    }
}

public interface IHealth
{

    void TakeDamage(float damage);
    void Death();
}
