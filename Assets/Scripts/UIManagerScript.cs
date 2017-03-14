using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManagerScript : MonoBehaviour
{

	public Animator animator;

	public void showLevelsPanel ()
	{
		animator.SetBool ("isLevelsPanelHidden", false);
	}

	public void openLevel (int level)
	{
		loadScene ("Level" + level);
	}

	public void openRecordMode ()
	{
		animator.SetBool ("isGameHidden", false);
		loadScene ("RecordMode");
	}

	private void loadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);	
	}
}
