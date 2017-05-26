using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioSounds : EnemySounds {

    private float _lowPitchRange = .75F;
    private float _highPitchRange = 1.5F;

    public override void Hurt()
    {
        _source.pitch = Random.Range(_lowPitchRange, _highPitchRange);
        _source.PlayOneShot(hurt);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
