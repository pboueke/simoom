using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
	private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
	{
		transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
			_camera.transform.rotation * Vector3.up);
	}
}