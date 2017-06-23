using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct enemySpawn {
	public string type;
	public string config;
	public Vector3 position;
}

[System.Serializable]
public struct propPosition {
	public string type;
	public bool randomRotation;
	public Vector3 position;
}

[System.Serializable]
public struct eventPosition {
	public string type;
	public string config;
	public Vector3 position;
}

[System.Serializable]
public struct gatePosition {
	public string type;
	public string scene;
	public float angle;
}

public class ArenaConfig : MonoBehaviour {

	public float arenaDiscScale = 140f;
	public float arenaTime = 240f;
	public bool finalArena = false;

	// Currently:
	// For each type of thing, there is a position where it must be located.

	public gatePosition[] gatePoints;
	public enemySpawn[] spawnPoints;
	public propPosition[] propPoints;
	public eventPosition[] eventPoints;

	// Use this for initialization
	void Start () {
		
	}

}
