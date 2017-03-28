using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStore : MonoBehaviour
{

	public static int unlockedLevelNumber ()
	{
		if (!PlayerPrefs.HasKey ("unlockedLevelNumber")) {
			return 1;
		}
		return PlayerPrefs.GetInt ("unlockedLevelNumber");
	}

	public static void bumpLevel ()
	{
		if (unlockedLevelNumber () == 15) {
			return;
		}

		PlayerPrefs.SetInt ("unlockedLevelNumber", unlockedLevelNumber () + 1);
		PlayerPrefs.Save ();
	}

	public static int starsForLevel (int levelNumber)
	{
		if (!PlayerPrefs.HasKey ("levelStars" + levelNumber)) {
			return 0;
		}
		return PlayerPrefs.GetInt ("levelStars" + levelNumber);
	}

	public static void updateStarsForLevel (int levelNumber, int stars)
	{
		if (starsForLevel (levelNumber) > stars) {
			return;
		}

		PlayerPrefs.SetInt ("levelStars" + levelNumber, stars);
		PlayerPrefs.Save ();
	}
}
