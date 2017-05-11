using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
	
	//list of event gameopbjects, set at the inspector
	public GameObject oasis_one;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SpawnAllEvents (eventPosition[] eventPoints) {

		foreach (eventPosition spawn in eventPoints) {

			// oasis handler
			if (spawn.type.Equals ("oasis")) {
				// if spawn.config is set, it can be used to configure this event
				Instantiate (oasis_one, spawn.position, new Quaternion ());
				continue;
			}
			else {
				Debug.LogWarning ("Specified event is not recognizable: "+ spawn.type);
			}

		}
	}
}
