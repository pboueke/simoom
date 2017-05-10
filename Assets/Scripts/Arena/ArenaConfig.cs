using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct enemySpawns {
	public string type;
	public string config;
	public Vector3 position;
}

[System.Serializable]
public struct propPositions {
	public string type;
	public Vector3 position;
}

[System.Serializable]
public struct eventPositions {
	public string type;
	public string config;
	public Vector3 position;
}

public class ArenaConfig : MonoBehaviour {

	public float arenaDiscScale = 140f;

	// This may change. Could be a good a idea to read configs from a file.
	// vvvvvvvvv
	// Currently:
	// For each type of thing, there is a position where it must be located.

	public enemySpawns[] spawnPoints;
	public propPositions[] propPoints;
	public enemySpawns[] eventPoints;

	// Use this for initialization
	void Start () {
		
	}

}
