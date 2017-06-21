using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    public AudioClip[] hurt;
    public AudioClip dash;
    public AudioClip drink;

    //states
    bool isDrinking = false;

    private AudioSource _source;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Hurt()
    {
        int r = Random.Range(0, hurt.Length);
        _source.PlayOneShot(hurt[r]);
    }

    public void Dash()
    {
        _source.PlayOneShot(dash);
    }

    public void Drink(bool isDrinkingNow) {
        if (isDrinkingNow && !isDrinking)
        {
            _source.clip = drink;
            _source.loop = true;
            _source.Play();
            isDrinking = true;
        }
        else if (!isDrinkingNow && isDrinking)
        {
            _source.Stop();
            _source.loop = false;
            isDrinking = false;
        }
        
    }

    // Update is called once per frame
    void Update () {
		
	}
}
