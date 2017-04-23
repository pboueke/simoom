using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float _startingHealth = 100f;
    public ParticleSystem _deathParticles;
	public Renderer rend;
	private float _currentHealth;
    private bool _dead;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake() {
        //_deathParticles = GetComponent<ParticleSystem> ();
		//rend = GetComponent<Renderer>();
        _deathParticles.Pause();
    } 

    private void OnEnable() {
		SetHealth(_startingHealth);
        _dead = false;
    }

    public void TakeDamage(float amount) {
        _currentHealth -= amount;
		updateHealthIndication ();
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
		updateHealthIndication ();
	}

	void updateHealthIndication() {
		Material[] ms = rend.materials;
		float healthLeft = _currentHealth / _startingHealth;
		float H, S, V;
		for (int i = 0; i < ms.Length; i++) {
			Color.RGBToHSV (ms [i].color, out H, out S, out V);
			rend.materials [i].color = Color.HSVToRGB (H, S*healthLeft, V);
		}
	}
}
