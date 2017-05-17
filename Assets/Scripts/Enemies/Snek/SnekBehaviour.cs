using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekBehaviour : MonoBehaviour {

	// this sets how forward the snek target should be in relation to it target
	public float forwardChasingRange = 40f;
	public Rigidbody shotBody;
	public Transform firePointTransform;
	public float shotVelocity;
	public float intervalBetweenShots;

	// this sets the random variation range of the position the Snek user for chase
	// disabled untill figured out: public float targetPositionFluctuation = 15f;

	[HideInInspector] public float lastTargetSetTime = 0f;
	[HideInInspector] public float targetSetInterval = 15f;
	[HideInInspector] public float lastShotTime = 0f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		lastShotTime += Time.deltaTime;

	}

	private Rigidbody Shot(Quaternion rotation) {
		return Instantiate(shotBody, firePointTransform.position, rotation) as Rigidbody;
	}

	private void ShootRegular(Quaternion rotation) {
		Rigidbody shotInstance = Shot(rotation);
		/*Vector3 velocity = firePointTransform.forward;
		velocity = Quaternion.AngleAxis(0 , Vector3.up) * velocity;*/
		shotInstance.velocity = shotVelocity * shotInstance.transform.forward;
	}

	public void Fire(Transform target) {
		lastShotTime = 0f;
		Vector3 relativePos = target.position - firePointTransform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
		ShootRegular(rotation);
	}
}
