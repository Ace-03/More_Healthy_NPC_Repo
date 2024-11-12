using System;
using UnityEngine;

public class StandardHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int startingHealth = 100;

    private int currentHealth;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public float CurrentHpPct
    {
        get { return (float)currentHealth / (float)startingHealth; }
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        currentHealth -= amount;

        OnHPPctChanged(CurrentHpPct);

        if (CurrentHpPct <= 0)
            Die();

        Debug.Log(currentHealth);
    }

    public void GainHealth(int amount)
    {
        if (amount >= 100)
            throw new ArgumentOutOfRangeException("Invalid Health amount specified: " + amount);

        currentHealth += amount;

        OnHPPctChanged(CurrentHpPct);

        if (currentHealth >= 100)
        {
            currentHealth = 100;
        }

        Debug.Log(currentHealth);
    }

    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }
}