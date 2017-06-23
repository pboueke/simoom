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
		ShowDeathPanel (false);
		ShowVictoryPanel (false);
	}

	private void ShowBackground() {
		Color aux = this.GetComponent<Image> ().color;
		aux.a = 0.5f;
		this.GetComponent<Image> ().color = aux;
	}

	private void HideBackground() {
		Color aux = this.GetComponent<Image> ().color;
		aux.a = 0f;
		this.GetComponent<Image> ().color = aux;
	}

	private void ShowCursor() {
		Cursor.visible = true;
	}
	
	public void ShowDeathPanel(bool active = true) {
		if (death.activeSelf || victory.activeSelf) {
			return;
		}
		if (active) {
			ShowBackground ();
			ShowCursor ();
		}
		death.SetActive (active);
	}

	public void ShowVictoryPanel(bool active = true) {
		if (death.activeSelf || victory.activeSelf) {
			return;
		}
		if (active) {
			ShowBackground ();
			ShowCursor ();
		}
		victory.SetActive (active);
	}
}
