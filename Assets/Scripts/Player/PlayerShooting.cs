using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

    public Rigidbody _shot;
    public Transform _fireTransform;
    public float _shotVelocity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp("Fire1")) {
            Fire();
        }
	}

    private void Fire() {
        Rigidbody shotInstance = Instantiate(_shot, _fireTransform.position, _fireTransform.rotation) as Rigidbody;

        shotInstance.velocity = _shotVelocity * _fireTransform.forward;
    }
}
