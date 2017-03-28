using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartScreen : MonoBehaviour
{

	public Animator animator;

	public void showLevelsPanel ()
	{
		animator.SetBool ("isLevelsPanelHidden", false);
	}

	public void AnimationDidFaded()
	{
		SceneManager.LoadScene ("MapScreen");
	}
}
