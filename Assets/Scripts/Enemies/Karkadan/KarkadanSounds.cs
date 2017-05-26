using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarkadanSounds : EnemySounds {

    public AudioClip[] roars;
    public AudioClip shieldHit;
    public AudioClip skinHit;

    private float _lowPitchRange = .75F;
    private float _highPitchRange = 1.5F;

    // Use this for initialization
    void Start () {
		
	}

    public void Roar() {
        int r = Random.Range(0, roars.Length);
        _source.pitch = Random.Range(_lowPitchRange, _highPitchRange);
        _source.PlayOneShot(roars[r]);
    }

    public void Hurt(bool hasHitShield)
    {
        _source.pitch = Random.Range(_lowPitchRange, _highPitchRange);
        if (hasHitShield)
        {
            _source.PlayOneShot(shieldHit);
        }
        else
        {
            _source.PlayOneShot(skinHit);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
