using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public Rigidbody _shot;
    public Transform _fireTransform;
    public float _shotVelocity;
    public float _timeBetweenShots;

    private float _timer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && _timer >= _timeBetweenShots) {
            Fire();
        }
	}

    private void Fire() {
        _timer = 0f;

        Rigidbody shotInstance = Instantiate(_shot, _fireTransform.position, _fireTransform.rotation) as Rigidbody;

        shotInstance.velocity = _shotVelocity * _fireTransform.forward;
    }
}
