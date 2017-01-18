using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManagerScript : MonoBehaviour
{

	public Animator uiAnimator;

	public void loadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);	
	}

	public void openSelectMaps ()
	{
		uiAnimator.SetBool ("isSelectMapHidden", false);
	}
}
