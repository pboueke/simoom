using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public PlayerHealth _playerHealth;
    public GameObject _enemyPrefab;
    public float _spawnTime = 3f;
    public Transform[] _spawnPoints;

	private GameObject[] _enemies;

	// Use this for initialization
	void Start () {
        //Keep spawning enemies from _spawnTime to _spawnTime
		_enemies = new GameObject[3];
        InvokeRepeating("Spawn", 0.5f, _spawnTime);
	}

	void MakeBoss(GameObject instance) {
		instance.transform.localScale *= 2;
		EnemyHealth hp = (EnemyHealth) instance.GetComponent ("EnemyHealth");
		ScorpioShooting ss = (ScorpioShooting) instance.GetComponent ("ScorpioShooting");
		hp.SetHealth(hp._startingHealth*5);
		ss._boss = true;
	}

	GameObject MakeEnemy(Vector3 position, int spawnPointIndex) {
		return Instantiate(_enemyPrefab, position, _spawnPoints[spawnPointIndex].rotation);
	}

	void AlertDeath() {
		if (_enemies [1]) {
			ScorpioShooting ss = (ScorpioShooting)_enemies [1].GetComponent ("ScorpioShooting");
			ss.ReceiveDeathAlert ();
		}
	}

    void Spawn() {
        if (_playerHealth.GetHealth() <= 0f) {
            return;
        }

        int spawnPointIndex = Random.Range(0, _spawnPoints.Length);

		Vector3 basePos = _spawnPoints[spawnPointIndex].position;
		for (int i = 0; i < 3; i++) {
			Vector3 position = basePos + new Vector3 ((i-1)*3, 0, 0);
			GameObject instance = MakeEnemy (position, spawnPointIndex);
			instance.transform.parent = this.transform;
			_enemies [i] = instance;
			if (i == 1)
				MakeBoss (instance);
		}
    }
}
