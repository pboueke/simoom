using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour {

	public GameObject village_one;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SpawnAllProps (propPositions[] propPoints) {

		foreach (propPositions spawn in propPoints) {

			// scorpio handler
			if (spawn.type.Equals ("village 1")) {
				Instantiate (village_one, spawn.position, new Quaternion ());
				continue;
			}
			else {
				Debug.LogWarning ("Specified prop is not recognizable: "+ spawn.type);
			}

		}
	}
}
