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

	private void loadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);	
	}

	public void AnimationDidFaded()
	{
	}
}
