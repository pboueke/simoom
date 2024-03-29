﻿using System.Collections;
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
		Collider[] cols = Physics.OverlapSphere (controller.transform.position, controller.enemyConfig.lookRange);
		//Debug.DrawRay (controller.eyes.position + new Vector3(0,1,0), controller.eyes.forward.normalized * controller.enemyConfig.lookRange, Color.green);

		//if (Physics.SphereCast (controller.eyes.position, controller.enemyConfig.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyConfig.lookRange)
		foreach (Collider col in cols) {
			if (col.CompareTag ("Player")) {
				controller.chaseTarget = col.transform;
				return true;
			}
		}
		return false;
	}
}
