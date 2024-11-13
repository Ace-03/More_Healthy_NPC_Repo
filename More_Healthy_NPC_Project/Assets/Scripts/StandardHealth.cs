using System;
using UnityEngine;

public class StandardHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int startingHealth = 100;

    private int currentHealth;
    
    private bool isPoisoned;

    public int _amount = 1;

    public Renderer playerMat;

    public GameObject Damage_Holder;
    TextController textController;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
        playerMat = GetComponent<Renderer>();

        textController = Damage_Holder.GetComponent<TextController>();
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

        textController.setText("-", amount);

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

        textController.setText("+", amount);
        Debug.Log(currentHealth);
    }

    public void GetPoisoned(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Health amoutn specified: " + amount);
        
        isPoisoned = !isPoisoned;

        if(isPoisoned)
            playerMat.material.color = Color.green;
        else
            playerMat.material.color = Color.yellow;

        
        _amount = amount / 10;

        Debug.Log("Get Poisoned");
    }

    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        if (isPoisoned)
        {
            TakeDamage(_amount);
            textController.setText("-", _amount);
        }
    }
}