using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarkadanHealth : EnemyHealth {

	public override void TakeDamage(float amount, Vector3 direction) {

		float damageMultiplier = 1;

		// adds extra damage if the shot hit from behind
		if (Vector3.Angle (direction, transform.forward) < 120) {
			damageMultiplier = GetComponent<KarkadanBehaviour> ().backDamageMultiplier;
		}

		Debug.Log( amount * damageMultiplier);
		_currentHealth -= amount * damageMultiplier;
		updateHealthIndication ();
		if (_currentHealth <= 0f && !_dead) {
			OnDeath();
		}
	}

}
