using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiledImageMovement : MonoBehaviour {

	public float x_speed;
	public float y_speed;

	private RawImage _img;

	// Use this for initialization
	void Start () {
		_img = this.GetComponent<RawImage> ();
	}
	
	void FixedUpdate () {
		_img.uvRect = new Rect (_img.uvRect.x + x_speed / 1000, _img.uvRect.y + y_speed / 1000, _img.uvRect.width, _img.uvRect.height);
	}
}
