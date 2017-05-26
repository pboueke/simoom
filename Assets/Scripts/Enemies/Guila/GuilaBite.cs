using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Guila/Actions/Bite")]
public class GuilaBite : Action {

	public override void Act(StateController controller)
	{
		Bite (controller);
	}

	private void Bite (StateController controller)
	{
		// bites if:
		// * guila is over ground and
		// * is in range and
		// * has not acted in the last interval
		GuilaBehaviour gb = controller.gameObject.GetComponent<GuilaBehaviour> ();
		gb.lastBiteTime += Time.deltaTime;
		if (gb.emerged && controller.navMeshAgent.remainingDistance < gb.distanceToBite &&
			gb.lastBiteTime > gb.biteSecondsInterval) {
			controller.navMeshAgent.updateRotation = true;
            gb.Bite();
			gb.lastBiteTime = 0;
			gb.target.GetComponent<PlayerHealth> ().TakeDamage (gb.biteDamage);
		}
	}
}
