using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour {

    public float _startingDash = 200f;
    public float _neededDash = 100f;
    public float _dashCooldownTime = 2f;
    private float _currentDash;
    private bool _hasDash;
    public Slider _slider;  // The slider to represent how much dash the tank currently has.
    public Image _fillImage;  // The image component of the slider.
    public Color _fullDashColor = Color.green;  // The color the dash bar will be when available.
    public Color _emptyDashColor = Color.black; // The color the dash bar will be when empty.
    public Color _chargeDashColor = Color.red;  // The color the dash bar will be when not available.

    private PlayerSounds _sound;

    private void Awake()
    {
        _sound = GetComponent<PlayerSounds>();
    }

    // Update is called once per frame
    void Update ()
    {
        float increase = Time.deltaTime * _neededDash / _dashCooldownTime;
        Fill(increase);
    }

    private void OnEnable()
    {
        _currentDash = _startingDash;
        _hasDash = true;
        // Update the health slider's value and color.
        SetDashUI();
    }

    private void SetDashUI()
    {
        // Set the slider's value appropriately.
        _slider.value = _currentDash;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        float fraction = _currentDash / _neededDash;
        if (fraction >= 1f)
            _fillImage.color = _fullDashColor;
        else
            _fillImage.color = Color.Lerp(_emptyDashColor, _chargeDashColor, fraction);
    }

    public void Consume()
    {
        if (!_hasDash) return;

        _sound.Dash();

        _currentDash = Mathf.Max(_currentDash - _neededDash, 0f);

        if (_currentDash < _neededDash)
        {
            _hasDash = false;
        }

        // Change the UI elements appropriately.
        SetDashUI();
    }

    private void Fill(float amount)
    {
        _currentDash = Mathf.Min(_currentDash + amount, _startingDash);

        if (_currentDash >= 100f)
        {
            _hasDash = true;
        }

        // Change the UI elements appropriately.
        SetDashUI();
    }

    // Return whether we are dried out
    public bool hasDash() {
        return _hasDash;
    }

}
