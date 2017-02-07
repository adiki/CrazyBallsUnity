using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManagerScript : MonoBehaviour
{

	public Animator animator;

	public void loadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);	
	}

	public void showLevelsPanel() {
		animator.SetBool ("isLevelsPanelHidden", false);
	}
}
