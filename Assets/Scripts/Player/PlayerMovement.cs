using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float _speed = 6f;
	Vector3 _movement;
	Animator _anim;
	Rigidbody _playerRigidbody;
	int _floorMask;
	float _camRayLength = 100f;

	// Gets called regardless of wether the script is enabled or not
	void Awake () {
		_floorMask = LayerMask.GetMask("Floor");
		_anim = GetComponent <Animator> ();
		_playerRigidbody = GetComponent <Rigidbody> ();
	}

	// Function which Unity will automatically call on its scripts
	//whenever there's a physics update
	void FixedUpdate () {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
        float f = Input.GetAxisRaw("Fire1");

        Move(h,v);
		Turning();
		Animating(h,v,f);
	}

	void Move (float h, float v) {
		_movement.Set(h, 0f, v);
		_movement = _movement.normalized * _speed * Time.deltaTime;
		_playerRigidbody.MovePosition(transform.position + _movement);
	}

	void Turning () {
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast(camRay, out floorHit, _camRayLength, _floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			_playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Animating (float h, float v, float f) {
		bool running = h!=0f || v!=0f;
		_anim.SetBool("isRunning", running);
        if (f != 0f) {
            _anim.SetTrigger("triggerShot");
        }
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
