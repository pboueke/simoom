using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Karkadan/Actions/DoDamage")]
public class KarkadanDamageAction : Action {
	public override void Act(StateController controller)
	{
		DoDamage (controller);
	}

	private void DoDamage (StateController controller)
	{
		KarkadanBehaviour kb = controller.gameObject.GetComponent<KarkadanBehaviour> ();
		kb.lastDamageTime += Time.deltaTime;
		if (kb.isCharging && kb.canDamage && kb.lastDamageTime > kb.damageSecondsInterval) {
			kb.lastDamageTime = 0;
			kb.target.GetComponent<PlayerHealth> ().TakeDamage (kb.chargeDamage);
		}
	}
}
