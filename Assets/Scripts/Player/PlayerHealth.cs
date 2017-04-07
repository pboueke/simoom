using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float _startingHealth = 100f;
    private float _currentHealth;
    private bool _dead;
    public Slider _slider;                             // The slider to represent how much health the tank currently has.
    public Image _fillImage;                           // The image component of the slider.
    public Color _fullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color _zeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        

    // Use this for initialization
    private void Awake()
    {
    }

    private void OnEnable()
    {
        _currentHealth = _startingHealth;
        _dead = false;

        // Update the health slider's value and color.
        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        // Change the UI elements appropriately.
        SetHealthUI ();

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

    public float GetHealth()
    {
        return _currentHealth;
    }

    private void SetHealthUI ()
    {
        // Set the slider's value appropriately.
        _slider.value = _currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        _fillImage.color = Color.Lerp (_zeroHealthColor, _fullHealthColor, _currentHealth / _startingHealth);
    }
}
