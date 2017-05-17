﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public string playerObjectName = "Carpet";
	public ArenaConfig _config;

	private GameObject _player;
	private PlayerHealth _playerHealth;
	private PlayerLevel _playerLevel;

	// Enemy Handlers
	public ScorpioManager scorpios;
	public GuilaManager guilas;
	public KarkadanManager karkadans;
	public SnekManager sneks;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find (playerObjectName);
		_playerHealth = _player.GetComponent<PlayerHealth> ();
		_playerLevel = _player.GetComponent<PlayerLevel> ();
		SpawnAll ();
	}
		

	public void AlertDeath(int experienceGained) {
        //add experience
        _playerLevel.addXp(experienceGained);
	}

	void SpawnAll () {
		if (_playerHealth.GetHealth () <= 0f) {
			return;
		}

		// iterates over all the configured spawn points
		foreach (enemySpawn spawn in _config.spawnPoints) {
			
			// scorpio handler
			if (spawn.type.IndexOf ("scorpio") > -1) {
				string[] cfg = spawn.config.Split (new char[] { ' ' });
				int units = int.Parse (cfg [0]);
				int bosses = 0;
				if (cfg.Length > 1) {
					bosses = int.Parse (cfg [1]);
				}
				scorpios.Spawn (spawn.position, units, bosses);
			} else if (spawn.type.IndexOf ("guila") > -1) {
				guilas.Spawn (spawn.position);
			} else if (spawn.type.IndexOf ("karkadan") > -1) {
				karkadans.Spawn (spawn.position);
			} else if (spawn.type.IndexOf ("snek") > -1) {
				int units = int.Parse (spawn.config);
				sneks.Spawn (spawn.position, units);
			}
			// other enemy handlers
			// else if...
			else {
				Debug.LogWarning ("Specified enemy spawn is not recognizable: "+ spawn.type);
			}

		}
	}
}