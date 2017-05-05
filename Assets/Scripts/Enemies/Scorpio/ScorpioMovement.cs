using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScorpioMovement : MonoBehaviour {

    public float _securityRadius;
    public float _visionRadius;

    private Transform _player;
    PlayerHealth _playerHealth;
    EnemyHealth _enemyHealth;
    NavMeshAgent _nav;
    Animator _anim;

    // Use this for initialization
    void Awake () {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();

        _nav.SetDestination(_player.position);
        _nav.Stop();
    }

    private bool Look (Vector3 origin, Vector3 forward) {
        RaycastHit hit;
        if (Physics.SphereCast(origin, 1, forward, out hit, _visionRadius) &&
                hit.collider.CompareTag("Player")) {
            return true;
        } else return false;
    }
	
	// Update is called once per frame
	void Update () {
        if (_enemyHealth.GetHealth() > 0f && _playerHealth.GetHealth() > 0f) {
            Vector3 enemyPos = gameObject.transform.position;
            Vector3 playerPos = _player.transform.position;
            Vector3 lookForward = (playerPos - enemyPos).normalized;
            float distance = Vector3.Distance(enemyPos, playerPos);

            bool clearSight = Look(enemyPos, lookForward);
            if (distance <= _visionRadius && (!clearSight || distance > _securityRadius)) {
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
