using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Snek/Actions/Sleep")]
public class SnekSleepAction : Action {

	public override void Act(StateController controller)
	{
		Sleep (controller);
	}

	private void Sleep (StateController controller)
	{
		// Show sleep signal
		// or do whatever sleepy Sneks do
	}


}
