using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartScreen : MonoBehaviour
{

	public Animator animator;

	void Update() 
	{
		if (Input.GetKeyUp(KeyCode.Escape)) {
			if (Application.platform == RuntimePlatform.Android) {
				AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
				activity.Call<bool>("moveTaskToBack", true);
			}
		}
	}

	public void showLevelsPanel ()
	{
		animator.SetBool ("isLevelsPanelHidden", false);
	}

	public void AnimationDidFaded()
	{
		SceneManager.LoadScene ("MapScreen");
	}
}
