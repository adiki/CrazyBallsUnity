using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManagerScript : MonoBehaviour
{

	public Animator animator;

	private int levelToOpen;

	public void showLevelsPanel ()
	{
		animator.SetBool ("isLevelsPanelHidden", false);
	}

	public void openLevel (int level)
	{
		animator.SetBool ("isGameHidden", false);
		levelToOpen = level;
	}

	private void loadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);	
	}

	public void AnimationDidFaded()
	{
		loadScene ("Level" + levelToOpen);
	}
}
