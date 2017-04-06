using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioShooting : MonoBehaviour {

    public Rigidbody _shot;
    public Transform _fireTransform;
    public float _shotVelocity;
    public float _timeBetweenShots;
	public bool _boss = false;

    private float _timer;
    private Transform _player;
    private PlayerHealth _playerHealth;
    private EnemyHealth _enemyHealth;
	private int _aggroLevel = 0;

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

	private void AugmentShot() {
		_shot.transform.localScale *= 2;
		_shotVelocity *= 0.5f;
		ScorpioVenom venom = (ScorpioVenom) _shot.GetComponent ("ScorpioVenom");
		venom._damage *= 10;
	}

	private void NormalizeShot() {
		_shot.transform.localScale *= 0.5f;
		_shotVelocity *= 2;
		ScorpioVenom venom = (ScorpioVenom) _shot.GetComponent ("ScorpioVenom");
		venom._damage *= 0.1f;
	}

	public void ReceiveDeathAlert() {
		_aggroLevel += 1;
	}

    private void Fire() {
        _timer = 0f;

		if (_boss) AugmentShot ();
		Shoot ();
		if (_boss) NormalizeShot ();
		if (_aggroLevel > 0) Shoot ();
    }

	private void Shoot() {
		Debug.Log (_aggroLevel.ToString ());Debug.Log (_aggroLevel.ToString ());
		Rigidbody shotInstance = Instantiate(_shot, _fireTransform.position, _fireTransform.rotation) as Rigidbody;
		shotInstance.velocity = _shotVelocity * _fireTransform.forward;
	}
}
