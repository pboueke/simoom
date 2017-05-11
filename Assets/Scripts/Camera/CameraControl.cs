using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform _target;
    public float _smoothing = 5f;

    private Vector3 _offset;

	// Use this for initialization
	void Start () {
        _offset = transform.position - _target.position;
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 targetCameraPosition = _target.position + _offset;

        transform.position = Vector3.Lerp(transform.position, targetCameraPosition,
            _smoothing * Time.deltaTime);
	}
}
