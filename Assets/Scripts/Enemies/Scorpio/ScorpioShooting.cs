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
    private ScorpioMovement _enemyMovement;
	private int _aggroLevel = 0;
    private int _shotCount = 0;
    private float _lastAngle = 0;
    private float _step = 7.5f;

    private static int[] _angles =  {0, 45, -45, 90, -90, 135, -135, 180};

    // Use this for initialization
    void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyMovement = GetComponent<ScorpioMovement>();
    }

    // Update is called once per frame
    void Update() {
        _timer += Time.deltaTime;

        Vector3 enemyPos = gameObject.transform.position;
        Vector3 playerPos = _player.transform.position;
        float distance = Vector3.Distance(enemyPos, playerPos);

        if (_timer >= _timeBetweenShots &&
                _enemyHealth.GetHealth() > 0f &&
                _playerHealth.GetHealth() > 0f &&
                distance <= _enemyMovement._visionRadius) {
            Fire();
        }
    }

	private void AugmentShot(Rigidbody shotInstance) {
		shotInstance.transform.localScale *= 2;
		shotInstance.velocity *= 0.8f;
		ScorpioVenom venom = shotInstance.GetComponent("ScorpioVenom") as ScorpioVenom;
		venom._damage *= 2;
	}

	public void ReceiveDeathAlert(int dead) {
		_aggroLevel = dead;
        if (_aggroLevel > 2 && _aggroLevel < 6) {
            _timeBetweenShots /= 2;
        }
	}

    private void Fire() {
        _timer = 0f;

        if (_aggroLevel >= 2) ShootFrenzy();
        else {
            _shotCount++;
            if (_shotCount == 10) {
                _shotCount = 0;
                ShootMultiple(_boss);
            } else {
                ShootRegular(_boss, 0);
                if (_aggroLevel > 0) ShootRegular(false, 0);
            }
        }
    }

    private Rigidbody Shot() {
        return Instantiate(_shot, _fireTransform.position, _fireTransform.rotation) as Rigidbody;
    }

	private void ShootRegular(bool bossShot, float angle) {
		Rigidbody shotInstance = Shot();
        Vector3 velocity = _fireTransform.forward;
        velocity = Quaternion.AngleAxis(angle, Vector3.up) * velocity;
        shotInstance.velocity = _shotVelocity * velocity;
        if (bossShot) AugmentShot(shotInstance);
	}

    private void ShootMultiple(bool bossShot) {
        for (int i = 0; i < 3; i++) {
            Rigidbody shotInstance = Shot();
            Vector3 velocity = _fireTransform.forward;
            velocity = Quaternion.AngleAxis((i-1)*20, Vector3.up) * velocity;
            shotInstance.velocity = _shotVelocity * velocity;
            AugmentShot(shotInstance);
            if (bossShot) AugmentShot(shotInstance);
        }
    }

    private void ShootFrenzy() {
        foreach (int angle in _angles) {
            Rigidbody shotInstance = Shot();
            Vector3 velocity = Vector3.forward;
            velocity = Quaternion.AngleAxis(angle+_lastAngle, Vector3.up) * velocity;
            shotInstance.velocity = _shotVelocity * velocity;
        }
        if (_shotCount == 7) {
            ShootRegular(_boss, 0);
            ShootRegular(_boss, Random.Range(-15f, 15f));
            ShootRegular(_boss, Random.Range(-15f, 15f));
            ShootRegular(_boss, Random.Range(-15f, 15f));
            ShootRegular(_boss, Random.Range(-15f, 15f));
            _shotCount = 0;
        }
        _shotCount++;
        _lastAngle += _step;
        if (Mathf.Abs(_lastAngle) > 30) _step = -_step;
    }
}
