using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public PlayerHealth _playerHealth;
    public PlayerLevel _playerLevel;

	public ArenaConfig _config;

	// Enemy Handlers
	public ScorpioManager scorpios;

	// Use this for initialization
	void Start () {
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
		foreach (enemySpawns spawn in _config.spawnPoints) {
			
			// scorpio handler
			if (spawn.type.IndexOf ("scorpio") > -1) {
				string[] cfg = spawn.config.Split (new char[] { ' ' });
				int units = int.Parse (cfg [0]);
				int bosses = 0;
				if (cfg.Length > 1) {
					bosses = int.Parse (cfg [1]);
				}
				scorpios.Spawn (spawn.position, units, bosses);
				continue;
			}
			// other enemy handlers
			// else if...
			else {
				Debug.LogWarning ("Specified enemy spawn is not recognizable: "+ spawn.type);
			}

		}
	}
}