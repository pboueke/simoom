using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioGroup {
    public List<GameObject> _scorpios;
    public List<GameObject> _bosses;

    public ScorpioGroup() {
        _scorpios = new List<GameObject>();
        _bosses = new List<GameObject>();
    }
}

public class ScorpioManager : MonoBehaviour {
	
	public float _bossMultiplier = 3;
	public GameObject _enemyPrefab;

	[HideInInspector]
    public List<ScorpioGroup> _groups = new List<ScorpioGroup>();

	private EnemyManager _em;

	// Use this for initialization
	void Start () {
		_em = this.transform.parent.GetComponent<EnemyManager> ();
	}

	/// <summary>
	/// Receives every death alert from dying scorpios and redirects the message where it needs to go.
	/// In this case, any alive boss instance and the enemy manager.
	/// </summary>
	void AlertDeath(int experienceGained) {
        foreach (ScorpioGroup sg in _groups) {
            int dead = 0;
            foreach (GameObject s in sg._scorpios) if (s == null) dead++;
            foreach (GameObject b in sg._bosses) {
                if (b == null) continue;
                ScorpioShooting ss = b.GetComponent<ScorpioShooting>();
                ss.ReceiveDeathAlert(dead);
            }
        }

		// sends message to the nemey manager
		_em.AlertDeath(experienceGained);
	}

	/// <summary>
	/// Creates an enemy scorpio.
	/// </summary>
	/// <param name="spawnPoint"> Selected spawnpoint transform.</param>
	/// <param name="position"> Position where to create the enemy.</param>
	GameObject MakeEnemy(Vector3 position) {
		return Instantiate(_enemyPrefab, position, new Quaternion());
	}

	/// <summary>
	/// Turns the selected instance a boss.
	/// </summary>
	/// <param name="instance"> Selected scorpio instance.</param>
	void MakeBoss(GameObject instance) {
		instance.transform.localScale *= 2;
		EnemyHealth hp = (EnemyHealth) instance.GetComponent("EnemyHealth");
		EnemyExperience xp = (EnemyExperience)instance.GetComponent("EnemyExperience");
		ScorpioShooting ss = (ScorpioShooting) instance.GetComponent("ScorpioShooting");
		hp._startingHealth = hp._startingHealth * _bossMultiplier;
		hp.SetHealth(hp._startingHealth);
		xp._experienceValue = (int)((float)xp._experienceValue * _bossMultiplier);
		ss._boss = true;
	}

	/// <summary>
	/// Spawms a number of scorpios
	/// </summary>
	/// <param name="spawnPoint"> Position of the selected spawm point.</param>
	/// <param name="enemyNumber"> Number of enemies to be created.</param>
	/// <param name="bossNumber"> Number of the enemies to be converted to a boss.</param>
	public void Spawn(Vector3 spawnPoint, int enemyNumber, int bossNumber, bool hasKey = false) {
		bool usedKey = false;
        ScorpioGroup enemyGroup = new ScorpioGroup();
		for (int i = 0; i < enemyNumber; i++) {
			Vector3 position = spawnPoint + new Vector3((i-1)*3, 0, 0);
			GameObject instance = MakeEnemy(position);
			instance.transform.parent = this.transform;

			if (!usedKey && hasKey) {
				instance.GetComponent<EnemyExperience> ().hasKey = true;
				usedKey = true;
			}

			if (i < bossNumber) {
                enemyGroup._bosses.Add (instance);
				MakeBoss (instance);
			} else {
				enemyGroup._scorpios.Add (instance);
			}
		}
        _groups.Add(enemyGroup);
	}
}
