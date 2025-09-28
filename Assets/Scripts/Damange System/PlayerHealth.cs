using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHealth = 5;
    [SerializeField] float invulnSeconds = 0.5f;

    public int Current { get; private set; }
    public int Max => maxHealth;

    public event Action<int,int> OnHealthChanged; // (current, max)
    public event Action OnDied;

    bool invulnerable;

    void Awake()
    {
        Current = Mathf.Clamp(Current == 0 ? maxHealth : Current, 0, maxHealth);
        OnHealthChanged?.Invoke(Current, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (invulnerable || amount <= 0) return;
        Current = Mathf.Max(0, Current - amount);
        OnHealthChanged?.Invoke(Current, maxHealth);

        if (Current <= 0) Die();
        else StartCoroutine(IFrames());
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;
        Current = Mathf.Min(maxHealth, Current + amount);
        OnHealthChanged?.Invoke(Current, maxHealth);
    }

    IEnumerator IFrames()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnSeconds);
        invulnerable = false;
    }

    void Die()
    {
        OnDied?.Invoke();
        // Example: disable controls / respawn later
        // gameObject.SetActive(false);
    }
}