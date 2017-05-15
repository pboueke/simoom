using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour {

	public string playerEventColliderName = "PlayerEventCollider";
	public string playerObjectName = "Carpet";
	public string referencedSceneName;
	public string type;
	public float transitionTime = 1f;


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
			doTransition ();
		}
	}

	void doTransition () {

		GameObject player = GameObject.Find(playerObjectName);
		GameObject camera = GameObject.Find ("Main Camera");

		// new position, as if the camera is following the player direction moving away from the arena
		Vector3 newPos = transform.position * 3;

		// disable the camera target
		camera.GetComponent<CameraControl>()._following = false;

		// move the camera - moves it away
		StartCoroutine (MoveOverSeconds (camera, newPos, transitionTime * 0.9f));
		// set camera for the next scene
		StartCoroutine (MoveAfterSeconds (camera, -newPos, transitionTime * 0.95f));

		// set the direction where the player will be sent to in the new level
		player.transform.position = (Vector3.zero - player.transform.position).normalized;

		StartCoroutine(ChangeLevel(referencedSceneName, transitionTime));
		//SceneManager.LoadScene (referencedSceneName);
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

	public IEnumerator ChangeLevel(string levelName, float seconds) {
		yield return new WaitForSeconds (seconds);
		SceneManager.LoadScene (levelName);
	}

	public IEnumerator MoveAfterSeconds (GameObject obj, Vector3 pos, float seconds) {
		yield return new WaitForSeconds (seconds);
		obj.transform.position = pos;
	}

	public IEnumerator MoveOverSpeed (GameObject obj, Vector3 pos, float speed) {
		while (obj.transform.position != pos) {
			obj.transform.position = Vector3.MoveTowards (obj.transform.position, pos, speed * Time.deltaTime);
			yield return new WaitForEndOfFrame ();
		}
	}

	public IEnumerator MoveOverSeconds (GameObject obj, Vector3 pos, float seconds) {
		float elapsedTime = 0;
		Vector3 startingPos = obj.transform.position;
		while (elapsedTime < seconds) {
			obj.transform.position = Vector3.Lerp (startingPos, pos, (elapsedTime / seconds));
			elapsedTime += Time.deltaTime;
			yield return new WaitForEndOfFrame ();
		}
		obj.transform.position = pos;
	}

}
