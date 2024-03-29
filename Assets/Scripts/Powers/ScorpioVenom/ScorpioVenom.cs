﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioVenom : MonoBehaviour {

    public ParticleSystem _explosionParticles;
    public float _damage = 10f;
    public float _maxLifeTime = 2f;

    // Use this for initialization
    private void Start()
    {
        Destroy(gameObject, _maxLifeTime);
    }

    private void Awake()
    {
        _explosionParticles.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health) {
            health.TakeDamage(_damage);
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
    void Update()
    {

    }
}
