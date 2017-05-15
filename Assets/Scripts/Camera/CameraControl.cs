using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour {

    public Transform _target;
    public float _smoothing = 5f;
	public bool _following = true;
	public string playerObjectName = "Carpet";

    private Vector3 _offset;

	// Use this for initialization
	void Start () {
		_offset = transform.position - _target.position;
		DontDestroyOnLoad (this.gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode) {
		// resets the target after a scene transition
		_target = GameObject.Find (playerObjectName).transform;
		_following = true;
	}

	
	// Update is called once per frame
	void FixedUpdate () {

		if (_following) {
			Vector3 targetCameraPosition = _target.position + _offset;

			transform.position = Vector3.Lerp (transform.position, targetCameraPosition,
				_smoothing * Time.deltaTime);
		}
	}
}
