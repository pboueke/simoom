using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    // Player status
    private PlayerHealth _playerHealth;
    private PlayerWater _playerWater;
    // Drinking to heal 
    public float _drinkingRate = 10.0f;

    private PlayerSounds _sounds;

    void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerWater = GetComponent<PlayerWater>();
        _sounds = GetComponent<PlayerSounds>();

		DontDestroyOnLoad (this.gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float dTime = Time.deltaTime;

        bool isDrinking = Input.GetKey(KeyCode.Q);

        if (isDrinking && !_playerHealth.isFull() && !_playerWater.isDry())
        {
            _playerHealth.Heal(_playerWater.Drink(_drinkingRate * dTime));
        }
        else
        {
            isDrinking = false;
        }

        _sounds.Drink(isDrinking);
    }
}
