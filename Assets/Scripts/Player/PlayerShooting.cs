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

    private void Shoot(float angle, bool secondary) {
        Rigidbody shotInstance = Instantiate(_shot, _fireTransform.position, _fireTransform.rotation) as Rigidbody;
        Vector3 velocity = _fireTransform.forward;
        velocity = Quaternion.AngleAxis(angle, Vector3.up) * velocity;
        shotInstance.velocity = _shotVelocity * velocity;
        GreenMagicHit power = shotInstance.GetComponent<GreenMagicHit>();
        power.powerUp(_extraPower);
        if (secondary) {
            power.powerDown();
        }
    }

    private void Fire() {
        _timer = 0f;

        Shoot(0f, false);
        for (int i = 0; i < _density; i++) {
            // Shoot secondary shots
            Shoot(_angles[i], true);
            _angles[i] += _steps[i];
            if (Mathf.Abs(_angles[i]) >= _wideness / 2) {
                _steps[i] = -_steps[i];
            }
        }
    }

    public void increasePower() {
        _extraPower += 2f;
    }

    public void increaseDensity() {
        _angles.Clear();
        _steps.Clear();
        _density += 1;
        float step = _wideness / (_density + 1);
        float angle = -(_wideness / 2);
        for (int i = 0; i < _density; i++) {
            angle += step;
            _angles.Add(angle);
            if (angle > 0) _steps.Add(_baseStep);
            else if (angle < 0) _steps.Add(-_baseStep);
            else _steps.Add(0f);
        }
        Debug.Log(_angles.Count);
        Debug.Log(_steps.Count);
    }
}
