using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	public string playerObjectName = "Carpet";

	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	public void ForceLoadByIndex(int sceneIndex)
	{
		GameObject player = null;
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		foreach (GameObject go in allObjects) {
			if (go.name == playerObjectName) {
				player = go;
				break;
			}
		}

		Destroy (player);
		Destroy (GameObject.Find ("Canvas").gameObject);
		Destroy (GameObject.Find ("Main Camera").gameObject);

		LoadByIndex (sceneIndex);
	}
}