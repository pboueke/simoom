using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour {

	public GameObject EmptyGate;

	private List<GameObject> _gates;

	// Use this for initialization
	void Start () {
		_gates = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnAllGates (gatePosition[] gatePoints, float distance) {

		// For each configured gate, an gate is instanciated in a distance equal to disc radious
		// in the Z axis. Then it is rotate with its configured value around the point (0,0,0).
		// We also set the parameters for the gate.
		foreach (gatePosition spawn in gatePoints) {
			GameObject g = Instantiate (EmptyGate, new Vector3(0f, 4.5f, distance), new Quaternion ());
			g.transform.RotateAround (Vector3.zero, Vector3.up, spawn.angle);
			GateController gc = g.GetComponent<GateController> ();
			gc.referencedSceneName = spawn.scene;
			gc.type = spawn.type;
			continue;
		}
	}
}
