using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuilaManager : MonoBehaviour {

	public GameObject _enemyPrefab;

	[HideInInspector]
	public List<GameObject> _guilas;

	private EnemyManager _em;

	// Use this for initialization
	void Start () {
		_guilas = new List<GameObject> ();
		_em = this.transform.parent.GetComponent<EnemyManager> ();
	}
	
	void AlertDeath(int experienceGained) {
		// sends message to the nemey manager
		_em.AlertDeath(experienceGained);
	}

	GameObject MakeEnemy(Vector3 position) {
		Quaternion randomQuar = Quaternion.Euler (0, Random.Range (0, 360), 0);
		Vector3 positionAdjustment = new Vector3 (0, -1.5f, 0);
		return Instantiate(_enemyPrefab, position + positionAdjustment, randomQuar);
	}

	public void Spawn(Vector3 spawnPoint, bool hasKey=false) {
		GameObject instance = MakeEnemy(spawnPoint);
		instance.transform.parent = this.transform;
		instance.GetComponent<EnemyExperience> ().hasKey = hasKey;
	}
}
