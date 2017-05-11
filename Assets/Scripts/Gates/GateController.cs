using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour {

	public string playerEventColliderName = "PlayerEventCollider";
	public string playerObjectName = "Carpet";
	public string referencedSceneName;
	public string type;


	private bool inTrigger = false;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		hideHighLight();
		transform.RotateAround (Vector3.zero, Vector3.up, 0);
	}


	// Update is called once per frame
	void Update () {
		if (inTrigger && Input.GetKeyDown(KeyCode.E))
		{
			//DontDestroyOnLoad (GameObject.Find(playerObjectName));
			SceneManager.LoadScene (referencedSceneName);
		}
	}
		
	void OnTriggerEnter(Collider col)
	{
		inTrigger = true;

		if (col.gameObject.name == playerEventColliderName)
		{
			showHighlight();
		}
	}

	void OnTriggerExit(Collider col)
	{
		inTrigger = false;

		if (col.gameObject.name == playerEventColliderName)
		{
			hideHighLight();
		}
	}

	// highlight funcions
	private void showHighlight()
	{
		rend.material.color = Color.HSVToRGB(0.5f,1.0f,0.8f);

	}

	private void hideHighLight()
	{
		rend.material.color = Color.HSVToRGB(0.0f, 1.0f, 0.8f);
	}
}
