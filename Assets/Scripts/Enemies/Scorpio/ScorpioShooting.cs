using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioShooting : MonoBehaviour {

    public Rigidbody _shot;
    public Transform _fireTransform;
    public float _shotVelocity;
    public float _timeBetweenShots;

    private float _timer;
    private Transform _player;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;

    // Use this for initialization
    void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update() {
        _timer += Time.deltaTime;

        if (_enemyHealth.GetHealth() > 0f && _playerHealth.GetHealth() > 0f && _timer >= _timeBetweenShots) {
            Fire();
        }
    }

    private void Fire() {
        _timer = 0f;

        Rigidbody shotInstance = Instantiate(_shot, _fireTransform.position, _fireTransform.rotation) as Rigidbody;

        shotInstance.velocity = _shotVelocity * _fireTransform.forward;
    }
}
