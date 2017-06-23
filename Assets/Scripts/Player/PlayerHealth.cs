using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public float _startingHealth = 100f;
    private float _currentHealth;
    private bool _dead;
    public Slider _slider;                             // The slider to represent how much health the tank currently has.
    public Image _fillImage;                           // The image component of the slider.
    public Color _fullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color _zeroHealthColor = Color.red;         // The color the health bar will be when on no health.

	// Player's Animator Controller
	private Animator _anim;

    private PlayerSounds _sound;
    private Camera _camera;

    // Use this for initialization
    private void Awake()
    {
		_currentHealth = _startingHealth;
        _sound = GetComponent<PlayerSounds>();
        _camera = Camera.main;
		_anim = GetComponent <Animator> ();
    }

    private void OnEnable()
    {
        _dead = false;

        // Update the health slider's value and color.
        SetHealthUI();
    }

    private void OnDeath()
    {
        _dead = true;
        //gameObject.SetActive(false);
        _camera.GetComponent<AudioSource>().PlayOneShot(_sound.death);
        //Destroy(gameObject);
		GameObject.Find ("EndGamePanel").GetComponent<EndGamePanelController>().ShowDeathPanel();
    }

    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        _slider.value = _currentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        _fillImage.color = Color.Lerp(_zeroHealthColor, _fullHealthColor, _currentHealth / _startingHealth);
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _sound.Hurt();
		_anim.SetTrigger ("Hurt");

        // Change the UI elements appropriately.
        SetHealthUI ();

        if (_currentHealth <= 0f && !_dead)
        {
            OnDeath();
        }
    }

    public void Heal(float amount)
    {
        // Heal as much as possible until health is full
        _currentHealth = Mathf.Min(_currentHealth+amount, _startingHealth);
        SetHealthUI();
    }

    // return whether health is full or not
    public bool isFull()
    {
        return _currentHealth >= _startingHealth;
    }

    // get current health value
    public float GetHealth()
    {
        return _currentHealth;
    }

	private void onSceneLoaded() {
		SetHealthUI ();
	}

}
