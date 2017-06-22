using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuilaBehaviour : MonoBehaviour {

	public float emergeSeconds = 0.5f;
	public float holeHeight = 0.5f;
	public float biteDamage = 1.5f;
	public float biteSecondsInterval = 1f;
	public float distanceToBite = 5f;
	public string playerObjectName = "Carpet";

	[HideInInspector] public float lastBiteTime;
	[HideInInspector] public bool emerged = false;
	[HideInInspector] public bool isEmerging = false;
	[HideInInspector] public GameObject target;

	private NavMeshAgent _nav;
    private GuilaSounds _sounds;

    private void Awake()
    {
        _sounds = GetComponent<GuilaSounds>();
    }

    public void Start () 
	{
		lastBiteTime = Time.deltaTime;
		target = GameObject.Find (playerObjectName);
		_nav = GetComponent<NavMeshAgent>();
		_nav.enabled = false;
	}

	public void Emerge ()
	{
		// move up
		_nav.enabled = false;
		isEmerging = true;
		Vector3 newPos = new Vector3 (transform.position.x, holeHeight, transform.position.z);
		StartCoroutine (EmergeOverSeconds(emergeSeconds));
		StartCoroutine (MoveOverSeconds(this.gameObject, newPos, emergeSeconds));
	}

    public void Bite()
    {
		if (_sounds != null)
        	_sounds.Bite();
    }

	private IEnumerator EmergeOverSeconds(float seconds) {
		yield return new WaitForSeconds (seconds);
		emerged = true;
		isEmerging = false;
		_nav.enabled = true;
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
