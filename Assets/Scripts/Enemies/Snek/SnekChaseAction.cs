using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Snek/Actions/Chase")]
public class SnekChaseAction : Action {

	public override void Act(StateController controller)
	{
		Chase (controller);
	}

	private void Chase (StateController controller)
	{
		SnekBehaviour sb = controller.GetComponent<SnekBehaviour> ();

		sb.lastTargetSetTime += Time.deltaTime;
		if (sb.lastTargetSetTime > sb.targetSetInterval) {
			sb.lastTargetSetTime = 0;
			controller.navMeshAgent.destination = controller.chaseTarget.position;
			controller.navMeshAgent.destination += -controller.chaseTarget.forward * sb.forwardChasingRange;
			// straffing could not be achieved yet
			//float fluc = sb.targetPositionFluctuation;
			//controller.navMeshAgent.destination *= random...
			controller.navMeshAgent.Resume ();
		}
	}


}
