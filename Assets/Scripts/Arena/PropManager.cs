using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct propRelation {
	public string name;
	public GameObject obj;
}

public class PropManager : MonoBehaviour {

	//list of prop gameopbjects, set at the inspector
	public propRelation[] propList;

	private Dictionary<string, GameObject> propDic = new Dictionary<string, GameObject>();

	public void SpawnAllProps (propPosition[] propPoints) {
		foreach (propRelation pr in propList) {
			propDic [pr.name] = pr.obj;
		}

		foreach (propPosition spawn in propPoints) {

			if (!propDic.ContainsKey (spawn.type)) {
				Debug.LogWarning ("Specified prop is not recognizable: "+ spawn.type);
				continue;
			}

			// any specific handler can be entered here

			// generic handler
			Vector3 pos = new Vector3 (spawn.position.x,propDic[spawn.type].transform.position.y,spawn.position.z);
			if (spawn.randomRotation) {
				Instantiate (propDic[spawn.type], pos, Quaternion.AngleAxis (Random.Range (0, 360), Vector3.up));
			}
			Instantiate (propDic[spawn.type], pos, propDic[spawn.type].transform.rotation);
		}
	}
}
