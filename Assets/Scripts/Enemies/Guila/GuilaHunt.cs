using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Guila/Actions/Hunt")]
public class GuilaHunt : Action {

	public override void Act(StateController controller)
	{
		Hunt (controller);
	}

	private void Hunt (StateController controller)
	{
		GuilaBehaviour gb = controller.gameObject.GetComponent<GuilaBehaviour> ();
		if (!gb.emerged && !gb.isEmerging) {
			gb.Emerge ();
		} else  if (!gb.isEmerging) {
			controller.navMeshAgent.destination = controller.chaseTarget.position;
			controller.navMeshAgent.Resume ();
		}
	}


}
