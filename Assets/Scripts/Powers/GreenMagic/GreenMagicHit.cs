using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMagicHit: MonoBehaviour {

    public ParticleSystem _explosionParticles;
    public float _damage = 10f;
    public float _maxLifeTime = 2f;

	// Use this for initialization
	private void Start () {
        Destroy(gameObject, _maxLifeTime);
	}

    private void Awake()
    {
        _explosionParticles.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if (health) {
			health.TakeDamage(_damage, transform.position.normalized);
        }

        //detach particle system from power
        _explosionParticles.transform.parent = null;
        //play particle animation
        _explosionParticles.Play();
        //destroy stuff
        Destroy(gameObject);
        Destroy(_explosionParticles.gameObject, _explosionParticles.main.duration);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
