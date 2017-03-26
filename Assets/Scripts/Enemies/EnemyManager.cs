using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public PlayerHealth _playerHealth;
    public GameObject _enemyPrefab;
    public float _spawnTime = 3f;
    public Transform[] _spawnPoints;

	// Use this for initialization
	void Start () {
        //Keep spawning enemies from _spawnTime to _spawnTime
        InvokeRepeating("Spawn", _spawnTime, _spawnTime);
	}

    void Spawn() {
        if (_playerHealth.GetHealth() <= 0f) {
            return;
        }

        int spawnPointIndex = Random.Range(0, _spawnPoints.Length);

        Instantiate(_enemyPrefab, _spawnPoints[spawnPointIndex].position, 
            _spawnPoints[spawnPointIndex].rotation);
    }
}
