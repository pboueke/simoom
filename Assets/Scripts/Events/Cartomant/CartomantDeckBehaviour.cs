using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartomantDeckBehaviour : MonoBehaviour {

    public bool yetToBeUsed = true;

    private GameObject _player;
    private PlayerHealth _playerHealth;

    // effect tunnig
    private ParticleSystem Effect0_Explosion;
    public float Effectt0_ExplosionDamage = 10.0f;

    // Use this for initialization
    void Start () {
        // general init
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();

        // effects
        Effect0_Explosion = transform.GetComponentInChildren<ParticleSystem>();

    }

    void Update() { }

    public void applyEffect()
    {
        // choose our *random* effect
        int eventId = 0; // set random id

        switch (eventId)
        {
            case 0:
                applyPlayerExplosion();
                break;
        }

        yetToBeUsed = false;
    }

    // effect functions

    // Effect Id: 0
    // Description: small explosion on player dealing some damage
    private void applyPlayerExplosion()
    {
        _playerHealth.TakeDamage(Effectt0_ExplosionDamage);
        Effect0_Explosion.Play();
        Destroy(Effect0_Explosion.gameObject, Effect0_Explosion.main.duration);
    }
}
