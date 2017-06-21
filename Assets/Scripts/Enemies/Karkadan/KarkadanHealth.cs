using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarkadanHealth : EnemyHealth {

    [HideInInspector]
    new KarkadanSounds _ksounds;

    private void Awake()
    {
        _ksounds = GetComponent<KarkadanSounds>();
    }

    public override void TakeDamage(float amount, Vector3 direction) {

		float damageMultiplier = 1;
        bool hasHitShield = true;

		// adds extra damage if the shot hit from behind
		if (Vector3.Angle (direction, transform.forward) < 60) {
			damageMultiplier = GetComponent<KarkadanBehaviour> ().backDamageMultiplier;
            hasHitShield = false;
		}

        _ksounds.Hurt(hasHitShield);
			
		_currentHealth -= amount * damageMultiplier;
		updateHealthIndication ();
		if (_currentHealth <= 0f && !_dead) {
			OnDeath();
		}
	}

}
