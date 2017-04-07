using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Player movement speed
	public float _speed = 6f;

    // How much speed boost does the player get when dashing?
    public float _dashMultiplier = 3f;
    // For how much time does it dash?
    public float _dashDuration = 1f;
    // How many consecutive times can he dash?
    public int _dashLimit = 2;
    // How long does it take to recover a dash?
    public float _dashCooldown = 2f;

    // Is the player dashing?
    private bool _dashing;
    // Direction of the current dash
    private Vector3 _dashDirection;
    // For how long has the player been dashing
    private float _dashDurationTimer;
    // Timer to reload dashes
    private float _dashCooldownTimer;
    // How many dashes does the player have left?
    private int _dashesAvailable;

    // Direction of current movement
    private Vector3 _movement;
    // Player's Animator Controller
    private Animator _anim;
    // Player's Rigid Body
    private Rigidbody _playerRigidbody;
    // Layer mask for floor, used for mouse position fetching
    private int _floorMask;
    // Maximum distance that camera ray can reach to find intersection with floor and thus the mouse position
    private float _camRayLength = 100f;

	// Gets called regardless of wether the script is enabled or not
	void Awake () {
		_floorMask = LayerMask.GetMask("Floor");
		_anim = GetComponent <Animator> ();
		_playerRigidbody = GetComponent <Rigidbody> ();
        _dashing = false;
        _dashesAvailable = _dashLimit;
	}

	// Function which Unity will automatically call on its scripts
	//whenever there's a physics update
	void FixedUpdate () {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
        bool f = Input.GetButton("Fire1");
        bool d = Input.GetButtonDown("Fire2");

        Move(h,v,d);
		Turning();
		Animating(h,v,f,d);
	}

	void Move (float h, float v, bool d) {
        float dTime = Time.deltaTime;

        if (_dashing) {
            _dashDurationTimer += dTime;

            if (_dashDurationTimer < _dashDuration) {
                // player is dashing and there's dashing time left
                _movement = _dashDirection * _speed * _dashMultiplier * dTime;
                _playerRigidbody.MovePosition(transform.position + _movement);
            }
            else {
                // player dashing time exceeded limit during dash: compose dash and normal movement
                float dashTimeLeft = dTime - (_dashDurationTimer - _dashDuration);
                _movement.Set(h, 0f, v);
                _movement = _movement.normalized * _speed * (dTime - dashTimeLeft);
                _movement += _dashDirection * _speed * _dashMultiplier * dashTimeLeft;
                _playerRigidbody.MovePosition(transform.position + _movement);
                _dashing = false;
                _dashCooldownTimer = 0f;
            }
        }
        else {
            // if player has spent dashes, reload them on cooldown
            if (_dashesAvailable < _dashLimit) {
                _dashCooldownTimer += dTime;
                if (_dashCooldownTimer >= _dashCooldown) {
                    _dashCooldownTimer = _dashCooldownTimer - _dashCooldown;
                    _dashesAvailable = Mathf.Min(_dashesAvailable + 1, _dashLimit);
                    if (_dashesAvailable >= _dashLimit) {
                        _dashCooldownTimer = 0f;
                    }
                }
            }

            if (d && _dashesAvailable > 0) {
                // player is trying to dash and can dash
                _dashing = true;
                _dashesAvailable--;
                _dashDurationTimer = dTime;
                _dashDirection.Set(h, 0f, v);
                _dashDirection = _dashDirection.normalized;
                _movement = _dashDirection * _speed * _dashMultiplier * dTime;
                _playerRigidbody.MovePosition(transform.position + _movement);
            }
            else {
                // player is not dashing nor trying to dash
                _movement.Set(h, 0f, v);
                _movement = _movement.normalized * _speed * dTime;
                _playerRigidbody.MovePosition(transform.position + _movement);
            }
        }
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

	void Animating (float h, float v, bool f, bool d) {
		bool running = h!=0f || v!=0f;
		_anim.SetBool("isRunning", running);
        if (f) {
            _anim.SetTrigger("triggerShot");
        }
	}
}
