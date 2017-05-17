using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Snek/Decisions/Look")]
public class SnekLookDecision : Decision {

	public override bool Decide (StateController controller) 
	{
		bool targetVisible = Look (controller);
		return targetVisible;
	}

	private bool Look (StateController controller)
	{
		Collider[] cols = Physics.OverlapSphere (controller.transform.position, controller.enemyConfig.lookRange);
		foreach (Collider col in cols) {
			if (col.CompareTag ("Player")) {
				controller.chaseTarget = col.transform;
				return true;
			}
		}
		return false;
	}
}
