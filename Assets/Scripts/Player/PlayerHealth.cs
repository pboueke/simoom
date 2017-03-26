using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float _startingHealth = 100f;
    private float _currentHealth;
    private bool _dead;

    // Use this for initialization
    private void Awake()
    {
    }

    private void OnEnable()
    {
        _currentHealth = _startingHealth;
        _dead = false;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0f && !_dead)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        _dead = true;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public float getHealth()
    {
        return _currentHealth;
    }
}
