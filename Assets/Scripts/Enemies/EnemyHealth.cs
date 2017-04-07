using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float _startingHealth = 100f;
    public ParticleSystem _deathParticles;
    private float _currentHealth;
    private bool _dead;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake() {
        //_deathParticles = GetComponent<ParticleSystem> ();
        _deathParticles.Pause();
    } 

    private void OnEnable() {
        _currentHealth = _startingHealth;
        _dead = false;
    }

    public void TakeDamage(float amount) {
        _currentHealth -= amount;
        if (_currentHealth <= 0f && !_dead) {
            OnDeath();
        }
    }

    private void OnDeath() {
        _dead = true;
        //detach particles from enemy
        _deathParticles.transform.parent = null;
        //play particle effect
        _deathParticles.Play();
        //destroy stuff
        Destroy(_deathParticles.gameObject, _deathParticles.main.duration);
        Destroy(gameObject);
		gameObject.SendMessageUpwards("AlertDeath");
    }

	// Update is called once per frame
	void Update () {
		
	}

    public float GetHealth() {
        return _currentHealth;
    }

	public void SetHealth(float hp) {
		_currentHealth = hp;
	}
}
