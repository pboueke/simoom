using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanelController : MonoBehaviour {

	private GameObject death;
	private GameObject victory;

	// Use this for initialization
	void Start () {
		death = this.gameObject.transform.GetChild (0).gameObject;
		victory = this.gameObject.transform.GetChild (1).gameObject;
	}

	private void ShowBackground() {
		Color aux = this.GetComponent<Image> ().color;
		aux.a = 0.5f;
		this.GetComponent<Image> ().color = aux;
	}

	private void ShowCursor() {
		Cursor.visible = true;
	}
	
	public void ShowDeathPanel() {
		if (death.activeSelf || victory.activeSelf) {
			return;
		}
		ShowBackground ();
		ShowCursor ();
		death.SetActive (true);
	}

	public void ShowVictoryPanel() {
		if (death.activeSelf || victory.activeSelf) {
			return;
		}
		ShowBackground ();
		ShowCursor ();
		victory.SetActive (true);
	}
}
