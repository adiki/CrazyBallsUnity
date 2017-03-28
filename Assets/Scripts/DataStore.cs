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

	public static void bumpLevel() 
	{
		if (unlockedLevelNumber () == 15) {
			return;
		}

		PlayerPrefs.SetInt ("unlockedLevelNumber", unlockedLevelNumber () + 1);
		PlayerPrefs.Save ();
	}
}
