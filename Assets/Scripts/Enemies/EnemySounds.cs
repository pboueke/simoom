using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour {

    public AudioClip hurt;
    public AudioClip death;

    [HideInInspector] public AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    virtual public void Hurt() {
        _source.PlayOneShot(hurt);
    }

    virtual public void Death() {
        _source.PlayOneShot(death);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
