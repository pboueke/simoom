using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour {

    public int _xp;

    public Text playerLevelDisplay;
    public Image playerXpCircle;

    // Use this for initialization
    void Start () {
        _xp = 0;

        // starting lvl: 1
        addXp(1);
    }

    /// <summary>
    /// Adds experience to the player`s leveling system.
    /// </summary>
    /// <param name="experience"> The amount of e_xperience to be added.</param>
    public void addXp(int experience)
    {
        _xp += experience;
        // update UI
        int currentLevel = getLevel();
        int xpForCurrentLevel = getXpForLevel(currentLevel);
        int xpForNextLevel = getXpForLevel(currentLevel + 1);
        playerLevelDisplay.text = (currentLevel < 10) ? "0" + currentLevel.ToString() : currentLevel.ToString();
        playerXpCircle.fillAmount = (float)(_xp - xpForCurrentLevel) / (float)(xpForNextLevel - xpForCurrentLevel);
    }

    /// <summary>
    /// Gets the player level based on the current _xp. 
    /// This version simply uses the SQRT aproach, it may be revisited in the future (if so, getXpForLevel also needs an update).
    /// </summary>
    /// <returns>The current level of the player.</returns>
    private int getLevel()
    {
        return (int)(Mathf.Floor(Mathf.Sqrt(_xp)));
    }

    /// <summary>
    /// Does the opposite of GetLevel(), returning the amount of XP needed for the targeted level.
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private int getXpForLevel (int level)
    {
        return (int)Mathf.Pow(level, 2f);
    }
}
