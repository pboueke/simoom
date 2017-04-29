using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWater : MonoBehaviour {

    public float _startingWater = 100f;
    private float _currentWater;
    private bool _dry;
    public Slider _slider;                             // The slider to represent how much health the tank currently has.
    public Image _fillImage;                           // The image component of the slider.
    public Color _fullWaterColor = Color.green;       // The color the health bar will be when on full health.
    public Color _zeroWaterColor = Color.red;         // The color the health bar will be when on no health.

    private void OnEnable()
    {
        _currentWater = _startingWater;
		_dry = false;
        // Update the health slider's value and color.
        SetWaterUI();
    }

    private void OnDry()
    {
        _dry = true;
        //Destroy(gameObject);
    }

    private void SetWaterUI()
    {
        // Set the slider's value appropriately.
        _slider.value = _currentWater;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        _fillImage.color = Color.Lerp(_zeroWaterColor, _fullWaterColor, _currentWater / _startingWater);
    }

    public void Drain(float amount)
    {
		if (_dry) {
			return;
		}
        _currentWater -= amount;
        _dry = false;

        // Change the UI elements appropriately.
        SetWaterUI();

        if (_currentWater <= 0f && !_dry)
        {
            OnDry();
        }
    }

    public void Fill(float amount)
    {
        _currentWater = Mathf.Min(_currentWater + amount, _startingWater);
		_dry = false;
        // Change the UI elements appropriately.
        SetWaterUI();
    }

    public float Drink(float amount) {
		if (_dry) {
			return 0.0f;
		}
        // If amount we wanted to drink is greater than how
        //much water we have, drink all the remaining water.
        float sip = Mathf.Min(amount, _currentWater);
        // Drain amount we sipped
        Drain(sip);
        // Return amount we sipped
        return sip;
    }

    // Return whether we are dried out
    public bool isDry() {
        return _dry;
    }

    // Return whether we are full of water
    public bool isFull()
    {
        return _currentWater >= _startingWater;
    }

    // Return amount of available water
    public float GetWater()
    {
        return _currentWater;
    }
}
