using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLevel : MonoBehaviour {

    public int _xp;
	public bool _hasKey = false;

    public Text playerLevelDisplay;
    public Image playerXpCircle;

    private PlayerShooting _playerShooting;

    private PlayerSounds _sound;

    // Use this for initialization
    void Start () {
        _xp = 0;
        _playerShooting = GetComponent<PlayerShooting>();

		SceneManager.sceneLoaded += OnSceneLoaded;

        _sound = GetComponent<PlayerSounds>();

        // starting lvl: 1
        addXp(1);
    }

	// delgate onsceneload function
	private void OnSceneLoaded(Scene aScene, LoadSceneMode aMode) {
		_hasKey = false;
	}

    private void levelUp(int level)
    {
        _sound.LevelUp();

        if (level == 1) return;
        if (level % 2 == 1) {
            // Odd level up - increase power
            _playerShooting.increasePower();
        } else {
            // Even level up - increase density
            _playerShooting.increaseDensity();
        }
    }

    /// <summary>
    /// Adds experience to the player`s leveling system.
    /// </summary>
    /// <param name="experience"> The amount of e_xperience to be added.</param>
    public void addXp(int experience)
    {
        // control level up
        int currentLevel = getLevel();
        _xp += experience;
        int newLevel = getLevel();
        if (newLevel != currentLevel) {
            for (int i = currentLevel; i < newLevel; i++) {
                levelUp(i + 1);
            }
        }
        // update UI
        int xpForCurrentLevel = getXpForLevel(newLevel);
        int xpForNextLevel = getXpForLevel(newLevel + 1);
        playerLevelDisplay.text = (newLevel < 10) ? "0" + newLevel.ToString() : newLevel.ToString();
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
