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
    private int _shotCount = 0;
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

    private void AugmentShot(Rigidbody shotInstance) {
        shotInstance.transform.localScale *= 1.5f;
    }

    private void Shoot(Quaternion rot, int augments, Quaternion angle) {
        Rigidbody shotInstance = Shot(rot);
        Vector3 velocity = shotVelocity * shotInstance.transform.forward;
        velocity = angle * velocity;
        for (int i = 0; i < augments; i++) {
            AugmentShot(shotInstance);
        }
        shotInstance.velocity = velocity;
    }

	private void ShootRegular(Quaternion rotation, int type) {
        List<int> angles = new List<int>();
        if (type == 0) {
            for (int i = -1; i < 2; i++) angles.Add(i*(15));
        } else if (type == 1) {
            for (int i = -2; i < 3; i++) angles.Add(i*(10));
        } else {
            angles.Add(-25);
            angles.Add(-15);
            angles.Add(-5);
            angles.Add(0);
            angles.Add(5);
            angles.Add(15);
            angles.Add(25);
        }
        foreach (int angle in angles) {
            Quaternion ang = Quaternion.AngleAxis(angle, Vector3.up);
            Shoot(rotation, 2-type, ang);
        }
		/*Vector3 velocity = firePointTransform.forward;
		velocity = Quaternion.AngleAxis(0 , Vector3.up) * velocity;*/
	}

	public void Fire(Transform target) {
		lastShotTime = 0f;
		Vector3 relativePos = target.position - firePointTransform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos);
		ShootRegular(rotation, _shotCount);
        _shotCount++;
        if (_shotCount == 3) _shotCount = 0;
	}
}
