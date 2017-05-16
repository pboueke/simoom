using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Guila/Decisions/Look")]
public class GuilaLook : Decision {

	public override bool Decide (StateController controller) 
	{
		bool targetVisible = Look (controller);
		return targetVisible;
	}

	private bool Look (StateController controller)
	{
		RaycastHit hit;
		Debug.DrawRay (controller.eyes.position + new Vector3(0,1,0), controller.eyes.forward.normalized * controller.enemyConfig.lookRange, Color.green);

		if (Physics.SphereCast (controller.eyes.position, controller.enemyConfig.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyConfig.lookRange)
			&& hit.collider.CompareTag ("Player")) {
			controller.chaseTarget = hit.transform;
			return true;
		} else 
		{
			return false;
		}
	}
}
