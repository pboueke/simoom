using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OasisBehaviour : MonoBehaviour {

    public string playerEventColliderName = "PlayerEventCollider";
	public string playerObjectName = "Carpet";
    public float waterVolume = 50.0f;
    public float fillingRate = 25.0f;

	private PlayerWater _playerWater;
    private bool inTrigger = false;
    private Transform water;
    private float waterLeft;
    private Vector3 waterFull;
    private Vector3 waterEmpty;


    // Use this for initialization
    void Start () {
		_playerWater = GameObject.Find (playerObjectName).GetComponent<PlayerWater> ();
	}

    private void Awake()
    {
        waterLeft = waterVolume;
        water = transform.Find("Water");
        waterFull.Set(0f, 0.09f, 0f);
        waterEmpty.Set(0f, 0f, 0f);
        water.localPosition = waterFull;
    }

    // Update is called once per frame
    void Update () {
        float dTime = Time.deltaTime;

        if (inTrigger && waterLeft >= 0.0 && !_playerWater.isFull()) {
            // Fill up player water
            float amount = Mathf.Min(fillingRate * dTime, waterLeft);
            waterLeft -= amount;
            _playerWater.Fill(amount);
            // Update water level
            float percentageLeft = waterLeft / waterVolume;
            water.localPosition = Vector3.Lerp(waterEmpty, waterFull, percentageLeft);
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == playerEventColliderName) {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == playerEventColliderName)
        {
            inTrigger = false;
        }
    }
}
