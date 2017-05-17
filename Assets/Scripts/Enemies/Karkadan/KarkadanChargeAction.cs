using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Karkadan/Actions/Charge")]
public class KarkadanChargeAction : Action {

	public override void Act(StateController controller)
	{
		Charge (controller);
	}

	private void Charge (StateController controller)
	{
		KarkadanBehaviour kb = controller.gameObject.GetComponent<KarkadanBehaviour> ();
		if (!kb.isCharging) {
			kb.Charge ();
		}
		controller.navMeshAgent.destination = controller.chaseTarget.position;
	}
}
