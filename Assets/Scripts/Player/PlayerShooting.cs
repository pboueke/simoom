using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public Rigidbody _shot;
    public Transform _fireTransform;
    public float _shotVelocity;
    public float _timeBetweenShots;

    private float _timer;
    private float _extraPower;
    private int _density;
    private float _baseStep;
    private float _wideness;
    private List<float> _angles;
    private List<float> _steps;

    // Use this for initialization
    void Start () {
		_density = 0;
        _extraPower = 0f;
        _baseStep = 2f;
        _wideness = 60f;
        _angles = new List<float>();
        _steps = new List<float>();
	}
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer >= _timeBetweenShots) {
            Fire();
        }
	}

    private void Shoot(float angle, float powerDown, Vector3 offset) {
        Vector3 position = _fireTransform.position + offset;
        Rigidbody shotInstance = Instantiate(_shot, position, _fireTransform.rotation) as Rigidbody;
        Vector3 velocity = _fireTransform.forward;
        velocity = Quaternion.AngleAxis(angle, Vector3.up) * velocity;
        shotInstance.velocity = _shotVelocity * velocity;
        GreenMagicHit power = shotInstance.GetComponent<GreenMagicHit>();
        power.powerUp(_extraPower);
        power.powerDown(powerDown);
    }

    private void Fire() {
        _timer = 0f;

        Vector3 offset = _fireTransform.forward;
        offset = Quaternion.AngleAxis(90, Vector3.up) * offset;
        if (_density >= 4) {
            Shoot(0f, 0.4f, offset * 0);
            Shoot(0f, 0.4f, offset * 2);
            Shoot(0f, 0.4f, offset * -2);
        }
        else if (_density >= 1) {
            Shoot(0f, 0.5f, offset * 1f);
            Shoot(0f, 0.5f, offset * -1f);
        }
        else {
            Shoot(0f, 1.0f, offset * 0);
        }
        for (int i = 0; i < _angles.Count; i++) {
            // Shoot secondary shots
            Shoot(_angles[i], 0.2f, offset * 0);
            _angles[i] += _steps[i];
            if (Mathf.Abs(_angles[i]) >= _wideness / 2) {
                _steps[i] = -_steps[i];
            }
        }
    }

    public void increasePower() {
        _extraPower += 1.5f;
    }

    public void increaseDensity() {
        _density += 1;
        if (_density == 2 | _density == 3) {
            _angles.Clear();
            _steps.Clear();
            for (int i = 0; i < _density - 1; i++) {
                _angles.Add(0);
                if (i % 2 == 0) _steps.Add(_baseStep);
                else _steps.Add(-_baseStep);
            }
        }
    }
}
