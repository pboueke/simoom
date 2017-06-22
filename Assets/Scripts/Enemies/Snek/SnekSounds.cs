using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekSounds : EnemySounds {

    private float _lowPitchRange = .90F;
    private float _highPitchRange = 1.1F;

    // Use this for initialization
    void Start () {
		
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
