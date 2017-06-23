using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public GameObject player;

	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	public void ForceLoadByIndex(int sceneIndex)
	{
		Destroy (player);
		Destroy (GameObject.Find ("Canvas").gameObject);
		Destroy (GameObject.Find ("Main Camera").gameObject);

		LoadByIndex (sceneIndex);
	}
}