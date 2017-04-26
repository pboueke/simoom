using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour {

    public float _levelMultiplier = 1;
    public int _xp;

    // Use this for initialization
    void Start () {
        _xp = 0;
	}

    /// <summary>
    /// Adds experience to the player`s leveling system.
    /// </summary>
    /// <param name="experience"> The amount of e_xperience to be added.</param>
    public void addXp(int experience)
    {
        _xp += experience;
    }

    /// <summary>
    /// Gets the player level based on the current _xp. 
    /// This version simply uses the SQRT aproach, it may be revisited in the future.
    /// </summary>
    /// <returns>The current level of the player.</returns>
    private int getLevel()
    {
        return (int)(Mathf.Floor(_levelMultiplier * Mathf.Sqrt(_xp)));
    }
}
