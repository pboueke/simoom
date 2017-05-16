using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Guila/Actions/Hide")]
public class GuilaHide : Action {

	public override void Act(StateController controller)
	{
		Hide (controller);
	}
	
	private void Hide (StateController controller)
	{
		// do something if hiding
	}
}
