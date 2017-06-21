using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float _startingHealth = 100f;
    public ParticleSystem _deathParticles;
	public Renderer rend;

	// public for access of derived classes
	[HideInInspector] public float _currentHealth;
	[HideInInspector] public bool _dead;
    [HideInInspector] public EnemySounds _sounds;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake() {
        //_deathParticles = GetComponent<ParticleSystem> ();
		//rend = GetComponent<Renderer>();
        _deathParticles.Pause();
        _sounds = GetComponent<EnemySounds>();
    } 

    private void OnEnable() {
		SetHealth(_startingHealth);
        _dead = false;
    }

    public virtual void TakeDamage(float amount, Vector3 direction) {
        _currentHealth -= amount;
		if (_sounds != null)
        	_sounds.Hurt();
		updateHealthIndication ();
        if (_currentHealth <= 0f && !_dead) {
            OnDeath();
        }
    }

    public void OnDeath() {
        _dead = true;
        //detach particles from enemy
        _deathParticles.transform.parent = null;
        //play particle effect
        _deathParticles.Play();
        // get it`s experience value
		EnemyExperience exp = gameObject.GetComponent<EnemyExperience>();
		int xp = exp._experienceValue;
        //destroy stuff
        Destroy(_deathParticles.gameObject, _deathParticles.main.duration);
        Destroy(gameObject);

		// sends the message to this enemy manager (the one that spawned this instance)
		gameObject.SendMessageUpwards("AlertDeath", xp);
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

	public void updateHealthIndication() {
		Material[] ms = rend.materials;
		float healthLeft = _currentHealth / _startingHealth;
		float H, S, V;
		for (int i = 0; i < ms.Length; i++) {
			Color.RGBToHSV (ms [i].color, out H, out S, out V);
			rend.materials [i].color = Color.HSVToRGB (H, S*healthLeft, V);
		}
	}
}
