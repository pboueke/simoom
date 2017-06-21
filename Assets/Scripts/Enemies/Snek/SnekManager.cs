using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekManager : MonoBehaviour {

	public GameObject _enemyPrefab;

	[HideInInspector]
	public List<GameObject> _sneks;

	private EnemyManager _em;

	// Use this for initialization
	void Start () {
		_sneks = new List<GameObject> ();
		_em = this.transform.parent.GetComponent<EnemyManager> ();
	}

	void AlertDeath(int experienceGained) {
		// sends message to the nemey manager
		_em.AlertDeath(experienceGained);
	}

	GameObject MakeEnemy(Vector3 position) {
		return Instantiate(_enemyPrefab, position, new Quaternion());
	}

	public void Spawn(Vector3 spawnPoint, int enemyNumber, bool hasKey=false) {
		bool usedKey = false;
		for (int i = 0; i < enemyNumber; i++) {
			Vector3 position = spawnPoint + new Vector3((i-1)*3, 0, 0);
			GameObject instance = MakeEnemy(position);
			instance.transform.parent = this.transform;

			if (!usedKey && hasKey) {
				instance.GetComponent<EnemyExperience> ().hasKey = true;
				usedKey = true;
			}

			_sneks.Add (instance);
		}	
	}
}
