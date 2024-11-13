using System;
using System.Collections;
using UnityEngine;

public class NumerOfHitsHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private int healthInHits = 5;

    [SerializeField]
    private float invulnerabilityTimeAfterEachHit = 50f;

    private int hitsRemaining;
    private bool canTakeDamage = true;

    public event Action<float> OnHPPctChanged = delegate (float f) { };
    public event Action OnDied = delegate { };

    public float CurrentHpPct
    {
        get { return (float)hitsRemaining / (float)healthInHits; }
    }

    private void Start()
    {
        hitsRemaining = healthInHits;
    }

    public void TakeDamage(int amount)
    {
        
        if (canTakeDamage)
        {
            StartCoroutine(InvunlerabilityTimer());

            hitsRemaining--;

            OnHPPctChanged(CurrentHpPct);

            if (hitsRemaining <= 0)
                OnDied();
        }
        
    }

    public void GainHealth(int amount)
    {
        if (canTakeDamage)
        {
            hitsRemaining++;

            OnHPPctChanged(CurrentHpPct);
        }
    }

    public void GetPoisoned(int amount)
    {
        if (canTakeDamage)
        {
            StartCoroutine(InvunlerabilityTimer());

            hitsRemaining--;

            OnHPPctChanged(CurrentHpPct);

            if (hitsRemaining <= 0)
                OnDied();
        }
    }

    private IEnumerator InvunlerabilityTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invulnerabilityTimeAfterEachHit);
        canTakeDamage = true;
    }
}
