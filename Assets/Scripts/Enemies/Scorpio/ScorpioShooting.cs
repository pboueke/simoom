using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpioShooting : MonoBehaviour {

    public Rigidbody _shot;
    public Transform _fireTransform;
    public float _shotVelocity;
    public float _timeBetweenShots;

    private float _timer;

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeBetweenShots)
        {
            Fire();
        }
    }

    private void Fire()
    {
        _timer = 0f;

        Rigidbody shotInstance = Instantiate(_shot, _fireTransform.position, _fireTransform.rotation) as Rigidbody;

        shotInstance.velocity = _shotVelocity * _fireTransform.forward;
    }
}
