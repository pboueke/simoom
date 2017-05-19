using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingConfiguration : MonoBehaviour {

	private GameObject cursor;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		cursor = GameObject.Find ("AimCursor");
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode) {
		cursor.SetActive(true);
	}
}
