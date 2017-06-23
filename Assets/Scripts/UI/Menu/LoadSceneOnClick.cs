using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public string playerObjectName = "Carpet";

	private GameObject player;

	void Start() {
		player = GameObject.Find (playerObjectName);
	}

	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	public void ForceLoadByIndex(int sceneIndex)
	{
		Destroy (player.gameObject);
		Destroy (GameObject.Find ("Canvas").gameObject);
		Destroy (GameObject.Find ("Main Camera").gameObject);

		LoadByIndex (sceneIndex);
	}
}