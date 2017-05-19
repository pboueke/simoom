using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour {

	public float rotationSpeed = 1f;
	public float maxSize = 4f;
	public float minSize = 2f;
	public float growthFactor = 1f;

	private float scalingFactor = 1f;
	private bool increasing = true;
	private Plane disc;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		disc = new Plane (Vector3.up, GameObject.Find ("Disc").transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//rotate
		transform.Rotate (Vector3.forward * rotationSpeed * Time.deltaTime);

		//scale
		if (transform.localScale.sqrMagnitude > maxSize) {
			increasing = false;
		}
		if (transform.localScale.sqrMagnitude < minSize) {
			increasing = true;
		}
		scalingFactor = (increasing) ? growthFactor : -growthFactor;
		transform.localScale *= (1 + scalingFactor * Time.deltaTime);

		//position
		float dist;
		Ray ray = 	Camera.main.ScreenPointToRay(Input.mousePosition);
		if (disc.Raycast(ray, out dist)) {
			dist -= 0.1f;
			transform.position = ray.GetPoint (dist);
		}
	}
}
