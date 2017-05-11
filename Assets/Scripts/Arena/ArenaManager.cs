using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour {

	public ArenaConfig _config;
	public PropManager _propManager;
	public EventManager _eventManager;
	public GateManager _gateManager;
	public GameObject _arenaFloor;

	// Use this for initialization
	void Start () {

		// set disk size
		_arenaFloor.transform.localScale = new Vector3 (_config.arenaDiscScale, _config.arenaDiscScale, _config.arenaDiscScale);

		// add things to the arena
		_propManager.SpawnAllProps (_config.propPoints);
		_eventManager.SpawnAllEvents (_config.eventPoints);
		_gateManager.SpawnAllGates (_config.gatePoints, _config.arenaDiscScale);
	}
}
