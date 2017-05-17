using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Snek/Actions/Shoot")]
public class SnekShootAction : Action {

	public override void Act(StateController controller)
	{
		Shoot (controller);
	}

	private void Shoot (StateController controller)
	{
		SnekBehaviour sb = controller.GetComponent<SnekBehaviour> ();

		sb.lastShotTime += Time.deltaTime;
		if (sb.lastShotTime > sb.intervalBetweenShots) {
			sb.Fire (controller.chaseTarget);
		}
	}
}
