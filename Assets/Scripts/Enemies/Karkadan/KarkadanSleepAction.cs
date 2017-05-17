using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Karkadan/Actions/Sleep")]
public class KarkadanSleep : Action {

	public override void Act(StateController controller)
	{
		Sleep (controller);
	}

	private void Sleep (StateController controller)
	{
		// Show sleep signal
		// or do whatever sleepy karkadans do
	}


}
