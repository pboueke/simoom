using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public float _startingHealth = 100f;
    public ParticleSystem _deathParticles;
	public Renderer rend;
	public GameObject gateKeyGameObject;

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
        _sounds = _deathParticles.GetComponent<EnemySounds>();
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
        //play death sound
        if (_sounds != null)
            _sounds.Death();
        // get it`s experience value
        EnemyExperience exp = gameObject.GetComponent<EnemyExperience>();
		int xp = exp._experienceValue;
		if (exp.hasKey) {
            Vector3 keyPosition = transform.position;
            keyPosition.y = 2.0f;
            GameObject key = GameObject.Instantiate (gateKeyGameObject, keyPosition, transform.rotation);//GameObject.Find ("GateManager").transform);
		}
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
			rend.materials [i].color = Color.HSVToRGB (H, S, Mathf.Max(healthLeft, 0.3f));
		}
	}
}
