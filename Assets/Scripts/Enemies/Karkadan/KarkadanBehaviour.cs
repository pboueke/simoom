using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KarkadanBehaviour : MonoBehaviour {

	public float chargeDamage = 10f;
	public float damageSecondsInterval = 1f;
	public float chargePeriodInSeconds = 3f;
	public float chargeResetTimeInSeconds = 1f;
	public string playerObjectName = "Carpet";

	[HideInInspector] public float lastDamageTime;
	[HideInInspector] public bool isCharging;
	[HideInInspector] public bool canSleep;
	[HideInInspector] public bool canDamage;
	[HideInInspector] public GameObject target;

	private NavMeshAgent _nav;

	// Use this for initialization
	void Start () {
		target = GameObject.Find (playerObjectName);
		_nav = GetComponent<NavMeshAgent>();
		_nav.enabled = false;
		isCharging = false;
		canSleep = false;
		canDamage = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col) {
		if (col.CompareTag ("Player")) {
			canDamage = true;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.CompareTag ("Player")) {
			canDamage = false;
		}
	}

	public void Charge() {
		canSleep = false;
		isCharging = true;
		_nav.enabled = true;
		_nav.Resume ();
		StartCoroutine (EndChargeAfterSeconds (chargePeriodInSeconds));
		StartCoroutine (ReenableCharging (chargePeriodInSeconds + chargeResetTimeInSeconds));
	}

	private IEnumerator EndChargeAfterSeconds(float seconds) {
		yield return new WaitForSeconds (seconds);
		canSleep = true;
		_nav.updateRotation = true;
		_nav.Stop ();
	}

	private IEnumerator ReenableCharging (float seconds) {
		yield return new WaitForSeconds (seconds);
		isCharging = false;
	}
}
