using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScorpioMovement : MonoBehaviour {

    public float _securityRadius;

    private Transform _player;
    EnemyHealth _enemyHealth;
    NavMeshAgent _nav;
    Animator _anim;

    // Use this for initialization
    void Awake () {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _enemyHealth = GetComponent<EnemyHealth>();
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();

        _nav.SetDestination(_player.position);
        _nav.Stop();
    }
	
	// Update is called once per frame
	void Update () {
        if (_enemyHealth.getHealth() > 0f) {
            if (
                Vector3.Distance(
                    gameObject.transform.position,
                    _player.transform.position
                ) > _securityRadius
                )
            {
                _anim.SetBool("isMoving", true);
                _nav.SetDestination(_player.position);
                _nav.Resume();
            }
            else {
                _anim.SetBool("isMoving", false);
                _nav.Stop();
                _nav.transform.LookAt(_player.position);
            }            
        }
	}
}
