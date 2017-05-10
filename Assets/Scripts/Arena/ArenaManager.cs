using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour {

	public ArenaConfig _config;
	public PropManager _propManager;
	public GameObject _arenaFloor;

	// Use this for initialization
	void Start () {

		_arenaFloor.transform.localScale = new Vector3 (_config.arenaDiscScale, _config.arenaDiscScale, _config.arenaDiscScale);

		_propManager.SpawnAllProps (_config.propPoints);
	}
}
