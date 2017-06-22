using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour {

	public string playerEventColliderName = "PlayerEventCollider";
	public string playerObjectName = "Carpet";
	public string referencedSceneName;
	public string type;
	public float transitionTime = 1f;
    public AudioClip levelChangeSound;


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
		GameObject aim = GameObject.Find ("AimCursor");

		if (!player.GetComponent<PlayerLevel> ()._hasKey) 
		{
            StartCoroutine(Blink (gameObject.transform.GetChild(0).gameObject,0.25f,4));
			return;
		}

        // play level change sound
        Camera.main.GetComponent<AudioSource>().PlayOneShot(levelChangeSound);

        // disable aim
        aim.SetActive(false);

		// new position, as if the camera is following the player direction moving away from the arena
		Vector3 newPos = transform.position * 3;

		// disable the camera target
		camera.GetComponent<CameraControl>()._following = false;
		// move the camera - moves it away
		StartCoroutine (MoveOverSeconds (camera, newPos, transitionTime * 0.9f));
		// set camera for the next scene
		StartCoroutine (MoveAfterSeconds (camera, -newPos, transitionTime * 0.95f));

		// disable player functions
		StartCoroutine (InactiveAfterSeconds(player, 0.1f));

		StartCoroutine(ChangeLevel(referencedSceneName, transitionTime));
		//SceneManager.LoadScene (referencedSceneName);
	}
		
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == playerEventColliderName)
		{
            inTrigger = true;
            showHighlight();
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.name == playerEventColliderName)
		{
            inTrigger = false;
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

	public IEnumerator InactiveAfterSeconds (GameObject obj, float seconds) {
		yield return new WaitForSeconds (seconds);
		obj.SetActive (false);
	}

	IEnumerator Blink(GameObject obj, float delayBetweenBlinks, int numberOfBlinks )  
	{
		bool state = false;
		int counter = 0;
		while( counter <= numberOfBlinks )
		{
			obj.SetActive (state);
			state = !state;
			counter++;
			yield return new WaitForSeconds( delayBetweenBlinks );
		}
		obj.SetActive (false);
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
