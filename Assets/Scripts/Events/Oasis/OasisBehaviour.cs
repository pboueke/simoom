using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OasisBehaviour : MonoBehaviour {

    public string playerEventColliderName = "PlayerEventCollider";
    public PlayerWater playerWater;
    public float waterVolume = 50.0f;
    public float fillingRate = 25.0f;

    private bool inTrigger = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float dTime = Time.deltaTime;

        if (inTrigger && waterVolume >= 0.0 && !playerWater.isFull()) {
            float amount = Mathf.Min(fillingRate * dTime, waterVolume);
            waterVolume -= amount;
            playerWater.Fill(amount);
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
