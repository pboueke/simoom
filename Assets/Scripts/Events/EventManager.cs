using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public GameObject oasis_one;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SpawnAllEvents (eventPositions[] eventPoints) {

		foreach (eventPositions spawn in eventPoints) {

			// scorpio handler
			if (spawn.type.Equals ("oasis")) {
				Instantiate (oasis_one, spawn.position, new Quaternion ());
				continue;
			}
			else {
				Debug.LogWarning ("Specified event is not recognizable: "+ spawn.type);
			}

		}
	}
}
