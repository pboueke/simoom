using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateKeyBehasviour : MonoBehaviour {

	public string playerEventColliderName = "PlayerEventCollider";
	public string playerObjectName = "Carpet";

	private GameObject _player;
	private PlayerMovement _pm;
	private ArenaConfig _arenaConfig;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.Find(playerObjectName);
		_pm = _player.GetComponent<PlayerMovement> ();
		_arenaConfig = GameObject.Find ("GameManager").GetComponentInChildren<ArenaConfig> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!_pm.InArena (transform, _arenaConfig.arenaDiscScale)) {
			// if the player is not in the arena, lets put him there
			float distance = Vector3.Distance(Vector3.zero, transform.position) - _arenaConfig.arenaDiscScale;
			Vector3 direction = (Vector3.zero - transform.position).normalized;
			transform.position += distance * direction;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == playerEventColliderName) 
		{
			_player.GetComponent<PlayerLevel> ()._hasKey = true;
            _player.GetComponent<PlayerSounds>().GotKey();
			GameObject.Find ("KeyIcon").GetComponent<Image> ().enabled = true;
			gameObject.SetActive (false);
		}
	}
}
