using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuilaSounds : EnemySounds {

    public AudioClip bite;

    private float _lowPitchRange = .75F;
    private float _highPitchRange = 1.5F;

    // Use this for initialization
    void Start () {
		
	}

    public void Bite() {
        _source.pitch = Random.Range(_lowPitchRange, _highPitchRange);
        _source.PlayOneShot(bite);
    }

    public override void Hurt()
    {
        _source.pitch = Random.Range(_lowPitchRange, _highPitchRange);
        _source.PlayOneShot(hurt);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
